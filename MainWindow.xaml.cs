using Goheer.EXIF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            LibraryImages.Width = 252;
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


        private void Border_MouseEnter_1(object sender, MouseEventArgs e)
        {
            MessageBox.Show("sdf");
        }

        private void ImageMain_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {

                    var b= new BitmapImage(new Uri(files[0]));
                    ImageMain.Source = b;
                    OriginalHeight = b.Height;
                    OriginalWidth = b.Width;
                    CreateInfor(files[0]);
                    LibraryImages.CreatImageLibrary(files[0].Remove(files[0].LastIndexOf("\\")));
                   
                }
                catch
                {
                    LibraryImages.CreatImageLibrary(files[0]);
                }

            }

        }

        public void CreateInfor(string path)
        {

            Bitmap bmp = new Bitmap(path);

            EXIFextractor exit = new EXIFextractor(ref bmp, "\n");

            ImageInf.TextPath.Text = path;

            string s = path.Remove(0, path.LastIndexOf("\\") + 1);
            ImageInf.TextName.Text = s.Remove(s.IndexOf("."));
            ImageInf.TextFormat.Text = path.Remove(0, path.LastIndexOf(".") + 1);
            ImageInf.TextImageExtension.Text = $"{bmp.Width}x{bmp.Height}";


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

            //try { MessageBox.Show(exit["Image Width"].ToString()); } catch (Exception ex) { MessageBox.Show(ex.Message); }



            //MessageBox.Show(TmpImgEXIF.DateTaken.ToString());

            //BitmapDecoder d = BitmapDecoder.Create(new Uri(openfile.FileName), BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            //InPlaceBitmapMetadataWriter inplace = d.Frames[0].CreateInPlaceBitmapMetadataWriter();
            //  MessageBox.Show(i..ToString());

            //  MessageBox.Show((massImage.Count).ToString());

        }

        private void Border_MouseEnter2(object sender, MouseEventArgs e)
        {
            ImageInf.Width = 230;
        }

        private void ImageMain_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageInf.Width = 0;
            LibraryImages.Width = 0;
        }


        public void TransformImageLeft()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;
            ImageMain.Width = window.Width;
            ImageMain.Height = window.Height;
            TransformImage.Angle -= 90;
        }
        public void TransformImageRight()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;

            ImageMain.Width=window.Width;
            ImageMain.Height=window.Height;
            TransformImage.Angle += 90; }

        public void FlipVertically()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;
            ImageMain.Width = window.Width;
            ImageMain.Height = window.Height;
            TransformImage.Angle = 90;
        }

        public void FlipHorizontally()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;
            ImageMain.Width = window.Width;
            ImageMain.Height = window.Height;
            TransformImage.Angle = 180;
        }

        public void Fit()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;
            ImageMain.Width = window.Width;
            ImageMain.Height = window.Height;
            ImageMain.Stretch = Stretch.Uniform;
        }
        public void Aspecttofill() {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);

            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX = 1;
            ScaleTr.ScaleY = 1;
            ImageMain.Width = window.Width;
            ImageMain.Height = window.Height;
            ImageMain.Stretch = Stretch.Fill; }

        public void ZoomOut()
        {
            Scrol.ScrollToHorizontalOffset(window.Width / 2);
            Scrol.ScrollToVerticalOffset(window.Height / 2);
            ScaleTr.CenterX = window.Width / 2;
            ScaleTr.CenterY = window.Height / 2;
            ScaleTr.ScaleX -= 0.1;
            ScaleTr.ScaleY -= 0.1;
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

        }
        public void Next()
        {
            System.Windows.Controls.Image tmp =null;

            for (int i = 0; i < LibraryImages.ListIm.Count; i++)
            {
                try
                {
                    tmp = (LibraryImages.ListIm[i] as System.Windows.Controls.Image);
                if (tmp.Source.ToString() == ImageMain.Source.ToString())
                {
                    }
                    ImageMain.Source = (LibraryImages.ListIm[i + 1] as System.Windows.Controls.Image).Source;
                    } catch { }
            } 
        }
    }





}
