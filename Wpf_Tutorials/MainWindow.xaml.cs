using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

using System.ComponentModel;

namespace Wpf_Tutorials
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        public string txtContent
        { get; set; }
        
        public ViewModel()
        {
            txtContent = "Hello World!";
        }

        public async void Start()
        {
            await Task.Run(() =>
            {

                Thread.Sleep(2000);
                txtContent = "1...";
                OnPropertyRaised("txtContent");
                Thread.Sleep(500);
                txtContent = "2...";
                OnPropertyRaised("txtContent");
                Thread.Sleep(500);
                txtContent = "3...";
                OnPropertyRaised("txtContent");
                Thread.Sleep(500);
                txtContent = "Hellas!!";
                OnPropertyRaised("txtContent");
            });
        }

        // Based on this tutorial:
        // https://www.c-sharpcorner.com/article/explain-inotifypropertychanged-in-wpf-mvvm/

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
    
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();

        public void OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Start();
        }
    }
}
