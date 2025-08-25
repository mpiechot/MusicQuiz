#nullable enable

namespace Musicmania.Data.Categories
{
    /// <summary>
    /// Represents a category of music with a name and a thumbnail resource.
    /// </summary>
    public class CategoryData
    {
        /// <summary>
        ///     Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///    Gets or sets the thumbnail resource key of the category.
        ///    This is used to load the resource via the <see cref="ResourceManagement.ResourceManager"/>
        /// </summary>
        public string ThumbnailResourceKey { get; set; } = string.Empty;
    }
}
