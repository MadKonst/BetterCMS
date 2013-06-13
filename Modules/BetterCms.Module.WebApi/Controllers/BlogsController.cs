﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;

using BetterCms.Api;
using BetterCms.Core;
using BetterCms.Module.Blog.Api.DataFilters;
using BetterCms.Module.Blog.Api.DataModels;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.WebApi.Extensions;

using Microsoft.Web.Mvc;

namespace BetterCms.Module.WebApi.Controllers
{
    /// <summary>
    /// Blogs API controller.
    /// </summary>
    [ActionLinkArea(WebApiModuleDescriptor.WebApiAreaName)]
    public class BlogsController : ApiController
    {
        /// <summary>
        /// Gets the list of blog posts by specified filter.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// List of blog post service models.
        /// </returns>
        public IList<BlogPostModel> Get(ODataQueryOptions<BlogPostModel> options, [FromUri] BlogPostFilter filter)
        {
            using (var api = CmsContext.CreateApiContextOf<BlogsApiContext>())
            {
                var models = api.GetBlogPostsAsQueryable(filter);
                var results = options.ApplyToModels(models);
                return results.ToList();
            }
        }

        /// <summary>
        /// Gets the blog post by specified ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Blog post service model</returns>
        public BlogPostModel Get(string id)
        {
            using (var api = CmsContext.CreateApiContextOf<BlogsApiContext>())
            {
                return api.GetBlogPostsAsQueryable().FirstOrDefault(w => w.Id == id.ToGuidOrDefault());
            }
        }
    }
}