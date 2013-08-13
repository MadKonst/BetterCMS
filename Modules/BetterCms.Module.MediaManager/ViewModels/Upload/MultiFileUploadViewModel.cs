﻿using System;
using System.Collections.Generic;

using BetterCms.Core.Security;
using BetterCms.Module.MediaManager.Models;
using BetterCms.Module.Root.ViewModels.Security;

namespace BetterCms.Module.MediaManager.ViewModels.Upload
{
    [Serializable]
    public class MultiFileUploadViewModel
    {
        public Guid RootFolderId { get; set; }

        public MediaType RootFolderType { get; set; }

        public Guid? SelectedFolderId { get; set; }

        public IList<Tuple<Guid, string>> Folders { get; set; }

        public IList<Guid> UploadedFiles { get; set; }

        public Guid ReuploadMediaId { get; set; }

        public List<IUserAccess> UserAccessList { get; set; }

        public bool AccessControlEnabled { get; set; }

        public MultiFileUploadViewModel()
        {
            UserAccessList = new List<IUserAccess>();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("RootFolderId: {0}, RootFolderType: {1}, SelectedFolderId: {2}, ReuploadMediaId: {3}", RootFolderId, RootFolderType, SelectedFolderId, ReuploadMediaId);
        }
    }
}