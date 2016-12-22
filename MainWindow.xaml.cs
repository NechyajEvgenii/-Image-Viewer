using Goheer.EXIF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using System.Windows.Media.Animation;
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
            HideControlInform = true;
            HideControlLibrary = true;
            HideControlMenu = false;
            LibraryImages.Win = this;
            ControlM.Win = this;
            ImageInf.Win = this;

        }

        public bool HideControlInform { get; set; }
        public bool HideControlLibrary { get; set; }
        public bool HideControlMenu { get; set; }


        private void ShowControlLibrary(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                //  From = 0,
                To = 300,
                Duration = TimeSpan.FromSeconds(0.4),
            };
            LibraryImages.BeginAnimation(WidthProperty, animation);
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
                    BitmapImage b = new BitmapImage(new Uri(files[0]));
                    ImageMain.Source = b;
                    OriginalHeight = b.Height;
                    OriginalWidth = b.Width;
                    CreateInfor(files[0]);
                    LibraryImages.CreatImageLibrary(files[0].Remove(files[0].LastIndexOf("\\")), files[0]);
                }
                catch 
                {
                    LibraryImages.CreatImageLibrary(files[0],null);
                }
            }

        }

        public int IndexElem { get; set; }
        public  List<string> ListImagePath { get; set; }


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
            DoubleAnimation animation = new DoubleAnimation()
            {
                //  From = 0,
                To = 250,
                Duration = TimeSpan.FromSeconds(0.4),
            };

            ImageInf.BeginAnimation(WidthProperty, animation);
        }

        private void HideControl(object sender, MouseEventArgs e)
        {

            if (HideControlMenu)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.4),
                };
                ControlM.BeginAnimation(HeightProperty, animation);
            }

            if (HideControlInform)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.4),
                };
                ImageInf.BeginAnimation(WidthProperty, animation);
            }

            if (HideControlLibrary)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.4),
                };
                LibraryImages.BeginAnimation(WidthProperty, animation);
            }
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
            ScaleTr.ScaleX = ScaleTr.ScaleX <= 0.2 ? 0.1 : ScaleTr.ScaleX - 0.1;
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

                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.8),
                };
                ImageMain.BeginAnimation(OpacityProperty, animation);
            }
            catch { }
        }

        int i;

        private void Elapsed(object sender)
        {

            Save();
            try
            {
                IndexElem++;
                if (i >= ListImagePath.Count)
                {
                    i = 0;
                }
                ImageMain.Source = new BitmapImage(new Uri(ListImagePath[i]));
                CreateInfor(ListImagePath[i]);

                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(2),
                };
                ImageMain.BeginAnimation(OpacityProperty, animation);
                i++;
            }
            catch { }

        }

        public void Next()
        {
            try
            {
                Save();
                IndexElem++;
                if (IndexElem >= ListImagePath.Count)
                {
                    IndexElem = 0;
                }
                ImageMain.Source = new BitmapImage(new Uri(ListImagePath[IndexElem]));
                CreateInfor(ListImagePath[IndexElem]);

                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.8),
                };
                ImageMain.BeginAnimation(OpacityProperty, animation);

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }




        private void ShowControlMenu(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                //  From = 0,
                To = 80,
                Duration = TimeSpan.FromSeconds(0.4),
            };
            ControlM.BeginAnimation(HeightProperty, animation);
        }

        Timer timer;

        public void SlideShowStart()
        {
            timer = new Timer(Elapsed, null, 5000, 5000);

        }
        public void SlideShowStop()
        {
            timer.Dispose();
        }


        private void Save()
        {
            //var str= ImageMain.Source.ToString();
            //var path = str.Remove(0, str.IndexOf("///") + 3); 
            //var angle = Int32.Parse(TransformImage.Angle.ToString());
            //Bitmap bitm = new Bitmap(path);
            
            //while (angle > 360)
            //{
            //    angle -= 360;
            //}
          
            //switch (angle / 90)
            //{
            //    case 1:
            //        bitm.RotateFlip(RotateFlipType.Rotate90FlipX);
            //        break;
            //    case 2:
            //        bitm.RotateFlip(RotateFlipType.Rotate180FlipX);
            //        break;
            //    case 3:
            //        bitm.RotateFlip(RotateFlipType.Rotate270FlipX);
            //        break;
            //    default:
            //        bitm.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            //        break;
            //}

            //bitm.Save(path);
        }

        //    private void HideControlMenuM(object sender, MouseEventArgs e)
        //    {
        //        if (HideControlMenu)
        //        {
        //            DoubleAnimation animation = new DoubleAnimation()
        //            {
        //                To = 0,
        //                Duration = TimeSpan.FromSeconds(0.2),
        //            };
        //            ControlM.BeginAnimation(HeightProperty, animation);
        //        }
        //    }

        //    private void HideControlFileInform(object sender, MouseEventArgs e)
        //    {
        //        if (HideControlInform)
        //        {
        //            DoubleAnimation animation = new DoubleAnimation()
        //            {
        //                To = 0,
        //                Duration = TimeSpan.FromSeconds(0.2),
        //            };
        //            ImageInf.BeginAnimation(WidthProperty, animation);
        //        }
        //    }

        //    private void HideControlLibraryM(object sender, MouseEventArgs e)
        //    {
        //        if (HideControlLibrary)
        //        {
        //            DoubleAnimation animation = new DoubleAnimation()
        //            {
        //                To = 0,
        //                Duration = TimeSpan.FromSeconds(0.4),
        //            };
        //            LibraryImages.BeginAnimation(WidthProperty, animation);
        //        }
        //    }
    }
}
