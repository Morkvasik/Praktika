using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Задание_4
{
    public partial class MainWindow : Window
    {
        double zoom = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Картинки|*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage(new Uri(dialog.FileName));

                Picture.Source = img;
                Picture.Width = img.PixelWidth;
                Picture.Height = img.PixelHeight;

                zoom = 1;
                Picture.LayoutTransform = new ScaleTransform(zoom, zoom);
                ZoomText.Text = "Масштаб: 100%";
            }
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            zoom = Math.Round(zoom + 0.2, 1);

            Picture.LayoutTransform = new ScaleTransform(zoom, zoom);
            ZoomText.Text = "Масштаб: " + (zoom * 100).ToString("0") + "%";
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (zoom > 0.2)
            {
                zoom = Math.Round(zoom - 0.2, 1);

                Picture.LayoutTransform = new ScaleTransform(zoom, zoom);
                ZoomText.Text = "Масштаб: " + (zoom * 100).ToString("0") + "%";
            }
        }
    }
}