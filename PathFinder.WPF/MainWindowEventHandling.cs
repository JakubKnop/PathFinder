using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PathFinder.WPF
{
    public class MainWindowEventHandling
    {
        public static async Task Copy(ListView from, ListView to, TextBox rtb, TextBox ltb)
        {
            if (ltb.Text != rtb.Text)
            {
                if (from.SelectedItems.Count == 0)
                {
                    FileOnList ParentFile = new FileOnList(ltb.Text);
                    Window progressBarWindow = new Window();
                    progressBarWindow.Width = 300;
                    progressBarWindow.Height = 100;
                    progressBarWindow.Name = "Copying";
                    ProgressBar progressBar = new ProgressBar();
                    progressBarWindow.Content = progressBar;
                    progressBarWindow.Show();
                    progressBar.Minimum = 0;
                    progressBar.Maximum = ParentFile.Size;
                    List<string> PathList = new List<string>();
                    foreach (object obj in from.ItemsSource)
                    {
                        if (obj is FileOnList)
                        {
                            progressBar.Value++;
                            FileOnList fol = obj as FileOnList;
                            PathList.Add(fol.FullPath);
                        }
                        await FileController.CopyAsync(PathList, rtb.Text);
                        to.Items.Refresh();
                        from.Items.Refresh();
                    }
                    progressBarWindow.Close();
                }
                else
                {
                    List<string> PathList = new List<string>();
                    foreach (object obj in from.SelectedItems)
                    {
                        if (obj is FileOnList)
                        {
                            FileOnList fol = obj as FileOnList;
                            PathList.Add(fol.FullPath);
                        }
                        await FileController.CopyAsync(PathList, rtb.Text);
                        to.Items.Refresh();
                        from.Items.Refresh();
                    }
                }
                MessageBox.Show("file copied");
            }
        }
        public static void Sort(ListView lv, ComboBox cb)
        {
            if (cb.SelectedIndex == 0)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv.ItemsSource);
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
            }
            else if (cb.SelectedIndex == 1)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv.ItemsSource);
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Size", ListSortDirection.Descending));
            }
        }
        public static void PathWriting(TextBox tb, ListView lv)
        {
            try
            {
                lv.ItemsSource = FileController.GetDirectoryContent(tb.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Acces denied/ Wrong path");
                tb.Text = "";
            }
        }
        public static void OpenOrProceed(ListView lv, TextBox tb)
        {
            if (lv.SelectedItems.Count == 1)
            {
                if (lv.SelectedItem is FileOnList)
                {
                    FileOnList a = (FileOnList)lv.SelectedItem;
                    FileOnList k = new FileOnList(a.FullPath);
                    if (k.IsDirectory == true)
                    {
                        lv.ItemsSource = FileController.GetDirectoryContent(k.FullPath);
                        tb.Text = k.FullPath;
                    }
                    else if (k.IsDirectory == false)
                    {
                        Process.Start("notepad.exe", k.FullPath);
                    }
                }
            }
        }
    }
}
