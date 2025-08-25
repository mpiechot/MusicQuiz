using Musicmania.ResourceManagement.Providers;
using System;
using System.Collections.Generic;

namespace Musicmania.ResourceManagement
{
    public class ResourceHandleFactory
    {
        private readonly Dictionary<string, IResourceProvider> providers = new();
        public ResourceHandleFactory()
        {
            RegisterProvider(AddressablesProvider.Prefix, new AddressablesProvider());
            RegisterProvider(ResourcesFolderProvider.Prefix, new ResourcesFolderProvider());
            RegisterProvider(WebResourceProvider.Prefix, new WebResourceProvider());
            RegisterProvider(JsonResourceProvider.Prefix, new JsonResourceProvider());
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
