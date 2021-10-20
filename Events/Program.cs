using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Events
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);
    
    public class ListChanged
    {
        public List<string> _list;

        public ListChanged()
        {
            _list = new List<string>();
        }
        public void Add(string value)
        {
            OnChanged(EventArgs.Empty);

            _list.Add(value);
        }

        public event ChangedEventHandler changed;

        protected virtual void OnChanged(EventArgs e)
        {
            if (changed != null)
            {
                changed(this, e);
            }
        }


    }
    
    class Listener
    {
        public ListChanged list;
        public Listener(ListChanged _list)
        {
            list = _list;
            list.changed += new ChangedEventHandler(list_changed);
        }

        private void list_changed(object sender, EventArgs e)
        {
            
            Console.WriteLine("Element added: ");
            foreach (string element in list._list)
            {
                Console.WriteLine(element);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListChanged list = new ListChanged();
            Listener listener = new Listener(list);
            list.Add("element");
            list.Add("element2");
            Console.ReadKey();
        }
    }
}
