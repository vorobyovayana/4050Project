
using FinaiProejct_200OK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinaiProejct_200OK.Utilities
{
    class ReviewCSVParser
    {
        public static List<Review> parseRoster(String fileContents)
        {
            IEnumerable<Review> reviews = Enumerable.Empty<Review>();
            // get rows by spliting '\n' syntax
            string[] lines = fileContents.Split('\n');
            try
            {
                reviews = lines.Select(line => line.Split(','))
                    .Where(values => values[0] != "")
                    .Where(values => values[0] != "ReviewId")
                       .Select(values =>
                       new Review(
                        Convert.ToInt32(values[0]),
                        Convert.ToInt32(values[1]),
                        Convert.ToInt32(values[2]),
                        values[3]
                        )
                       );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return reviews.ToList<Review>();
        }
    }
}