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
            for (int i = 0; i < Database.allEvents.Count; ++i)
            {
                EventBox.Items.Add(sorted[i].eventName);
            }
        }

        private void Event_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (EventBox.SelectedIndex != -1)
            {
                string newText = "";
                Event[] sorted;
                sorted = Database.allEvents.OrderBy(eventName => eventName.eventName).ToArray();
                Event FTLevent = sorted[EventBox.SelectedIndex];

                newText += FTLevent.eventText;

                if (FTLevent.autoRewardType != null || FTLevent.augmentReward != null || FTLevent.weaponReward != null || FTLevent.droneReward != null || FTLevent.crewReward != null)
                { // TODO: add all the other reward types, since there's a lot
                    newText += "\n\n Rewards/Losses:";
                    if (FTLevent.weaponReward != null)
                        newText += "\nWeapon reward: " + FTLevent.weaponReward.weaponName + " (" + FTLevent.weaponReward.blueprintName + ")";
                }

                EventText.Text = newText;
            }
            else
            {
                EventText.Text = "Event not found.";
            }
        }
    }
}
