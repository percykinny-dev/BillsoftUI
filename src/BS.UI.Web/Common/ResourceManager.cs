//namespace Presentation.Web.Common
//{

    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Localization;
    using Presentation.Web.Common;

    public class ResourceManager
    {
        private readonly IStringLocalizer<ResourceManager> _localizer;
        private readonly Dictionary<string, string> _cachedResources;

        public ResourceManager(IStringLocalizer<ResourceManager> localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _cachedResources = LoadResources();
        }

        public string GetResource(ResourceKeyEnum key)
        {
            return _cachedResources[key.ToString()];
        }

        public Dictionary<string, string>  GetResources()
        {
            return _cachedResources;
        }

        private Dictionary<string, string> LoadResources()
        {
            var resources = new Dictionary<string, string>();

            foreach (ResourceKeyEnum key in Enum.GetValues(typeof(ResourceKeyEnum)))
            {
                resources[key.ToString()] = _localizer[key.ToString()];
            }

            return resources;
        }
    }
//}