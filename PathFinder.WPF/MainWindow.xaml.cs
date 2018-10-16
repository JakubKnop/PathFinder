using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PathFinder.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void OnKeyDownHandler1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                MainWindowEventHandling.PathWriting(LeftTextBox, LeftList);
            }
        }
        private void OnKeyDownHandler2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                MainWindowEventHandling.PathWriting(RightTextBox, RightList);
            }
        }
        private void LeftList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindowEventHandling.OpenOrProceed(LeftList, LeftTextBox);
        }
        private void RightList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindowEventHandling.OpenOrProceed(RightList, RightTextBox);
        }
        private async void ContextMenuCopyClicked1(object sender, RoutedEventArgs e)
        {
            await MainWindowEventHandling.Copy(LeftList, RightList, RightTextBox, LeftTextBox);
        }
        private async void ContextMenuCopyClicked2(object sender, RoutedEventArgs e)
        {
            await MainWindowEventHandling.Copy(RightList, LeftList, LeftTextBox, RightTextBox);
        }
        private void SortButton1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindowEventHandling.Sort(LeftList, SortButton1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tmp = LeftTextBox.Text.Split('\\').Last().Length ;
            if (LeftTextBox.Text.Length > tmp)
            {
                LeftTextBox.Text = LeftTextBox.Text.Substring(0, LeftTextBox.Text.Length - tmp - 1);
                MainWindowEventHandling.PathWriting(LeftTextBox, LeftList);
            }
            else
            {
                MessageBox.Show("Exception");
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            LeftTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            RightTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            LeftList.ItemsSource = FileController.GetDirectoryContent(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            RightList.ItemsSource = FileController.GetDirectoryContent(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }
    }
}
