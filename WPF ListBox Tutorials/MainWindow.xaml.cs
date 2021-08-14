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
        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// The content of the item
        /// </summary>
        public string Content
        { get; set; }

        /// <summary>
        /// A one char Abbreviation that is displayed in a graphical shape in the navigation
        /// </summary>
        public string Abbr
        { get; set; }
        
        public ContentItem()
        { 
            
        }
    }
    
    public class ViewModel
    {
        /// <summary>
        /// Stores all content
        /// </summary>
        public List<ContentItem> ContentList
        { get; set; }

        /// <summary>
        /// Stores the currently selected index. Needed to know the last selected index of the navigation listbox
        /// </summary>
        public int SelectedIndex;

        /// <summary>
        /// ViewModel Constructor
        /// </summary>
        public ViewModel()
        {
            SelectedIndex = -1;
            ContentList = new List<ContentItem>();
        }
    }
    
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instance of ViewModel
        /// </summary>        
        private ViewModel _viewModel;

        /// <summary>
        /// If the ListBox selection is changed, the Content will be saved/loaded and the currently selected index will temporarily be saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadContent(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel.SelectedIndex >= 0)
            {
                _viewModel.ContentList[_viewModel.SelectedIndex].Content = Content.Text;
            }

            Content.Text = _viewModel.ContentList[Navigation.SelectedIndex].Content;
            _viewModel.SelectedIndex = Navigation.SelectedIndex;
        }

        /// <summary>
        /// Main Process
        /// </summary>
        public MainWindow()
        {
            _viewModel = new ViewModel();   // Creates new ViewModel Object
            InitializeComponent();  
            DataContext = _viewModel;   // Links the ViewModel to the XAML Datacontext to allow Databinding
            _viewModel.ContentList.Add(new ContentItem() { Name = "Item 1", Content = "This is content 1.", Abbr = "1" });  // Adds ContentItem
            _viewModel.ContentList.Add(new ContentItem() { Name = "Item 2", Content = "This is content 2.", Abbr = "2" });  // Adds ContentItem
            _viewModel.ContentList.Add(new ContentItem() { Name = "Item 3", Content = "This is content 3.", Abbr = "3" });  // Adds ContentItem
        }

    }
}
