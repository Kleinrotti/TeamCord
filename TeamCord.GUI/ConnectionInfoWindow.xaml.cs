using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using TeamCord.Core;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for ConnectionInfoWindow.xaml
    /// </summary>
    public partial class ConnectionInfoWindow : Window
    {
        private BaseConnectionInfo _connectionInfo;

        public ConnectionInfoWindow(BaseConnectionInfo connectionInfo)
        {
            InitializeComponent();
            _connectionInfo = connectionInfo;
            CreateControls();
        }

        private void CreateControls()
        {
            if (_connectionInfo is VoiceConnectionInfo val)
            {
                var properties = typeof(VoiceConnectionInfo).GetProperties().ToList();

                foreach (var v in properties)
                {
                    var labelLeft = new Label();
                    var labelRight = new Label();
                    var attribute = (UnitAttribute)
                        typeof(VoiceConnectionInfo)
                        .GetProperty(v.Name)
                        .GetCustomAttribute(typeof(UnitAttribute));
                    var attributeValue = attribute?.Unit ?? "";
                    labelLeft.Content = v.Name;
                    stackPanelLeft.Children.Add(labelLeft);
                    labelRight.Content = v.GetValue(val) + attributeValue;
                    stackPanelRight.Children.Add(labelRight);
                }
            }
        }
    }
}