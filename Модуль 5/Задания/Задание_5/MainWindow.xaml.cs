using System;
using System.Windows;
using System.Windows.Controls;

namespace Задание_5
{
    public partial class MainWindow : Window
    {
        double a = 0;
        string znak = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string s = (sender as Button).Content.ToString();

            if (Display.Text == "0")
                Display.Text = s;
            else
                Display.Text += s;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            a = Convert.ToDouble(Display.Text);
            znak = (sender as Button).Content.ToString();
            Display.Text = "0";
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double b = Convert.ToDouble(Display.Text);
            double result = 0;

            if (znak == "+")
                result = a + b;
            else if (znak == "-")
                result = a - b;
            else if (znak == "*")
                result = a * b;
            else if (znak == "/")
            {
                if (b == 0)
                {
                    Display.Text = "Ошибка";
                    znak = "";
                    return;
                }

                result = a / b;
            }

            Display.Text = result.ToString();
            znak = "";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            a = 0;
            znak = "";
        }
    }
}