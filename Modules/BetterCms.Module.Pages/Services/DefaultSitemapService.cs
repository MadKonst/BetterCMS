﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

using BetterCms.Core.DataAccess;
using BetterCms.Module.Pages.Models;
using BetterCms.Module.Root.Models;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Helpers;

using NHibernate.Linq;

namespace BetterCms.Module.Pages.Services
{
    /// <summary>
    /// Default sitemap service.
    /// </summary>
    public class DefaultSitemapService : ISitemapService
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSitemapService" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DefaultSitemapService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets the nodes by URL.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// Node list.
        /// </returns>
        public IList<SitemapNode> GetNodesByPage(Page page)
        {
            var urlHash = page.PageUrl.UrlHash();
            return repository
                .AsQueryable<SitemapNode>()
                .Where(n => (n.Page != null && n.Page.Id == page.Id) || (n.UrlHash == urlHash))
                .Fetch(node => node.Sitemap)
                .Fetch(node => node.ChildNodes)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Changes the URL in all nodes for all sitemaps (creates sitemap archives).
        /// </summary>
        /// <param name="oldUrl">The old URL.</param>
        /// <param name="newUrl">The new URL.</param>
        /// <returns>
        /// Node with new url count.
        /// </returns>
        public IList<SitemapNode> ChangeUrlsInAllSitemapsNodes(string oldUrl, string newUrl)
        {
            var oldUrlHash = (oldUrl ?? string.Empty).UrlHash();
            newUrl = newUrl ?? string.Empty;

            var updatedNodes = new List<SitemapNode>();

            var nodes = repository.AsQueryable<SitemapNode>()
                .Where(n => n.UrlHash == oldUrlHash)
                .Fetch(node => node.Sitemap)
                .Distinct()
                .ToList();

            var sitemaps = nodes
                .Select(node => node.Sitemap)
                .Distinct()
                .ToList();

            sitemaps.ForEach(sitemap => ArchiveSitemap(sitemap.Id));

            foreach (var node in nodes)
            {
                node.Url = newUrl;
                node.UrlHash = newUrl.UrlHash();
                repository.Save(node);
                updatedNodes.Add(node);
            }

            return updatedNodes;
        }

        /// <summary>
        /// Archives sitemap and deletes the node.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="version">The version.</param>
        /// <param name="deletedNodes">The deleted nodes.</param>
        public void DeleteNode(Guid id, int version, out IList<SitemapNode> deletedNodes)
        {
            deletedNodes = new List<SitemapNode>();

            var node = repository.AsQueryable<SitemapNode>()
                .Where(sitemapNode => sitemapNode.Id == id)
                .Fetch(sitemapNode => sitemapNode.Sitemap)
                .Distinct()
                .First();

            ArchiveSitemap(node.Sitemap.Id);

            node.Version = version;
            DeleteNode(node, ref deletedNodes);
        }

        /// <summary>
        /// Saves the node (does not archive sitemap).
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <param name="nodeId">The id.</param>
        /// <param name="version">The version.</param>
        /// <param name="url">The URL.</param>
        /// <param name="title">The title.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="displayOrder">The display order.</param>
        /// <param name="parentId">The parent id.</param>
        /// <param name="isDeleted">if set to <c>true</c> node is deleted.</param>
        /// <param name="parentNode">The parent node.</param>
        /// <returns>
        /// Updated or newly created sitemap node.
        /// </returns>
        public SitemapNode SaveNode(Sitemap sitemap, Guid nodeId, int version, string url, string title, Guid pageId, int displayOrder, Guid parentId, bool isDeleted = false, SitemapNode parentNode = null)
        {
            var node = nodeId.HasDefaultValue()
                ? new SitemapNode()
                : repository.First<SitemapNode>(nodeId);

            if (isDeleted)
            {
                if (!node.Id.HasDefaultValue())
                {
                    repository.Delete(node);
                }
            }
            else
            {
                node.Sitemap = sitemap;
                node.Version = version;
                node.Title = title;
                node.Page = !pageId.HasDefaultValue() ? repository.AsProxy<PageProperties>(pageId) : null;
                node.Url = node.Page != null ? null : url;
                node.UrlHash = node.Page != null ? null : url.UrlHash();
                node.DisplayOrder = displayOrder;
                if (parentNode != null && !parentNode.Id.HasDefaultValue())
                {
                    node.ParentNode = parentNode;
                }
                else
                {
                    node.ParentNode = parentId.HasDefaultValue()
                        ? null
                        : repository.First<SitemapNode>(parentId);
                }

                repository.Save(node);
            }

            return node;
        }

        /// <summary>
        /// Deletes the node (does not archive sitemap).
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="deletedNodes">The deleted nodes.</param>
        public void DeleteNode(SitemapNode node, ref IList<SitemapNode> deletedNodes)
        {
            foreach (var childNode in node.ChildNodes)
            {
                DeleteNode(childNode, ref deletedNodes);
            }

            repository.Delete(node);
            deletedNodes.Add(node);
        }

        #region History

        /// <summary>
        /// Gets the sitemap history.
        /// </summary>
        /// <param name="sitemapId">The sitemap identifier.</param>
        /// <returns>
        /// Sitemap previous archived versions.
        /// </returns>
        public IList<SitemapArchive> GetSitemapHistory(Guid sitemapId)
        {
            return repository
                .AsQueryable<SitemapArchive>()
                .Where(archive => archive.Sitemap.Id == sitemapId)
                .OrderByDescending(archive => archive.CreatedOn)
                .ToList();
        }

        /// <summary>
        /// Archives the sitemap.
        /// </summary>
        /// <param name="sitemapId">The sitemap identifier.</param>
        public void ArchiveSitemap(Guid sitemapId)
        {
            var sitemap = repository.AsQueryable<Sitemap>()
                .Where(map => map.Id == sitemapId)
                .FetchMany(map => map.Nodes)
                .ThenFetch(node => node.Page)
                .Distinct()
                .ToList()
                .First();

            var archive = new SitemapArchive
                {
                    Sitemap = sitemap,
                    Title = sitemap.Title,
                    ArchivedVersion = ToJson(sitemap)
                };

            repository.Save(archive);
        }

        /// <summary>
        /// Gets the archived sitemap version for preview.
        /// </summary>
        /// <param name="archiveId">The archive identifier.</param>
        /// <returns>
        /// Sitemap entity.
        /// </returns>
        public Sitemap GetArchivedSitemapVersionForPreview(Guid archiveId)
        {
            var archive = repository
                .AsQueryable<SitemapArchive>()
                .First(map => map.Id == archiveId);

            return FromJson(archive.ArchivedVersion);
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <returns>json representation of the sitemap.</returns>
        private static string ToJson(Sitemap sitemap)
        {
            var map = new ArchivedSitemap
            {
                Title = sitemap.Title,
                RootNodes = sitemap.Nodes != null
                    ? GetSitemapNodesInHierarchy(sitemap.Nodes.Where(f => f.ParentNode == null).OrderBy(sitemapNode => sitemapNode.DisplayOrder).ToList(), sitemap.Nodes.ToList())
                    : new List<ArchivedNode>()
            };

            var serializer = new JavaScriptSerializer();

            var serialized = serializer.Serialize(map);

            return serialized;
        }

        /// <summary>
        /// From the json forms sitemap.
        /// </summary>
        /// <param name="archivedVersion">The archived version.</param>
        /// <returns>the sitemap made from json.</returns>
        private static Sitemap FromJson(string archivedVersion)
        {
            var serializer = new JavaScriptSerializer();

            var deserialized = serializer.Deserialize<ArchivedSitemap>(archivedVersion);

            if (deserialized != null)
            {
                var sitemap = new Sitemap()
                {
                    Title = deserialized.Title,
                    Nodes = new List<SitemapNode>()
                };
                AddNodes(sitemap, deserialized.RootNodes, null);
                return sitemap;
            }

            return null;
        }

        /// <summary>
        /// Adds the nodes.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <param name="archivedNodes">The archived nodes.</param>
        /// <param name="parentNode">The parent node.</param>
        /// <returns>
        /// Sitemap node list.
        /// </returns>
        private static List<SitemapNode> AddNodes(Sitemap sitemap, IEnumerable<ArchivedNode> archivedNodes, SitemapNode parentNode)
        {
            var nodes = new List<SitemapNode>();
            foreach (var archivedNode in archivedNodes)
            {
                var node = new SitemapNode
                    {
                        Title = archivedNode.Title,
                        Url = archivedNode.Url,
                        Page = !archivedNode.PageId.HasDefaultValue()
                                ? new PageProperties()
                                        {
                                            Id = archivedNode.PageId,
                                            PageUrl = archivedNode.Url
                                        }
                                : null,
                        DisplayOrder = archivedNode.DisplayOrder,
                        ParentNode = parentNode
                    };

                node.ChildNodes = AddNodes(sitemap, archivedNode.Nodes, node);
                nodes.Add(node);
            }

            nodes.ForEach(sitemap.Nodes.Add);
            return nodes.OrderBy(node => node.DisplayOrder).ToList();
        }

        /// <summary>
        /// Gets the sitemap nodes in hierarchy.
        /// </summary>
        /// <param name="sitemapNodes">The sitemap nodes.</param>
        /// <param name="allNodes">All nodes.</param>
        /// <returns>Archived node list.</returns>
        private static List<ArchivedNode> GetSitemapNodesInHierarchy(IEnumerable<SitemapNode> sitemapNodes, IList<SitemapNode> allNodes)
        {
            var nodeList = new List<ArchivedNode>();

            foreach (var node in sitemapNodes)
            {
                nodeList.Add(new ArchivedNode
                {
                    Title = node.Title,
                    Url = node.Page != null ? node.Page.PageUrl : node.Url,
                    PageId = node.Page != null ? node.Page.Id : Guid.Empty,
                    DisplayOrder = node.DisplayOrder,
                    Nodes = GetSitemapNodesInHierarchy(allNodes.Where(f => f.ParentNode == node).OrderBy(sitemapNode => sitemapNode.DisplayOrder).ToList(), allNodes)
                });
            }

            return nodeList.OrderBy(n => n.DisplayOrder).ToList();
        }

        /// <summary>
        /// Class for archived sitemap node representation.
        /// </summary>
        private class ArchivedNode
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public Guid PageId { get; set; }
            public int DisplayOrder { get; set; }
            public List<ArchivedNode> Nodes { get; set; }
        }

        /// <summary>
        /// Class for archived sitemap representation.
        /// </summary>
        private class ArchivedSitemap
        {
            public string Title { get; set; }
            public List<ArchivedNode> RootNodes { get; set; }
        }

        #endregion
    }
}