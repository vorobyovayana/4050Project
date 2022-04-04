
using FinaiProejct_200OK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinaiProejct_200OK.Utilities
{
    class MovieCSVParser
    {
        public static List<Movie> parseRoster(String fileContents)
        {
            IEnumerable<Movie> movies = Enumerable.Empty<Movie>();
            // get rows by spliting '\n' syntax
            string[] lines = fileContents.Split('\n');
            try
            {
                movies = lines.Select(line => line.Split(','))
                    .Where(values => values[0] != "")
                       .Select(values =>
                       new Movie(
                        Convert.ToInt32(values[0]),
                        values[1],
                        DateTime.Parse(values[2])
                        )
                       );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return movies.ToList<Movie>();
        }
    }
}