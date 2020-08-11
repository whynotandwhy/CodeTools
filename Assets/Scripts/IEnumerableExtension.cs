using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IEnumerableExtention
{
    public static class IEnumerableExtension 
    {
        public static T ProtectedIndex<T>(this IEnumerable<T> collection, uint index)
        {
            return collection.ElementAt((int)index % collection.Count());
        }

        public static T ProtectedIndex<T>(this IEnumerable<T> collection, int index)
        {
            int desiredInt = index % collection.Count();
            if (desiredInt < 0)
                desiredInt += collection.Count();

            return collection.ElementAt(desiredInt);
        }
    }
}