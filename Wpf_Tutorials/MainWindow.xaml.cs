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
    /// ViewModel, welches das Databinding managed
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Inhalt für das Textfeld, welches per Databinding gefüttert wird
        /// </summary>
        public string txtContent
        { get; set; }

        /// <summary>
        /// Privates Feld, welches die Daten für das Databinding enthält
        /// </summary>
        private string _BtnCaption;

        /// <summary>
        /// Public Feld, mit welchem das private Feld per Accessors gefüttert wird und bei Set auch automatisch einen Refresh auslöst
        /// </summary>
        public string BtnCaption
        {
            get { return _BtnCaption; }
            set 
            {
                _BtnCaption = value; 
                OnPropertyRaised("BtnCaption"); 
            } 
        }

        /// <summary>
        /// Datencontainer für ListBox
        /// </summary>
        public List<string> ListBoxContent
        { get; set; }

        /// <summary>
        /// Boolsches Flag welches die Start()-Methode anhält
        /// </summary>
        public bool stopCondition
        { get; set; }

        /// <summary>
        /// Konstruktor für die ViewModel-Klasse
        /// </summary>
        public ViewModel()
        {
            txtContent = "Hello World!";
            _BtnCaption = "Start";
            ListBoxContent = new List<string>();
            ListBoxContent.Add("Item 1");
            ListBoxContent.Add("Item 2");
        }

        /// <summary>
        /// Worker-Task, der asynchron ausgeführt wird und z.B. eine Zahl hochzählt
        /// </summary>
        public async void Start()
        {
            await Task.Run(() =>
            {
                int i = 0;

                while (stopCondition == false)
                {
                    Thread.Sleep(10);
                    txtContent = i.ToString() + "...";
                    OnPropertyRaised("txtContent");
                    i++;
                }
                txtContent = "Stopped!";
                OnPropertyRaised("txtContent");
            });
        }

        public void AddListItem(string content)
        {
            ListBoxContent.Add(content);
            OnPropertyRaised("ListBoxContent");
        }

        /// <summary>
        /// PropertyChanged Event, wird für MVVM Erweiterung benötigt
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Diese Methode sorgt für einen Refresh des per Databinding verbundenen Controls
        /// </summary>
        /// <param name="propertyname"></param>
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
        /// <summary>
        /// Objekt-Instanz der ViewModel-Klasse
        /// </summary>
        ViewModel viewModel = new ViewModel();
        Window1 win1 = new Window1();

        /// <summary>
        /// Aktionen die durch einen Klick des Buttons ausgelöst werden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.stopCondition == true)
            {
                viewModel.stopCondition = false;
                viewModel.Start();
                viewModel.BtnCaption = "Stop";

                
                win1.Show();
            }
            else
            {
                viewModel.stopCondition = true;
                viewModel.BtnCaption = "Start";

                //win1.Hide();
            }

            viewModel.AddListItem("NewItem");
        }

        /// <summary>
        /// Aktion die durch Wechseln der ListBox Auswahl ausgelöst wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TB_LBIndex.Text = "Selected Index: " + ListBox1.SelectedIndex;
        }

        /// <summary>
        /// Hauptprozess des MainWindows
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            //ListBox.ItemsSource = viewModel.ListBoxContent;
            viewModel.Start();
        }

        /// <summary>
        /// Fügt in TextBox enthaltenen Inhalt als neues ListBox Element hinzu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_AddElement(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Add(TextBox1.Text);
        }

        /// <summary>
        /// Entfernt das ausgewählte ListboxElement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_DeleteElement(object sender, RoutedEventArgs e)
        {
            // https://www.youtube.com/watch?v=5t9rThr9BI8
            if (ListBox1.SelectedItem != null)
            {
                ListBox1.Items.Remove(ListBox1.SelectedItem);
            }
        }
    }
}
