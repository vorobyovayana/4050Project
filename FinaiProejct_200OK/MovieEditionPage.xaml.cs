﻿using FinaiProejct_200OK.Entities;
using FinaiProejct_200OK.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinaiProejct_200OK
{
    /// <summary>
    /// Interaction logic for MovieEditionPage.xaml
    /// </summary>
    public partial class MovieEditionPage : Window
    {
        
        public MovieEditionPage()
        {
            InitializeComponent();
            EditionAddMovieButton.Visibility = Visibility.Visible;
            toggleEvent(true);
        }

        public MovieEditionPage(int MovieId)
        {
            InitializeComponent();
            EditionEditMovieButton.Visibility = Visibility.Visible;
            EditionMovieIdTextBox.Text = MovieId.ToString();
            toggleEvent(true);
            try
            {
                using (var ctx = new MovieContext())
                {
                    var currentMovie = ctx.Movie.Where(x => x.MovieId.ToString() == EditionMovieIdTextBox.Text).First();
                    EditionMovieTitleTextBox.Text = currentMovie.MovieTitle;
                    EditionReleaseTextBox.Text = currentMovie.ReleaseDate.ToShortDateString();
                    EditionDirectorTextBox.Text = currentMovie.DirectorId.ToString();
                    EditionGenreTextBox.Text = currentMovie.GenreId.ToString();
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Something wrong when get the data from database! " + ex.Message);
                
            }

        }

        private void EditionButtonClick(Object o, EventArgs e)
        {
            try
            {
                using (var ctx = new MovieContext())
                {
                    DateTime theDate;
                    var currentMovie = ctx.Movie.Where(x => x.MovieId.ToString() == EditionMovieIdTextBox.Text).First();
                    currentMovie.MovieTitle = EditionMovieTitleTextBox.Text;                    
                    currentMovie.DirectorId = Convert.ToInt32(EditionDirectorTextBox.Text);
                    currentMovie.GenreId = Convert.ToInt32(EditionGenreTextBox.Text);

                    if (DateTime.TryParse(EditionReleaseTextBox.Text, out theDate))
                    {
                        currentMovie.ReleaseDate = theDate;
                        ctx.SaveChanges();
                        this.Close();
                    } else
                    {
                        EditionHintTextBox.Text = "The date time format is wrong!";
                    }

                    
                }
                
                
            } catch (Exception ex)
            {
                MessageBox.Show("No such element! " + ex.Message);
            }
            
        }

        private void AddButtonClick(Object o, EventArgs e)
        {
            if (EditionMovieTitleTextBox.Text.Length == 0 || EditionReleaseTextBox.Text.Length == 0 || EditionDirectorTextBox.Text.Length == 0
                || EditionGenreTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter all fields");
            }
            try
            {
                using (var ctx = new MovieContext())
                {
                    DateTime myDate;
                    Movie newMovie = new Movie();
                    newMovie.MovieTitle = EditionMovieTitleTextBox.Text;
                    newMovie.GenreId = Convert.ToInt32(EditionGenreTextBox.Text);
                    newMovie.DirectorId = Convert.ToInt32(EditionDirectorTextBox.Text);
                    if (DateTime.TryParse(EditionReleaseTextBox.Text, out myDate))
                    {
                        newMovie.ReleaseDate = myDate;
                        ctx.Movie.Add(newMovie);
                        ctx.SaveChanges();
                        this.Close();
                    } else
                    {
                        EditionHintTextBox.Text = "Date time format is wrong!";
                    }
                    
                }
            } catch (Exception ex)
            {
                MessageBox.Show("There are something wrong when data update to database! " );
                EditionHintTextBox.Text = "Some Input are wrong!";
            }
        }

        private void toggleEvent(bool toggle)
        {
            if (toggle)
            {
                EditionEditMovieButton.Click += EditionButtonClick;
                EditionAddMovieButton.Click += AddButtonClick;
            } else
            {

            }
        }
    }
}