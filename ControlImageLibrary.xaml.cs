using Goheer.EXIF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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

                var b = new BitmapImage(new Uri(str));
                this.Win.ImageMainS.Source = b;

                Win.OriginalHeight = b.Height;
                Win.OriginalWidth = b.Width;

                Win.CreateInfor(str);
                str = str.Remove(str.LastIndexOf("\\"));
                CreatImageLibrary(str);

                Win.IndexElem = Win.ListImagePath.IndexOf(str);
            }
        }


        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var tmp = (sender as Border);
            tmp.BorderThickness = new Thickness(1, 1, 1, 1);
            var tmpIm = (tmp.Child as System.Windows.Controls.Image);

            var s = tmpIm.Source.ToString();
            var str = s.Remove(0, s.LastIndexOf(@"/") + 1);
            imageName.Text = str.Split('.')[0];
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(0, 0, 0, 0);
            imageName.Text = null;
        }

        List<string> listimage;

        public void CreatImageLibrary(string str)
        {
            listimage = new List<string>();
            try
            {
                var massImage = Directory.GetFiles(str, "*.*", SearchOption.TopDirectoryOnly)
                        .Where(s => s.EndsWith(".jpg")
                        || s.EndsWith(".bmp")
                        || s.EndsWith(".png")
                        || s.EndsWith(".JPG")
                        );

                WrapPan.Children.Clear();

                foreach (var item in massImage)
                {
                    listimage.Add(item);
                    var im = new Border()
                    {
                        Width = 70,
                        Height = 50,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#dadada"),
                        Child = new System.Windows.Controls.Image()
                        {
                            Width = 65,
                            Height = 40,
                            Stretch = Stretch.Fill,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Source = new BitmapImage(new Uri(item))
                        }
                    };

                    WrapPan.Children.Add(im);
                }
                Win.ListImagePath = listimage;
            }
            catch { }
        }


        public MainWindow Win { get; set; }

        private void Thumb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tmp = (sender as Border).Child;

            var tmpIm = (tmp as System.Windows.Controls.Image);

            Win.ImageMain.Source = tmpIm.Source;

            Win.IndexElem = Win.ListImagePath.IndexOf(tmpIm.Source.ToString());

            Win.CreateInfor(tmpIm.Source.ToString().Remove(0,8));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Win.HideControlLibrary = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Win.HideControlLibrary = true;
        }
    }


}
