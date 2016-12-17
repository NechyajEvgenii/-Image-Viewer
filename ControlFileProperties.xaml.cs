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
    /// Логика взаимодействия для ControlFileProperties.xaml
    /// </summary>
    public partial class ControlFileProperties : UserControl
    {
        public ControlFileProperties()
        {
            InitializeComponent();
        }


        public MainWindow Win { get; set; }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Win.HideControlInform = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Win.HideControlInform = true;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Width >= 240)
            {
                grid.Opacity = 1;
            }

            if (Width <= 180)
            {
                grid.Opacity = 0;
            }
        }

        //public string  TextFormatC  {  set{ TextFormat.Text = value; } }
        //public string TextImageExtensionC { set{ TextImageExtension.Text = value; } }            
        //public string TextPathC { set{ TextPath.Text = value; } }            
        //public string TextDateC{ set{ TextDate.Text = value; }  }            
        //public string TextImageSizeC { set{ TextImageSize.Text = value; } }            
        //public string TextNameC{ set{TextName.Text = value; } }            
        //public string TextTimeC { set{ TextTime.Text = value;  } }           
    }                              
}
