#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Musicmania.Extensions
{
    /// <summary>
    /// Provides extension methods for IEnumerable<T> type.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Shuffles the elements of a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to shuffle.</param>
        /// <returns>A sequence whose elements are shuffled.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var rng = new Random();

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}