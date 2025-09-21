using Musicmania.ResourceManagement.Providers;
using Musicmania.Settings;
using System;
using System.Collections.Generic;

namespace Musicmania.ResourceManagement
{
    public class ResourceHandleFactory
    {
        private readonly Dictionary<string, IResourceProvider> providers = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceHandleFactory"/> class.
        /// </summary>
        /// <param name="resourceSettings">The settings containing provider configuration details.</param>
        public ResourceHandleFactory(ResourceSettings resourceSettings)
        {
            if (resourceSettings == null)
            {
                throw new ArgumentNullException(nameof(resourceSettings));
            }

            RegisterProvider(AddressablesProvider.Prefix, new AddressablesProvider());
            RegisterProvider(ResourcesFolderProvider.Prefix, new ResourcesFolderProvider());
            RegisterProvider(WebResourceProvider.Prefix, new WebResourceProvider());
            RegisterProvider(JsonResourceProvider.Prefix, new JsonResourceProvider());
            RegisterProvider(SpotifyResourceProvider.Prefix, new SpotifyResourceProvider(resourceSettings));
        }

        public IResourceProvider GetProvider(string prefix)
        {
            if (!providers.TryGetValue(prefix, out var provider))
            {
                throw new InvalidOperationException($"No provider registered for prefix '{prefix}'");
            }

            return provider;
        }

        private void RegisterProvider(string prefix, IResourceProvider provider)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentException("Prefix cannot be null or empty", nameof(prefix));
            }

            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            providers[prefix] = provider;
        }
    }
}
