using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TeamCord.Core;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for VolumeControl.xaml
    /// </summary>
    public partial class VolumeControl : Window
    {
        public event EventHandler<UserVolume> VolumeChanged;

        private IList<UserVolume> _userList;
        private ICollection<Slider> _sliders;

        public VolumeControl(IList<UserVolume> userList)
        {
            InitializeComponent();
            _userList = userList;
            Loaded += VolumeControl_Loaded;
            CreateSliders();
        }

        private void VolumeControl_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;
        }

        private void CreateSliders()
        {
            if (_userList.Count < 1)
            {
                textBlock_noUsers.Visibility = Visibility.Visible;
                return;
            }
            _sliders = new List<Slider>();
            foreach (var v in _userList)
            {
                var pnl = new StackPanel();
                var sl = new Slider
                {
                    Minimum = 0,
                    Maximum = 1.5,
                    Width = 50,
                    Height = 180,
                    Orientation = Orientation.Vertical,
                    Value = v.Volume,
                    AutoToolTipPlacement = AutoToolTipPlacement.TopLeft,
                    AutoToolTipPrecision = 1,
                    DataContext = v,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                sl.ValueChanged += Sl_ValueChanged;
                var txt = new Label
                {
                    Content = v.Username,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                _sliders.Add(sl);
                pnl.Children.Add(sl);
                pnl.Children.Add(txt);
                stackPanelSliders.Children.Add(pnl);
            }
        }

        private void Sl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider)e.Source;
            var vol = (UserVolume)slider.DataContext;
            var newVol = new UserVolume(vol.UserID, (float)slider.Value);
            VolumeChanged?.Invoke(sender, newVol);
        }
    }
}