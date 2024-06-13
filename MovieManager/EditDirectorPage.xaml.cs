using MovieManager.Models;
using MovieManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for EditDirectorPage.xaml
    /// </summary>
    public partial class EditDirectorPage : FramedDirectorPage
    {
        private readonly Director? director;
        public EditDirectorPage(DirectorViewModel directorViewModel, Director? director = null)
            : base(directorViewModel)
        {
            InitializeComponent();
            this.director = director ?? new Director(); // Elvis in action
            DataContext = director;
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                director!.DirectorName = tbDirectorName.Text.Trim();
                if (director.IdDirector == 0)
                {
                    DirectorViewModel.Directors.Add(director);
                }
                else
                {
                    DirectorViewModel.UpdateDirector(director);
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
