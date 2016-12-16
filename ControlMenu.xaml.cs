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

namespace Image_Viewer
{
    /// <summary>
    /// Логика взаимодействия для ControlMenu.xaml
    /// </summary>
    public partial class ControlMenu : UserControl
    {
        public ControlMenu()
        {
            InitializeComponent();
        }

        public MainWindow Win { get; set; }

       

        private void ClickTrnsformImageLeft(object sender, RoutedEventArgs e)
        {
            Win.TransformImageLeft();
        }

        private void ClickTrnsformImageRight(object sender, RoutedEventArgs e)
        {
            Win.TransformImageRight();
        }

        private void FlipVerticallyClick(object sender, RoutedEventArgs e)
        {
            Win.FlipVertically();
        }

        private void FlipHorizontallyClick(object sender, RoutedEventArgs e)
        {
            Win.FlipHorizontally();
        }

        private void ZoomInClick(object sender, RoutedEventArgs e)
        {
            Win.ZoomIn();
        }

        private void ZoomOutClick(object sender, RoutedEventArgs e)
        {
            Win.ZoomOut();
        }

        private void AspectToFillClick(object sender, RoutedEventArgs e)
        {
            Win.Aspecttofill();
        }

        private void FitClick(object sender, RoutedEventArgs e)
        {
            Win.Fit();
        }

        private void OriginalSizeClick(object sender, RoutedEventArgs e)
        {
            Win.OriginalSize();
        }

        private void ClickNext(object sender, RoutedEventArgs e)
        {
            Win.Next();
        }

        private void ClicPrevious(object sender, RoutedEventArgs e)
        {
            Win.Previous();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Win.HideControlMenu = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Win.HideControlMenu = true;
        }
    }
}
