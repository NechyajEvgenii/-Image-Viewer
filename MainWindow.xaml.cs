using Goheer.EXIF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
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
            AppDomain.CurrentDomain.AssemblyResolve += AppDomain_AssemblyResolve;
            InitializeComponent();
            HideControlInform = true;
            HideControlLibrary = true;
            HideControlMenu = false;
            LibraryImages.Win = this;
            ControlM.Win = this;
            ImageInf.Win = this;
            AutoSave = false;
        }

        public bool HideControlInform { get; set; }
        public bool HideControlLibrary { get; set; }
        public bool HideControlMenu { get; set; }
        public bool AutoSave { get; set; }

        private static Assembly AppDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("EXIFextractor"))
            {
                Console.WriteLine("Resolving assembly: {0}", args.Name);
                MessageBox.Show("1");
                // Загрузка запакованной сборки из ресурсов, ее распаковка и подстановка
                using (var resource = new MemoryStream(Image_Viewer.Properties.Resources.EXIFextractor))
                using (var reader = new BinaryReader(resource))
                {
                    var one_megabyte = 1024 * 1024;
                    var buffer = reader.ReadBytes(one_megabyte);
                    return Assembly.Load(buffer);
                }
            }
            MessageBox.Show("2");
            return null;
        }

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

                    BitmapImage b = new BitmapImage();
                    b.BeginInit();
                    b.CacheOption = BitmapCacheOption.OnLoad;
                    b.UriSource = new Uri(files[0]);
                    b.EndInit();

                    ImageMain.Source = b;
                    OriginalHeight = b.Height;
                    OriginalWidth = b.Width;
                    CreateInfor(files[0]);
                    LibraryImages.CreatImageLibrary(files[0].Remove(files[0].LastIndexOf("\\")), files[0]);
                }
                catch
                {
                    LibraryImages.CreatImageLibrary(files[0], null);
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

            //string s = path.Remove(0, path.LastIndexOf("\\") + 1);
            ImageInf.TextName.Text = System.IO.Path.GetFileNameWithoutExtension(path);  //s.Remove(s.IndexOf("."));
            ImageInf.TextFormat.Text = System.IO.Path.GetExtension(path);   //  path.Remove(0, path.LastIndexOf(".") + 1);

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

            exit = null;
            bmp = null;
         //   GC.Collect();
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
            if(AutoSave)
            Save();
        }
        public void TransformImageRight()
        {
            ToScale();
            TransformImage.Angle += 90;
            if (AutoSave)
                Save();
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
                if (IndexElem <= 0 )
                {
                    IndexElem = ListImagePath.Count;
                }
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = new Uri(ListImagePath[IndexElem]);
                b.EndInit();

                ImageMain.Source = b;
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


        private void Elapsed(object sender)
        {

            try
            {
                IndexElem++;
                if (IndexElem >= ListImagePath.Count)
                {
                    IndexElem = 0;
                }
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = new Uri(ListImagePath[IndexElem]);
                b.EndInit();

                ImageMain.Source = b;
                CreateInfor(ListImagePath[IndexElem]);


                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(2),
                };
                ImageMain.BeginAnimation(OpacityProperty, animation);
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
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = new Uri(ListImagePath[IndexElem]);
                b.EndInit();

                ImageMain.Source = b;
                CreateInfor(ListImagePath[IndexElem]);

                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.8),
                };
                ImageMain.BeginAnimation(OpacityProperty, animation);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    var sours = ImageMain.Source;
                    var str = ImageMain.Source.ToString();
                    var path = str.Remove(0, str.IndexOf("///") + 3);
                    var angle = Int32.Parse(TransformImage.Angle.ToString());

                    BitmapSource img = (BitmapSource)(sours);
                    CachedBitmap cache = new CachedBitmap(img, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    TransformedBitmap tb = new TransformedBitmap(cache, new RotateTransform(angle));
                    TiffBitmapEncoder encoder = new TiffBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(tb));
                    using (FileStream file = File.OpenWrite(path))
                    {
                        encoder.Save(file);
                    }

                },System.Windows.Threading.DispatcherPriority.Background);
            });
        }

    }
}
