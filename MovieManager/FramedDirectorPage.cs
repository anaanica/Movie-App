using MovieManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovieManager
{
    public class FramedDirectorPage :Page
    {
        public FramedDirectorPage(DirectorViewModel directorViewModel)
        {
            DirectorViewModel = directorViewModel;
        }
        public DirectorViewModel DirectorViewModel { get; }
        public Frame? Frame { get; set; }
    }
}
