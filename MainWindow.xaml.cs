using Goheer.EXIF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LibraryImages.Win = this;
            ControlM.Win = this;
            ImageInf.Win = this;
            HideControlInform = true;
            HideControlLibrary = true;
            HideControlMenu = true;
        }

        private void ShowControlLibrary(object sender, MouseEventArgs e)
        {
            LibraryImages.Width = 300;
        }


        public System.Windows.Controls.Image ImageMainS
        {
            get { return ImageMain; }
            set
            {
                ImageMain = value;
            }
        }


        public double OriginalHeight { get; set; }
        public double OriginalWidth { get; set; }


        private void ImageMain_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    var b = new BitmapImage(new Uri(files[0]));
                    ImageMain.Source = b;
                    OriginalHeight = b.Height;
                    OriginalWidth = b.Width;
                    CreateInfor(files[0]);
                    LibraryImages.CreatImageLibrary(files[0].Remove(files[0].LastIndexOf("\\")));
                    IndexElem = ListImagePath.IndexOf(files[0]);
                }
                catch
                {
                    LibraryImages.CreatImageLibrary(files[0]);
                }

            }

        }

        public int IndexElem { get; set; }
        public List<string> ListImagePath { get; set; }


        public void CreateInfor(string path)
        {
            ToScale();
            TransformImage.Angle = 0;
            Bitmap bmp = new Bitmap(path);

            EXIFextractor exit = new EXIFextractor(ref bmp, "\n");

            ImageInf.TextPath.Text = path;

            string s = path.Remove(0, path.LastIndexOf("\\") + 1);
            ImageInf.TextName.Text = s.Remove(s.IndexOf("."));
            ImageInf.TextFormat.Text = path.Remove(0, path.LastIndexOf(".") + 1);
            ImageInf.TextImageExtension.Text = $"{bmp.Width}x{bmp.Height}";

            var f = bmp.PropertyItems;


            try
            {
                var st = exit["Date Time"].ToString().Split(' ');
                ImageInf.TextTime.Text = st[1].Substring(0, st[1].Length - 1);
                ImageInf.TextDate.Text = st[0];
            }
            catch
            {
                ImageInf.TextTime.Text = null;
                ImageInf.TextDate.Text = null;
            }

        }

        private void ShowControlInform(object sender, MouseEventArgs e)
        {
            ImageInf.Width = 250;
        }

        private void HideControl(object sender, MouseEventArgs e)
        {
            if (HideControlInform)
                ImageInf.Width = 0;

            if (HideControlLibrary)
                LibraryImages.Width = 0;

            if  (HideControlMenu)
                ControlM.Height = 0;
        }

        private void ToScale()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;
            ImageMain.Width = window.Width;
            ImageMain.Height = window.Height;

        }

        public void TransformImageLeft()
        {
            ToScale();
            TransformImage.Angle -= 90;
        }
        public void TransformImageRight()
        {
            ToScale();
            TransformImage.Angle += 90;
        }

        public void FlipVertically()
        {
            ToScale();
            TransformImage.Angle = 90;
        }

        public void FlipHorizontally()
        {
            ToScale();
            TransformImage.Angle = 180;
        }

        public void Fit()
        {
            ToScale();
            ImageMain.Stretch = Stretch.Uniform;
        }
        public void Aspecttofill()
        {
            ToScale();
            ImageMain.Stretch = Stretch.Fill;
        }

        public void ZoomOut()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);
            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = ScaleTr.ScaleX <= 0.2 ? 0.1 : ScaleTr.ScaleX-0.1;
            ScaleTr.ScaleY = ScaleTr.ScaleY <= 0.2 ? 0.1 : ScaleTr.ScaleY - 0.1;
        }

        public void ZoomIn()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX += 0.1;
            ScaleTr.ScaleY += 0.1;
        }

        public void OriginalSize()
        {

            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;
            ImageMain.Width = OriginalWidth;
            ImageMain.Height = OriginalHeight;
        }


        public void Previous()
        {
            try
            {
                IndexElem--;
                if (IndexElem <= 0)
                {
                    IndexElem = ListImagePath.Count - 1;
                }
                ImageMain.Source = new BitmapImage(new Uri(ListImagePath[IndexElem]));
                CreateInfor(ListImagePath[IndexElem]);
            }
            catch { }
        }
        public void Next()
        {
            try
            {
                IndexElem++;
                if (IndexElem >= ListImagePath.Count)
                {
                    IndexElem = 0;
                }
                ImageMain.Source = new BitmapImage(new Uri(ListImagePath[IndexElem]));
                CreateInfor(ListImagePath[IndexElem]);
            }
            catch { }
        }


        public bool HideControlInform { get; set; }
        public bool HideControlLibrary { get; set; }
        public bool HideControlMenu { get; set; }

        private void ShowControlMenu(object sender, MouseEventArgs e)
        {
            ControlM.Height = 80;
        }
    }
}
