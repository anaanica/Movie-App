using MovieManager.Dal;
using MovieManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.ViewModels
{
    public class DirectorViewModel
    {
        public ObservableCollection<Director> Directors { get; }
        public DirectorViewModel()
        {
            Directors = new ObservableCollection<Director>(RepositoryFactory.GetRepository().GetDirectors());
            Directors.CollectionChanged += Directors_CollectionChanged;
        }

        private void Directors_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddDirector(Directors[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteDirector(e.OldItems!.OfType<Director>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateDirector(e.NewItems!.OfType<Director>().ToList()[0]);
                    break;
            }
        }

        // this is called on Update -> that triggers Replace!
        public void UpdateDirector(Director director) => Directors[Directors.IndexOf(director)] = director;
    }
}
