using Musicmania.Data;
using System.Collections.Generic;

namespace Musicmania
{
    public class CategoryStorage
    {
        public CategoryStorage(IEnumerable<CategoryData> categories)
        {
            Categories = new List<CategoryData>(categories);
        }

        public IReadOnlyList<CategoryData> Categories { get; }
    }
}