using System.Collections.Generic;

namespace Musicmania.Data.Categories
{
    /// <summary>
    /// Represents a save file containing a list of categories.
    /// </summary>
    public class CategoriesCollectionData : UnityEngine.Object
    {
        /// <summary>
        /// The list of categories.
        /// </summary>
        public List<CategoryData> Categories = new();
    }
}
