﻿using Microsoft.Win32;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LibraryImages.Win = this;

        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            LibraryImages.Width = 252;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            LibraryImages.Width = 0;
        }

        public Image ImageMainS { get { return ImageMain; } set { ImageMain = value; } }




        private void Border_MouseEnter_1(object sender, MouseEventArgs e)
        {
            MessageBox.Show("sdf");
        }

        private void ImageMain_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    ImageMain.Source = new BitmapImage(new Uri(files[0]));

                    LibraryImages.CreatImageLibrary(files[0].Remove(files[0].LastIndexOf("\\")) );

                }
                catch 
                {
                    LibraryImages.CreatImageLibrary(files[0]);
                }

            }

        }
    }
}
