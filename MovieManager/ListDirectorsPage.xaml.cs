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
    /// Interaction logic for ListDirectorsPage.xaml
    /// </summary>
    public partial class ListDirectorsPage : FramedDirectorPage
    {
        public ListDirectorsPage(DirectorViewModel directorViewModel) : base(directorViewModel)
        {
            InitializeComponent();
            lvDirectors.ItemsSource = directorViewModel.Directors;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Frame?.Navigate(new EditDirectorPage(DirectorViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvDirectors.SelectedItem != null)
            {
                Frame?.Navigate(new EditDirectorPage(DirectorViewModel, lvDirectors.SelectedItem as Director) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvDirectors.SelectedItem != null)
            {
                DirectorViewModel.Directors.Remove((lvDirectors.SelectedItem as Director)!);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame?.NavigationService.GoBack();
    }
}
