using System.Diagnostics;
using System.Windows;

namespace Задание_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string task = TaskText.Text.Trim();

            if (task != "")
            {
                TaskList.Items.Add("[ ] " + task);
                TaskText.Clear();
            }
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedIndex != -1)
            {
                int index = TaskList.SelectedIndex;
                string task = TaskList.Items[index].ToString();

                if (task.StartsWith("[ ]"))
                {
                    TaskList.Items[index] = "[✓] " + task.Substring(4);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedIndex != -1)
            {
                TaskList.Items.RemoveAt(TaskList.SelectedIndex);
            }
        }
    }
}