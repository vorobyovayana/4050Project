
using FinaiProejct_200OK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinaiProejct_200OK.Utilities
{
    class FavoriteCSVParser
    {
        public static List<Favorite> parseRoster(String fileContents)
        {
            IEnumerable<Favorite> favorites = Enumerable.Empty<Favorite>();
            // get rows by spliting '\n' syntax
            string[] lines = fileContents.Split('\n');
            try
            {
                favorites = lines.Select(line => line.Split(','))
                    .Where(values => values[0] != "")
                    .Where(values => values[0] != "MemberId")
                       .Select(values =>
                       new Favorite(
                        Convert.ToInt32(values[0]),
                        Convert.ToInt32(values[1])
                        )
                       );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return favorites.ToList<Favorite>();
        }
    }
}