using Microsoft.Win32;
using MovieManager.Dal;
using MovieManager.Models;
using MovieManager.Utils;
using MovieManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for EditMoviePage.xaml
    /// </summary>
    public partial class EditMoviePage : FramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";

        private readonly Movie? movie;
        public EditMoviePage(MovieViewModel movieViewModel, ActorViewModel actorViewModel, 
            DirectorViewModel directorViewModel, Movie? movie = null) 
            : base(movieViewModel, actorViewModel, directorViewModel)
        {
            InitializeComponent();
            this.movie = movie ?? new Movie(); // Elvis in action
            DataContext = movie;
            Init();
            cbActors.ItemsSource = actorViewModel.Actors;
            cbDirectors.ItemsSource = directorViewModel.Directors;
        }

        private void Init()
        {
            if (movie?.Actors != null)
            {
                foreach (var item in movie.Actors)
                {
                    lbActors.Items.Add(item);
                }
            }

            if (movie?.Directors != null)
            {
                foreach (var item in movie.Directors)
                {
                    lbDirectors.Items.Add(item);
                }
            }
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                movie!.Title = tbTitle.Text.Trim();
                movie!.MovieDescription = tbDescription.Text.Trim();
                movie!.Duration = tbDuration.Text.Trim();
                movie!.Link = tbLink.Text.Trim();
                movie!.Picture = ImageUtils.BitmapImageToByteArray((picture.Source as BitmapImage)!);
                movie!.Actors = RefreshActors();
                movie!.Directors = RefreshDirectors();
                if (movie.IdMovie == 0)
                {
                    MovieViewModel.Movies.Add(movie);
                    MovieViewModel.UpdateMovie(movie);
                }
                else
                {
                    MovieViewModel.UpdateMovie(movie);
                }
                Frame?.NavigationService.GoBack();
            }
        }

        private ObservableCollection<Director>? RefreshDirectors()
        {
            ObservableCollection<Director> directorsList = new ObservableCollection<Director>();

            foreach (var item in lbDirectors.Items)
            {
                directorsList.Add((Director)item);
            }

            return directorsList;
        }

        private ObservableCollection<Actor>? RefreshActors()
        {
            ObservableCollection<Actor> actorsList = new ObservableCollection<Actor>();

            foreach (var item in lbActors.Items)
            {
                actorsList.Add((Actor)item);
            }

            return actorsList;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true) // interesting -> can be null
            {
                picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame?.NavigationService.GoBack();

        private bool FormValid()
        {
            bool valid = true;
            gridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                e.Background = Brushes.White;
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Link".Equals(e.Tag) && !ValidationUtils.IsValidUrl(e.Text.Trim())))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
            });

            pictureBorder.BorderBrush = Brushes.WhiteSmoke;
            if (picture.Source == null)
            {
                pictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            }

            return valid;
        }

        private void Actors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbActors.SelectedItem != null)
            {
                Actor selectedActor = (Actor)cbActors.SelectedItem;

                lbActors.Items.Add(selectedActor);
            }
        }

        private void LbActors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Actor selectedActor = (Actor)lbActors.SelectedItem;

            if (selectedActor != null)
            {
                lbActors.Items.Remove(selectedActor);
            }
        }

        private void CbDirectors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDirectors.SelectedItem != null)
            {
                Director selectedDirector = (Director)cbDirectors.SelectedItem;

                lbDirectors.Items.Add(selectedDirector);
            }
        }

        private void LbDirectors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Director selectedDirector = (Director)lbDirectors.SelectedItem;

            if (selectedDirector != null)
            {
                lbDirectors.Items.Remove(selectedDirector);
            }
        }
    }
}
