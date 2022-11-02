using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TeamCord.Core;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for ChannelConnector.xaml
    /// </summary>
    public partial class ChannelConnector : Window
    {
        private IList<TCServer> _servers;
        private Action<string, TCChannel> _resultCallback;

        public ChannelConnector(IList<TCServer> servers, Action<string, TCChannel> resultCallback)
        {
            _servers = servers;
            _resultCallback = resultCallback;
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            foreach (var v in _servers)
            {
                var mItem = new TreeViewItem();
                mItem.Header = v.Name;
                mItem.DataContext = v;
                foreach (var x in v.VoiceChannels)
                {
                    var subItem = new RadioButton
                    {
                        DataContext = x,
                        Content = x.Name,
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
                        //var id = ((TCChannel)x.DataContext).Id;
                        _resultCallback(((TCServer)v.DataContext).Name, (TCChannel)x.DataContext);
                        Close();
                        return;
                    }
                }
            }
            Close();
        }
    }
}