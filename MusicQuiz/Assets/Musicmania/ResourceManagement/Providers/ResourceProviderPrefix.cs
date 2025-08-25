#nullable enable

using System;

namespace Musicmania.ResourceManagement.Providers
{
    [Obsolete("This enum should be used later when the resource provider system is fully implemented.")]
    public enum ResourceProviderPrefix
    {
        None,
        Resources,
        Web,
        AssetBundle,
        Addressable,
    }
}
