using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathConverter
{
    class Converter
    {
        public static string[] Convert(string input)
        {
            var results = new string[4];
            if (String.IsNullOrEmpty(input)) return results;

            var parts = input.Split(new[] { '/', '\\' });
            if (parts.Length < 2) return results;

            // suppose input is valid path to make things simple

            var first = parts[0];
            string driver = null;
            string driver2 = null;
            bool skipSecond = false;
            if (first == String.Empty)
            {
                var second = parts[1];
                if (second.Length == 1 && Char.IsLetter(second, 0))
                {
                    driver = second + ':';
                    driver2 = "/" + second;
                    skipSecond = true;
                }
            }
            else if (first.EndsWith(":"))
            {
                driver = first;
                driver2 = "/" + first.TrimEnd(':');
            }

            var list = new List<string> { driver ?? first };
            // clear the extra path delimiters
            var i = skipSecond ? 2 : 1;
            for (; i < parts.Length; i++)
            {
                var item = parts[i];
                if (item != String.Empty) list.Add(item);
            }
            // the last path delimiter should be preserved
            var last = parts.Last();
            if (last == String.Empty) list.Add(last);

            var s3 = String.Join(@"/", list);
            results = new string[]
            {
                String.Join(@"\", list),
                String.Join(@"\\", list),
                s3,
                s3
            };

            if (!String.IsNullOrEmpty(driver2))
            {
                list[0] = driver2;
                results[3] = String.Join(@"/", list);
            }

            return results;
        }
    }
}
