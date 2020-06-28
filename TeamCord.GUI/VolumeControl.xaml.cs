using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for VolumeControl.xaml
    /// </summary>
    public partial class VolumeControl : Window
    {
        public event EventHandler<Tuple<float, ulong>> VolumeChanged;

        private IList<Tuple<float, ulong>> _userList;
        private IList<Slider> _sliders;

        public VolumeControl(IList<Tuple<float, ulong>> userList)
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
            _sliders = new List<Slider>();
            foreach (var v in _userList)
            {
                var sl = new Slider
                {
                    Minimum = 0,
                    Maximum = 1.0,
                    Width = 50,
                    Height = 100,
                    Orientation = Orientation.Vertical,
                    Value = v.Item1,
                    AutoToolTipPlacement = AutoToolTipPlacement.TopLeft,
                    AutoToolTipPrecision = 1,
                    DataContext = v
                };
                sl.ValueChanged += Sl_ValueChanged;
                _sliders.Add(sl);
                stackPanelSliders.Children.Add(sl);
            }
        }

        private void Sl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider)e.Source;
            var tuple = (Tuple<float, ulong>)slider.DataContext;
            var newTuple = new Tuple<float, ulong>((float)slider.Value, tuple.Item2);
            VolumeChanged?.Invoke(sender, newTuple);
        }
    }
}