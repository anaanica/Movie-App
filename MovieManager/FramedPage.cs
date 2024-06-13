using MovieManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovieManager
{
    public class FramedPage : Page
    {
        public FramedPage(MovieViewModel movieViewModel, ActorViewModel actorViewModel,
            DirectorViewModel directorViewModel)
        {
            MovieViewModel = movieViewModel;
            ActorViewModel = actorViewModel;
            DirectorViewModel = directorViewModel;
        }
        public MovieViewModel MovieViewModel { get; }
        public ActorViewModel ActorViewModel { get; }
        public DirectorViewModel DirectorViewModel { get; }
        public Frame? Frame { get; set; }
    }
}
