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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuardianRP.UI.Control {

    public partial class LoadingBar : UserControl {

        private readonly    Storyboard          _barStoryboard  = new Storyboard();
        private readonly    DoubleAnimation     _barAnimation   = new DoubleAnimation();

        public static readonly DependencyProperty PropertyProgress = DependencyProperty.Register(
            "Progress", typeof(byte), typeof(LoadingBar), new PropertyMetadata((byte)0), new ValidateValueCallback(IsProgressValid)
        );

        public static readonly DependencyProperty PropertyLabel = DependencyProperty.Register(
            "Label", typeof(string), typeof(LoadingBar), new PropertyMetadata(null)
        );

        /// <summary>
        /// Loading bar progress as procentage
        /// </summary>
        public byte Progress {
            get {
                return (byte) GetValue(PropertyProgress);
            }
            set {
                UpdateProgress(value);
                SetValue(PropertyProgress, value);
            }
        }

        /// <summary>
        /// Loading bar label, if empty or null defaults to progress procentage
        /// </summary>
        public string Label {
            get {
                return (string) GetValue(PropertyLabel);
            }
            set {
                SetValue(PropertyLabel, value);
                if(!string.IsNullOrEmpty(value))
                    LabelProgress.Content = value;
            }
        }

        public LoadingBar() {
            InitializeComponent();

            // Define animation frame
            ImageFill.Width = 0;
            _barAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            // Define Storyboard and what to animate
            _barStoryboard.Children.Add(_barAnimation);
            Storyboard.SetTargetName(_barAnimation, ImageFill.Name);
            Storyboard.SetTargetProperty(_barAnimation, new PropertyPath(WidthProperty));
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            // Initial draw
            UpdateProgress(Progress);
            if(!string.IsNullOrEmpty(Label))
                LabelProgress.Content = Label;
        }

        private static bool IsProgressValid(object value) {
            return (byte)value >= 0 && (byte)value <= 100;
        }

        private void UpdateProgress(byte procentage) {
            if(string.IsNullOrEmpty(Label))
                LabelProgress.Content = $"{procentage}%";

            _barAnimation.From = ImageFill.Width;
            _barAnimation.To = Math.Round((ImageBorder.Width - 32) / 100D * procentage);
            _barStoryboard.Begin(this);
        }

    }

}
