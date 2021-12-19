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

                if (FTLevent.eventText != "")
                {
                    newText += FTLevent.eventText;
                }
                else
                {
                    newText += "This event has no text, and thus doesn't actually show up on screen.";
                }

                if (FTLevent.autoRewardType != null 
                    || FTLevent.augmentReward != null 
                    || FTLevent.weaponReward != null 
                    || FTLevent.droneReward != null 
                    || FTLevent.crewReward != null 
                    || FTLevent.scrapModifierMin != 0 || FTLevent.scrapModifierMax != 0
                    || FTLevent.fuelModifierMin != 0 || FTLevent.fuelModifierMax != 0
                    || FTLevent.missilesModifierMin != 0 || FTLevent.missilesModifierMax != 0
                    || FTLevent.dronePartsModifierMin != 0 || FTLevent.dronePartsModifierMax != 0)
                { // TODO: add all the other reward types, since there's a lot
                    newText += "\n\nRewards/Losses:";
                    if (FTLevent.scrapModifierMin != 0 || FTLevent.scrapModifierMax != 0)
                    {
                        if (FTLevent.scrapModifierMin == FTLevent.scrapModifierMax)
                        {
                            newText += "\nScrap change: " + FTLevent.scrapModifierMin;
                        }
                        else
                        {
                            newText += "\nScrap change: " + FTLevent.scrapModifierMin + " to " + FTLevent.scrapModifierMax;
                        }
                    }
                    if (FTLevent.fuelModifierMin != 0 || FTLevent.fuelModifierMax != 0)
                    {
                        if (FTLevent.fuelModifierMin == FTLevent.fuelModifierMax)
                        {
                            newText += "\nFuel change: " + FTLevent.fuelModifierMin;
                        }
                        else
                        {
                            newText += "\nFuel change: " + FTLevent.fuelModifierMin + " to " + FTLevent.fuelModifierMax;
                        }
                    }
                    if (FTLevent.missilesModifierMin != 0 || FTLevent.missilesModifierMax != 0)
                    {
                        if (FTLevent.missilesModifierMin == FTLevent.missilesModifierMax)
                        {
                            newText += "\nMissiles change: " + FTLevent.missilesModifierMin;
                        }
                        else
                        {
                            newText += "\nMissiles change: " + FTLevent.missilesModifierMin + " to " + FTLevent.missilesModifierMax;
                        }
                    }
                    if (FTLevent.dronePartsModifierMin != 0 || FTLevent.dronePartsModifierMax != 0)
                    {
                        if (FTLevent.dronePartsModifierMin == FTLevent.dronePartsModifierMax)
                        {
                            newText += "\nDrone parts change: " + FTLevent.dronePartsModifierMin;
                        }
                        else
                        {
                            newText += "\nDrone parts change: " + FTLevent.dronePartsModifierMin + " to " + FTLevent.dronePartsModifierMax;
                        }
                    }
                    if (FTLevent.weaponReward != null)
                        newText += "\nWeapon reward: " + FTLevent.weaponReward.weaponName + " (" + FTLevent.weaponReward.blueprintName + ")";
                    if (FTLevent.augmentReward != null)
                        newText += "Wut";
                    if (FTLevent.droneReward != null)
                        newText += "Wut";
                    if (FTLevent.crewReward != null)
                        newText += "Wut";

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
