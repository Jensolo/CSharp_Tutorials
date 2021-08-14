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

namespace WPF_ListBox_Tutorials
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public class ContentItem
    {
        public string Name
        { get; set; }

        public string Content
        { get; set; }

        public string Abbr
        { get; set; }
        
        public ContentItem()
        { 
            
        }
    
    }
    
    public class ViewModel
    {
        public List<ContentItem> ContentList
        { get; set; }

        public int SelectedIndex;

        public ViewModel()
        {
            SelectedIndex = -1;
            ContentList = new List<ContentItem>();
        }
    
    }
    
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;
        
        public MainWindow()
        {
            _viewModel = new ViewModel();
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.ContentList.Add(new ContentItem() { Name = "Item 1", Content = "This is content 1.", Abbr = "1" });
            _viewModel.ContentList.Add(new ContentItem() { Name = "Item 2", Content = "This is content 2.", Abbr = "2" });
            _viewModel.ContentList.Add(new ContentItem() { Name = "Item 3", Content = "This is content 3.", Abbr = "3" });
        }

        private void LoadContent(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel.SelectedIndex >= 0)
            {
                _viewModel.ContentList[_viewModel.SelectedIndex].Content = Content.Text;
            }
            
            Content.Text = _viewModel.ContentList[Navigation.SelectedIndex].Content;
            _viewModel.SelectedIndex = Navigation.SelectedIndex;
        }
    }
}
