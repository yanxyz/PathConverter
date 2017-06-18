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
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace PathConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Paths { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Paths = new ObservableCollection<string>(new string[4]);
            DataContext = this;
            InputLanguageManager.SetInputLanguage(this, System.Globalization.CultureInfo.CreateSpecificCulture("en"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var text = Clipboard.GetText();
            tbPath.Text = text;
        }

        private void tbPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = tbPath.Text.Trim();
            var results = Converter.Convert(input);
            // replace all items of Paths
            int i = 0;
            foreach (var item in results)
            {
                Paths[i] = item;
                ++i;
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/yanxyz/PathConverter/");
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var i = int.Parse(e.Parameter.ToString());
            Copy(Paths[i]);
        }

        private void Copy(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                tbPath.Focus();
                return;
            }

            try
            {
                Clipboard.SetText(text);
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                txtMessage.Text = text;
                txtMessage.SelectAll();
                txtMessage.Copy();
            }

            tbPath.Focus();
        }

        private void tbPath_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!tbPath.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                tbPath.SelectAll();
                tbPath.Focus();
            }
        }
    }
}
