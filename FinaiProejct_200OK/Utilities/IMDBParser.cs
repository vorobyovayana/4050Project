﻿
using FinaiProejct_200OK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinaiProejct_200OK.Utilities
{
    class IMDBParser
    {
        public static List<IMDBData> parseRoster(String fileContents)
        {
            IEnumerable<IMDBData> reviews = Enumerable.Empty<IMDBData>();
            // get rows by spliting '\n' syntax
            string[] lines = fileContents.Split('\n');
            try
            {
                reviews = lines.Select(line => line.Split(','))
                    .Where(values => values[0] != "")
                    .Where(values => values[0] != "MovieId")
                       .Select(values =>
                       new IMDBData(
                        Convert.ToInt32(values[0]),values[1],values[2])
                       );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return reviews.ToList<IMDBData>();
        }
    }
}