using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ControlImageLibrary.xaml
    /// </summary>
    public partial class ControlImageLibrary : UserControl
    {
        public ControlImageLibrary()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog()
            {
                Filter = "(*.BMP;*.JPG;*.GIF,*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"
            };
            if (openfile.ShowDialog() == true)
            {
                string str = openfile.FileName;
                str = str.Remove(str.LastIndexOf("\\"));


                var massImage = Directory.GetFiles(str, "*.*", SearchOption.TopDirectoryOnly)
                     .Where(s => s.EndsWith(".jpg") || s.EndsWith(".bmp") || s.EndsWith(".png"));


                //BitmapDecoder decoder = BitmapDecoder.Create(new Uri(openfile.FileName), BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default); //"распаковали" снимок и создали объект decoder
                //BitmapMetadata TmpImgEXIF = (BitmapMetadata)decoder.Frames[0].Metadata;

                //MessageBox.Show(TmpImgEXIF.DateTaken.ToString());

                //BitmapDecoder d = BitmapDecoder.Create(new Uri(openfile.FileName), BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                //InPlaceBitmapMetadataWriter inplace = d.Frames[0].CreateInPlaceBitmapMetadataWriter();
                //  MessageBox.Show(i..ToString());



                foreach (var item in massImage)
                {

                    var im = new Border()
                    {
                        Width = 130,
                        Height = 100,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        BorderBrush = (Brush)new BrushConverter().ConvertFromString("#dadada"),
                        Child = new Image()
                        {
                            Width = 120,
                            Height = 70,
                            Stretch = Stretch.Fill,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Source = new BitmapImage(new Uri(item))
                        }
                    };
                    WrapPan.Children.Add(im);

                }

                //  MessageBox.Show((massImage.Count).ToString());

            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(2, 2, 2, 2);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(0, 0, 0, 0);
        }
    }
}
