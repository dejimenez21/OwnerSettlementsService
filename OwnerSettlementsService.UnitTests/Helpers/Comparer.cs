using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.UnitTests.Helpers
{
    public class Comparer<T, TKey> : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.SerializeObject(x, settings) == JsonConvert.SerializeObject(y, settings);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
