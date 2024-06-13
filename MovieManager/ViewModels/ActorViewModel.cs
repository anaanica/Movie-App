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
    public class ActorViewModel
    {
        public ObservableCollection<Actor> Actors { get; }
        public ActorViewModel()
        {
            Actors = new ObservableCollection<Actor>(RepositoryFactory.GetRepository().GetActors());
            Actors.CollectionChanged += Actors_CollectionChanged;
        }

        private void Actors_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddActor(Actors[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteActor(e.OldItems!.OfType<Actor>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateActor(e.NewItems!.OfType<Actor>().ToList()[0]);
                    break;
            }
        }

        // this is called on Update -> that triggers Replace!
        public void UpdateActor(Actor actor) => Actors[Actors.IndexOf(actor)] = actor;
    }
}
