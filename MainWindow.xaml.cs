using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WelcomeScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ButtonLabelText ButtonLabelTextModel { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            ButtonLabelTextModel = new ButtonLabelText();

            DataContext = ButtonLabelTextModel;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonLabelTextModel.LabelTextChange();
            ButtonLabelTextModel.ButtonTextChange();
        }

        private void WindowCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            SizeChangedEventArgs canvas_ChangedArgs = e;

            if (canvas_ChangedArgs.PreviousSize.Width == 0) return;

            double oldHeight = canvas_ChangedArgs.PreviousSize.Height,
                   newHeight = canvas_ChangedArgs.NewSize.Height,
                   oldWidth = canvas_ChangedArgs.PreviousSize.Width,
                   newWidth = canvas_ChangedArgs.NewSize.Width;

            double scaleWidth = newWidth / oldWidth, scaleHeight = newHeight / oldHeight;

            foreach (FrameworkElement element in canvas.Children)
            {
                double oldLeft = Canvas.GetLeft(element),
                    oldTop = Canvas.GetTop(element);

                Canvas.SetLeft(element, oldLeft * scaleWidth);
                Canvas.SetTop(element, oldTop * scaleHeight);

                element.Width = element.Width * scaleWidth;
                element.Height = element.Height * scaleHeight;
            }
        }
    }

    public class ButtonLabelText : INotifyPropertyChanged
    {
        public bool DefaultState { get; set; }
        public string CurrentLabelText { get; set; }
        public string DefaultLabelText { get; set; }
        public string AlternateLabelText { get; set; }
        public string CurrentButtonText { get; set; }
        public string DefaultButtonText { get; set; }
        public string AlternateButtonText { get; set; }

        public ButtonLabelText()
        {
            DefaultState = true;
            DefaultLabelText = "Hit Enter Key";
            DefaultButtonText = "Enter";
            AlternateLabelText = "ImagineSoftware is a leading provider of medical billing, revnue cycle, and practice management software";
            AlternateButtonText = "Exit";
            CurrentLabelText = DefaultLabelText;
            CurrentButtonText = DefaultButtonText; 
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        public void LabelTextChange()
        {
            CurrentLabelText = CurrentLabelText == DefaultLabelText ? AlternateLabelText : DefaultLabelText;
            OnPropertyRaised("CurrentLabelText");

        }
        public void ButtonTextChange()
        {
            CurrentButtonText = CurrentButtonText == DefaultButtonText ? AlternateButtonText : DefaultButtonText;
            OnPropertyRaised("CurrentButtonText");
        }

    }
}
