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

namespace GuardianRP.Launcher.UI.Control {

    public partial class LoadingBar : UserControl {

        private readonly    Storyboard          _barStoryboard  = new Storyboard();
        private readonly    DoubleAnimation     _barAnimation   = new DoubleAnimation();
        private             byte                _value          = 0;
        private             bool                _customText     = false;

        public byte Value {
            get {
                return _value;
            }
            set {
                if(value >= 0 && value <= 100) {
                    UpdateBar(value);
                    _value = value;
                }
            }
        }

        public string Text {
            get {
                return LabelProgress.Content as string;
            }
            set {
                _customText = value != null;
                if(_customText) LabelProgress.Content = value;
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

        /// <summary>
        /// Update progress bar with new value and triggers animation
        /// </summary>
        /// <param name="procentage">New bar value as procentage</param>
        private void UpdateBar(byte procentage) {
            if(!_customText)
                LabelProgress.Content = $"{procentage}%";

            _barAnimation.From = ImageFill.Width;
            _barAnimation.To = Math.Round((ImageBorder.Width - 32) / 100D * procentage);
            _barStoryboard.Begin(this);
        }

        /// <summary>
        /// Set value/text pair
        /// </summary>
        /// <param name="value">Procentage value</param>
        /// <param name="text">Label text</param>
        public void SetState(byte value, string text) {
            Value = value;
            Text = text;
        }
        
    }

}
