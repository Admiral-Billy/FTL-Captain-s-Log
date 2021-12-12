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

namespace FTL_Captain_s_Log
{
    /// <summary>
    /// Interaction logic for EventsPage.xaml
    /// </summary>
    public partial class EventsPage : UserControl
    {
        public EventsPage()
        {
            InitializeComponent();
            var sorted = Database.allEvents.OrderBy(FTLevent => FTLevent.eventName).ToArray();
            for (int i = 0; i < Database.allWeapons.Count; ++i)
            {
                EventBox.Items.Add(sorted[i].eventName);
            }
        }

        private void Event_Selected(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
