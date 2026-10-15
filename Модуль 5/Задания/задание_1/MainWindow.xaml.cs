using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace задание_1
{
    public abstract class Figure
    {
        public abstract Shape Draw(double x, double y, double w, double h);
    }

    public class LineFigure : Figure
    {
        public override Shape Draw(double x, double y, double w, double h)
        {
            return new Line
            {
                X1 = x,
                Y1 = y,
                X2 = w,
                Y2 = h,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
        }
    }

    public class CircleFigure : Figure
    {
        public override Shape Draw(double x, double y, double w, double h)
        {
            return new Ellipse
            {
                Width = w,
                Height = h,
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };
        }
    }

    public class SquareFigure : Figure
    {
        public override Shape Draw(double x, double y, double w, double h)
        {
            return new Rectangle
            {
                Width = Math.Min(w, h),
                Height = Math.Min(w, h),
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
        }
    }

    public partial class MainWindow : Window
    {
        private Figure figure;
        private Point start;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            figure = new LineFigure();
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            figure = new CircleFigure();
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            figure = new SquareFigure();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(DrawingCanvas);
        }


        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (figure == null)
                return;

            Point end = e.GetPosition(DrawingCanvas);

            double x = Math.Min(start.X, end.X);
            double y = Math.Min(start.Y, end.Y);
            double w = Math.Abs(end.X - start.X);
            double h = Math.Abs(end.Y - start.Y);

            Shape shape;

            if (figure is LineFigure)
            {
                shape = figure.Draw(start.X, start.Y, end.X, end.Y);
            }
            else
            {
                shape = figure.Draw(x, y, w, h);
                Canvas.SetLeft(shape, x);
                Canvas.SetTop(shape, y);
            }

            DrawingCanvas.Children.Add(shape);
        }
    }
}
