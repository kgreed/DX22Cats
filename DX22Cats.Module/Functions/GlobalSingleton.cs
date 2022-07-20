using System;
using System.Linq;

namespace DX22Cats.Module.Functions
{
    public sealed class GlobalSingleton
    {
        private static readonly Lazy<GlobalSingleton>
            lazy = new(() => new GlobalSingleton());

        public static GlobalSingleton Instance => lazy.Value;

        public bool RefreshCats { get; set; }
        public bool RefreshCatFilterHolder { get; set; }
    }
}
