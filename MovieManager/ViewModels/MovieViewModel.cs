using MovieManager.Dal;
using MovieManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieManager.ViewModels
{
    public class MovieViewModel
    {
        public ObservableCollection<Movie> Movies { get; }
        public MovieViewModel()
        {
            Movies = new ObservableCollection<Movie>(RepositoryFactory.GetRepository().GetMovies());
            foreach (var item in Movies)
            {
                item.Actors = new ObservableCollection<Actor>(RepositoryFactory.GetRepository().GetMovieActors(item.IdMovie));
                item.Directors = new ObservableCollection<Director>(RepositoryFactory.GetRepository().GetMovieDirectors(item.IdMovie));
                //item.Actors = RepositoryFactory.GetRepository().GetMovieActors(item.IdMovie).ToList();
                //item.Directors = RepositoryFactory.GetRepository().GetMovieDirectors(item.IdMovie).ToList();
            }
            Movies.CollectionChanged += Movies_CollectionChanged;
        }

        private void Movies_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddMovie(Movies[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteMovie(e.OldItems!.OfType<Movie>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateMovie(e.NewItems!.OfType<Movie>().ToList()[0]);
                    
                    break;
            }
        }

        // this is called on Update -> that triggers Replace!
        public void UpdateMovie(Movie movie)
        {
            RepositoryFactory.GetRepository().DeleteMovieActors(movie);

            foreach (var actor in movie.Actors)
            {
                RepositoryFactory.GetRepository().SetMovieActors(movie.IdMovie, actor.IdActor);
            }

            RepositoryFactory.GetRepository().DeleteMovieDirectors(movie);

            foreach (var director in movie.Directors)
            {
                RepositoryFactory.GetRepository().SetMovieDirectors(movie.IdMovie, director.IdDirector);
            }

            Movies[Movies.IndexOf(movie)] = movie;
        }
    }
}
