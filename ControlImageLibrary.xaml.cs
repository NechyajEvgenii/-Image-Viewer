using Goheer.EXIF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
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


        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openfile = new OpenFileDialog()
        //    {
        //        Filter = "(*.BMP;*.JPG;*.GIF,*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"
        //    };
        //    if (openfile.ShowDialog() == true)
        //    {
        //        string str = openfile.FileName;

        //        var b = new BitmapImage(new Uri(str));
        //        this.Win.ImageMainS.Source = b;

        //        Win.OriginalHeight = b.Height;
        //        Win.OriginalWidth = b.Width;

        //        Win.CreateInfor(str);
        //        str = str.Remove(str.LastIndexOf("\\"));
        //        CreatImageLibrary(str);

        //        Win.IndexElem = Win.ListImagePath.IndexOf(str);
        //    }
        // }


        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var tmp = (sender as Border);
            tmp.BorderThickness = new Thickness(1, 1, 1, 1);
            var tmpIm = (tmp.Child as System.Windows.Controls.Image);

            var s = tmpIm.Source.ToString();
            imageName.Text = System.IO.Path.GetFileNameWithoutExtension(s);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(0, 0, 0, 0);
            imageName.Text = null;
        }

        static List<string> listimage;


        public void CreatImageLibrary(string pathfolfer, string pathfile)
        {

            ProgresBar1.Visibility = Visibility.Visible;

            WrapPan.Children.Clear();

            listimage = new List<string>();

            IEnumerable<string> massImage = null;
            new Thread(() =>
{

    try
    {
        massImage = Directory.GetFiles(pathfolfer, "*.*", SearchOption.TopDirectoryOnly)
               .Where(s => s.ToLower().EndsWith(".jpg")
               || s.ToLower().EndsWith(".bmp")
               || s.ToLower().EndsWith(".png"));

        if (massImage == null)
        {
            massImage = Directory.GetFiles(System.IO.Path.GetDirectoryName(pathfolfer), "*.*", SearchOption.TopDirectoryOnly)
              .Where(s => s.ToLower().EndsWith(".jpg")
              || s.ToLower().EndsWith(".bmp")
              || s.ToLower().EndsWith(".png"));
        }
    }
    catch { }
    if (massImage != null)
        foreach (var item in massImage)
        {

            Dispatcher.Invoke(() =>
        {
            try
            {
                listimage.Add(item);

                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.UriSource = new Uri(item);
                source.CacheOption = BitmapCacheOption.OnLoad;
                source.DecodePixelHeight = 130;
                source.DecodePixelWidth = 80;
                source.EndInit();

                var border = new Border()
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
                        Source = source
                    }

                };
                WrapPan.Children.Add(border);
                Win.ListImagePath = listimage;

            }
            catch { }
        }, System.Windows.Threading.DispatcherPriority.Background);
        }

    Dispatcher.Invoke(() =>
        {
            if (pathfile != null)
                Win.IndexElem = listimage.IndexOf(pathfile);
            ProgresBar1.Visibility = Visibility.Collapsed;
        }, System.Windows.Threading.DispatcherPriority.Background);



}).Start();
        }


        public MainWindow Win { get; set; }

        private void Thumb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tmp = (sender as Border).Child;

            var tmpIm = (tmp as System.Windows.Controls.Image).Source as BitmapImage;

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.CacheOption = BitmapCacheOption.OnLoad;
            logo.UriSource = tmpIm.UriSource;
            logo.EndInit();


            // Dispatcher.Invoke((Action)delegate
            //{
            Win.ImageMain.Source = logo;
            //  }, System.Windows.Threading.DispatcherPriority.Background);

            Win.IndexElem = Win.ListImagePath.IndexOf(logo.ToString());

            Win.CreateInfor(logo.ToString().Remove(0, 8));
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
