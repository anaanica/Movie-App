using Microsoft.Win32;
using MovieManager.Models;
using MovieManager.Utils;
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
    /// Interaction logic for EditActorPage.xaml
    /// </summary>
    public partial class EditActorPage : FramedActorPage
    {
        private readonly Actor? actor;
        public EditActorPage(ActorViewModel actorViewModel, Actor? actor = null) : base(actorViewModel)
        {
            InitializeComponent();
            this.actor = actor ?? new Actor(); // Elvis in action
            DataContext = actor;
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                actor!.ActorName = tbActorName.Text.Trim();
                if (actor.IdActor == 0)
                {
                    ActorViewModel.Actors.Add(actor);
                }
                else
                {
                    ActorViewModel.UpdateActor(actor);
                }
                Frame?.NavigationService.GoBack();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame?.NavigationService.GoBack();

        private bool FormValid()
        {
            bool valid = true;
            gridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                e.Background = Brushes.White;
                if (string.IsNullOrEmpty(e.Text.Trim()))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
            });

            return valid;
        }
    }
}
