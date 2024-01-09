using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CalculatorAvalona.ViewModels;

namespace CalculatorAvalona.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            // this.FindControl<ListBox>("ListBox").PointerPressed += LogList_OnDoubleTapped;
            this.FindControl<ListBox>("ListBox").AddHandler(PointerPressedEvent, LogList_OnDoubleTapped, RoutingStrategies.Tunnel);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ExecuteCommand(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            var inputBox = this.FindControl<TextBox>("InputBox");
            viewModel.ExecuteCommand(inputBox.Text);
            inputBox.Text = string.Empty;
        }
        
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExecuteCommand(sender, e);
            }
        }
        
        private void LogList_OnDoubleTapped(object sender, PointerPressedEventArgs e)
        {
            if (e.ClickCount != 2) return;
            var listBox = (ListBox)sender;
            var logEntry = (LogEntry)listBox.SelectedItem;
            if (Equals(logEntry.Color, Brushes.Red))
                return;
            ((MainWindowViewModel)DataContext).ExecuteCommand($"> {logEntry.Text.Split().Last()}");
        }
    }
}