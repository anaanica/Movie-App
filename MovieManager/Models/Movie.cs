using MovieManager.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MovieManager.Models
{
    public class Movie
    {
        public int IdMovie { get; set; }
        public string? Title { get; set; }
        public string? MovieDescription { get; set; }
        public string? Duration { get; set; }
        public string? Link { get; set; }
        public byte[]? Picture { get; set; }
        public BitmapImage Image 
        { 
            get => ImageUtils.ByteArrayToBitmapImage(Picture!); 
        }
        public ObservableCollection<Actor>? Actors { get; set; }
        public ObservableCollection<Director>? Directors { get; set; }
    }
}
