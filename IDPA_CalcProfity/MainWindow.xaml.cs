using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using IDPA_CalcProfity.MVVM.View;
using IDPA_CalcProfity.MVVM.ViewModel;

namespace IDPA_CalcProfity
{
    public partial class MainWindow : Window
    {
        private RadioButton _lastClickedRadioButton;
        private HomeViewModel _homeViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _homeViewModel = new HomeViewModel();
            MainFrame.Navigate(new HomeView { DataContext = _homeViewModel });

            SetDefaultButtonColor();
        }

        private void SetDefaultButtonColor()
        {
            var rechnerButton = FindName("RechnerButton") as RadioButton;
            if (rechnerButton != null)
            {
                SolidColorBrush clickedColor = new SolidColorBrush(Color.FromArgb(255, 18, 20, 23));
                ChangeRadioButtonColor(rechnerButton, clickedColor);

                _lastClickedRadioButton = rechnerButton;
            }
        }

        private void myFrame_ContentRendered(object sender, System.EventArgs e)
        {
            MainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard fadeOutStoryboard = (Storyboard)this.Resources["FadeOutStoryboard"];
            fadeOutStoryboard.Completed += (s, a) => Application.Current.Shutdown();
            fadeOutStoryboard.Begin();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ChangeRadioButtonColor(RadioButton radioButton, Brush color)
        {
            if (radioButton != null)
            {
                radioButton.Background = color;
            }
        }

        private void RadioButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                SolidColorBrush hoverColor = new SolidColorBrush(Color.FromArgb(255, 34, 38, 42)); // #22262A

                if (radioButton != _lastClickedRadioButton)
                {
                    ChangeRadioButtonColor(radioButton, hoverColor);
                }
            }
        }

        private void RadioButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton != _lastClickedRadioButton)
                {
                    SolidColorBrush previousColor = new SolidColorBrush(Color.FromArgb(255, 22, 26, 30)); // #161A1E
                    ChangeRadioButtonColor(radioButton, previousColor);
                }
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                SolidColorBrush clickedColor = new SolidColorBrush(Color.FromArgb(255, 18, 20, 23)); // #121417
                SolidColorBrush previousColor = new SolidColorBrush(Color.FromArgb(255, 22, 26, 30)); // #161A1E

                ChangeRadioButtonColor(_lastClickedRadioButton, previousColor);
                ChangeRadioButtonColor(radioButton, clickedColor);

                _lastClickedRadioButton = radioButton;

                // Navigate to the selected page
                switch (radioButton.Content.ToString())
                {
                    case "Rechner":
                        MainFrame.Navigate(new HomeView { DataContext = _homeViewModel });
                        break;
                    case "Kontakt":
                        MainFrame.Navigate(new KontaktView { DataContext = _homeViewModel });
                        break;
                    case "Hilfe":
                        MainFrame.Navigate(new InfoView { DataContext = _homeViewModel });
                        break;
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Handle RadioButton checked event if needed
        }

        // MouseEnter event handler for CloseButton
        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button closeButton)
            {
                // Set the background to a brighter red when mouse hovers
                closeButton.Background = new SolidColorBrush(Color.FromRgb(255, 102, 102)); // Bright Red (#FF6666)
            }
        }

        // MouseLeave event handler for CloseButton
        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button closeButton)
            {
                // Set the background back to red when the mouse leaves
                closeButton.Background = new SolidColorBrush(Colors.Red); // Red
            }
        }
    }
}
