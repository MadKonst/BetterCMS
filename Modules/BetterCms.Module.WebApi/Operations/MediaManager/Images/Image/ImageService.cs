﻿using ServiceStack.ServiceInterface;

namespace BetterCms.Module.Api.Operations.MediaManager.Images.Image
{
    public class ImageService : Service, IImageService
    {
        public GetImageResponse Get(GetImageRequest request)
        {
            // TODO: implement
            return new GetImageResponse
            {
                Data = new ImageModel()
            };
        }
    }
}