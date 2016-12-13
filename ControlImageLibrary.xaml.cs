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
                str = str.Remove(str.LastIndexOf("\\"));


                var massImage = Directory.GetFiles(str, "*.*", SearchOption.TopDirectoryOnly)
                     .Where(s => s.EndsWith(".jpg") || s.EndsWith(".bmp") || s.EndsWith(".png"));

                //Bitmap bmp = new Bitmap(openfile.FileName);


                //EXIFextractor exit = new EXIFextractor(ref bmp,"\n");

                //foreach (System.Web.UI.Pair s in exit)
                //{
                //   MessageBox.Show(s.First + " : " + s.Second + "\n");
                //}

                //try { MessageBox.Show(exit["Cell Width"].ToString()); } catch( Exception ex) { MessageBox.Show(ex.Message); }

                //try { MessageBox.Show(exit["Image Width"].ToString()); } catch (Exception ex) { MessageBox.Show(ex.Message); }


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
                        Width = 55,
                        Height = 40,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#dadada"),
                        Child = new System.Windows.Controls.Image()
                        {
                            Width = 50,
                            Height = 30,
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
            (sender as Border).BorderThickness = new Thickness(1, 1, 1, 1);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(0, 0, 0, 0);
        }
    }
}
