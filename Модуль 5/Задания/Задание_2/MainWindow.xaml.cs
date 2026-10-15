using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace задание_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                TextEditor.Text = File.ReadAllText(
                    dialog.FileName, Encoding.UTF8);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            dialog.DefaultExt = ".txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(
                    dialog.FileName, TextEditor.Text, Encoding.UTF8);

                MessageBox.Show("Файл сохранён!");
            }
        }
    }
}
