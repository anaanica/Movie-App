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
    /// Interaction logic for ListActorsPage.xaml
    /// </summary>
    public partial class ListActorsPage : FramedActorPage
    {
        public ListActorsPage(ActorViewModel actorViewModel) : base(actorViewModel)
        {
            InitializeComponent();
            lvActors.ItemsSource = actorViewModel.Actors;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Frame?.Navigate(new EditActorPage(ActorViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvActors.SelectedItem != null)
            {
                Frame?.Navigate(new EditActorPage(ActorViewModel, lvActors.SelectedItem as Actor) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvActors.SelectedItem != null)
            {
                ActorViewModel.Actors.Remove((lvActors.SelectedItem as Actor)!);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame?.NavigationService.GoBack();
    }
}
