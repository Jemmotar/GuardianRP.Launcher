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

namespace GuardianRP.UI.Control {

    public partial class PanelButton : UserControl {

        public enum Variants {
            Exit,
            Hide
        }

        public static Dictionary<Variants, BitmapFrame[]> Textures = new Dictionary<Variants, BitmapFrame[]>() {
            {
                Variants.Exit, new BitmapFrame[] {
                    BitmapFrame.Create(new Uri("pack://application:,,,/GuardianRP.UI;component/Resources/PanelButtonExit.PNG")),
                    BitmapFrame.Create(new Uri("pack://application:,,,/GuardianRP.UI;component/Resources/PanelButtonExitDown.PNG")),
                    BitmapFrame.Create(new Uri("pack://application:,,,/GuardianRP.UI;component/Resources/PanelButtonExitDisabled.PNG"))
                }
            },
            {
                Variants.Hide, new BitmapFrame[] {
                    BitmapFrame.Create(new Uri("pack://application:,,,/GuardianRP.UI;component/Resources/PanelButtonHide.PNG")),
                    BitmapFrame.Create(new Uri("pack://application:,,,/GuardianRP.UI;component/Resources/PanelButtonHideDown.PNG")),
                    BitmapFrame.Create(new Uri("pack://application:,,,/GuardianRP.UI;component/Resources/PanelButtonHideDisabled.PNG"))
                }
            }
        };


        public static readonly DependencyProperty PropertyVariant = DependencyProperty.Register(
            "Variant", typeof(Variants), typeof(PanelButton), new PropertyMetadata(Variants.Exit)
        );

        public Variants Variant {
            get {
                return (Variants)GetValue(PropertyVariant);
            }
            set {
                SetValue(PropertyVariant, value);
                UpdateImage();
            }
        }

        private bool _isPressed = false;
        public bool IsPressed {
            get {
                return _isPressed;
            }
            set {
                if(_isPressed != value) {
                    _isPressed = value;
                    UpdateImage();
                }
            }
        }

        public PanelButton() {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            UpdateImage(); // Initial draw
        }

        private void UpdateImage() {
            ImageButton.Source = Textures[Variant][IsEnabled ? (IsPressed ? 1 : 0) : 2];
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e) {
            IsPressed = e.LeftButton == MouseButtonState.Pressed;
            CaptureMouse();
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e) {
            IsPressed = e.LeftButton == MouseButtonState.Pressed;
            Mouse.Capture(null);
        }

    }

}
