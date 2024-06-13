using MovieManager.Models;
using MovieManager.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieManager
{
    /// <summary>
    /// Interaction logic for ListMoviesPage.xaml
    /// </summary>
    public partial class ListMoviesPage : FramedPage
    {
        public ListMoviesPage(MovieViewModel movieViewModel, ActorViewModel actorViewModel,
            DirectorViewModel directorViewModel) : base(movieViewModel, actorViewModel, directorViewModel)
        {
            InitializeComponent();
            lvMovies.ItemsSource = movieViewModel.Movies;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Frame?.Navigate(new EditMoviePage(MovieViewModel, ActorViewModel, DirectorViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvMovies.SelectedItem != null)
            {
                Frame?.Navigate(new EditMoviePage(MovieViewModel, ActorViewModel, DirectorViewModel,
                    lvMovies.SelectedItem as Movie) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvMovies.SelectedItem != null)
            {
                MovieViewModel.Movies.Remove((lvMovies.SelectedItem as Movie)!);
            }
        }

        private void BtnActors_Click(object sender, RoutedEventArgs e)
        {
            Frame?.Navigate(new ListActorsPage(ActorViewModel) { Frame = Frame });
        }

        private void BtnDirectors_Click(object sender, RoutedEventArgs e)
        {
            Frame?.Navigate(new ListDirectorsPage(DirectorViewModel) { Frame = Frame });
        }
    }
}
