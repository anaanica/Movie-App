using MovieManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovieManager
{
    public class FramedActorPage : Page
    {
        public FramedActorPage(ActorViewModel actorViewModel)
        {
            ActorViewModel = actorViewModel;
        }
        public ActorViewModel ActorViewModel { get; }
        public Frame? Frame { get; set; }
    }
}
