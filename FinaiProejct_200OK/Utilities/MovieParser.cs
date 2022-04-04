using FinaiProejct_200OK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FinaiProejct_200OK.Utilities
{
    class MovieParser
    {
        public static List<Movie> movies;
        public static List<Movie> ParseMovie(string fileContents)
        {
            movies = new List<Movie>();
            string[] lines = fileContents.Split('\n');

            foreach (string line in lines)
            {
                //Skip if line is empty
                if (string.IsNullOrEmpty(line)) continue;

                string[] fields = line.Trim().Split(',');
                //MessageBox.Show(fields.Length.ToString());
                if (fields.Length != 2)
                {
                    System.Windows.MessageBox.Show("Problem parsing file, check format");
                    continue;
                }
                else
                {
                    try
                    {
                        Movie newMovie = new Movie();
                        newMovie.MovieTitle = fields[0];
                        newMovie.ReleaseDate = DateTime.Parse(fields[1]);
                        movies.Add(newMovie);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return movies;
        }
    
}
}
