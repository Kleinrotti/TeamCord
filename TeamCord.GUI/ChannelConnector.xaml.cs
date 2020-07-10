using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for ChannelConnector.xaml
    /// </summary>
    public partial class ChannelConnector : Window
    {
        private IDictionary<string, IDictionary<ulong, string>> _channels;
        private Action<ulong> _resultCallback;

        public ChannelConnector(IDictionary<string, IDictionary<ulong, string>> channels, Action<ulong> resultCallback)
        {
            _channels = channels;
            _resultCallback = resultCallback;
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            foreach (var v in _channels)
            {
                var mItem = new TreeViewItem();
                mItem.Header = v.Key;
                foreach (var x in v.Value)
                {
                    var subItem = new RadioButton
                    {
                        DataContext = x,
                        Content = x.Value,
                        GroupName = "channels"
                    };
                    mItem.Items.Add(subItem);
                }
                treeView_items.Items.Add(mItem);
            }
        }

        private void button_Apply_Click(object sender, RoutedEventArgs e)
        {
            var test = treeView_items.SelectedItem;
            //search for the checked radiobutton to return the channel id via callback
            foreach (TreeViewItem v in treeView_items.Items)
            {
                foreach (RadioButton x in v.Items)
                {
                    if (x.IsChecked ?? false)
                    {
                        var id = ((KeyValuePair<ulong, string>)x.DataContext).Key;
                        _resultCallback(id);
                        Close();
                        return;
                    }
                }
            }
            Close();
        }
    }
}