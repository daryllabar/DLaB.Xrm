#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

#if DLAB_UNROOT_COMMON_NAMESPACE
namespace DLaB.Common
#else
namespace Source.DLaB.Common
#endif
{
    public class ToCsvOptions
    {
        public bool SkipNullValues { get; set; }
        public bool SkipEmptyStrings { get; set; }
        public string? DefaultValue { get; set; }
    }

    public static class ToCsvExtensions
    {
        public static string ToCsv(this IEnumerable<string?> items, ToCsvOptions? options)
        {
            if (items == null) { throw new ArgumentNullException(nameof(items)); }

            options ??= new ToCsvOptions();

            var values = items;

            if (options.SkipNullValues)
            {
                values = values.Where(i => i != null);
            }

            if (options.SkipEmptyStrings)
            {
                values = values.Where(i => !string.IsNullOrEmpty(i));
            }

            var filteredValues = values.ToList();
            if (filteredValues.Count == 0)
            {
                return options.DefaultValue ?? string.Empty;
            }

            return string.Join(", ", filteredValues);
        }
    }
}
