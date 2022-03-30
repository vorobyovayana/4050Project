using FinaiProejct_200OK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinaiProejct_200OK.Utilities
{
    class MovieParser
    {
        public static List<Movie> movies = new List<Movie>();

        public static List<Movie> parseRoaster(string fileContent)
        {
            string[] lines = fileContent.Split('\n');
            

            foreach (string line in lines)
            {
                
                string[] fields = line.Trim().Split(',');
                if (fields.Length != 2)
                {
                    MessageBox.Show("There is a problem in parse. " + fields[0]);
                }
                else
                {
                    try
                    {
                        Movie mv = new Movie();
                        mv.MovieTitle = fields[0];
                        mv.ReleaseDate = DateTime.Parse(fields[1]);
                        movies.Add(mv);
                    } catch (Exception ex)
                    {
                        MessageBox.Show("Parse error: " + ex.Message);
                    }
                    
                }

            }

            return movies;

        }
    }
}
