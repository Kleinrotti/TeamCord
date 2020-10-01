using System;
using System.Windows;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for TotpWindow.xaml
    /// </summary>
    public partial class TotpWindow : Window
    {
        private Action<string> _totpCallback;

        public TotpWindow(Action<string> totpCallback)
        {
            InitializeComponent();
            _totpCallback = totpCallback;
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_Code.Text != "")
            {
                _totpCallback(textBox_Code.Text);
                Close();
            }
        }
    }
}