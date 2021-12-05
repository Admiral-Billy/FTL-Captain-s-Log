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
    /// Interaction logic for WeaponsPage.xaml
    /// </summary>
    public partial class WeaponsPage : UserControl
    {
        public WeaponsPage()
        {
            InitializeComponent();
            for (int i = 0; i < Database.allWeapons.Count; ++i)
            {
                WeaponBox.Items.Add(Database.allWeapons[i].blueprintName);
            }
        }

        private void Weapon_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (WeaponBox.SelectedIndex != -1)
            {
                string newText = "";
                Weapon weapon = Database.allWeapons[WeaponBox.SelectedIndex];

                newText += weapon.weaponName + "\n\n";

                newText += "Required Power: " + weapon.powerCost + "\n";
                newText += "Charge Time: " + weapon.cooldown + " seconds\n";
                if (weapon.missileCost != 0)
                newText += "Missile Consumption: " + weapon.missileCost + "\n";
                newText += "\n";

                newText += "Shots Per Charge: " + weapon.shotCount + "\n";
                if (weapon.projectilesPerShot > 1)
                newText += "Projectiles Per Shot: " + weapon.projectilesPerShot + "\n";
                newText += "\n";

                if (weapon.baseDamage != 0)
                newText += "Hull Damage: " + weapon.baseDamage + "\n";
                newText += "System Damage: TBD\n";
                newText += "Crew Damage: TBD\n";
                newText += "\n";

                newText += "Locks down rooms on hit\n";
                if (weapon.fireChance != 0)
                newText += "Fire Chance: " + weapon.fireChance * 10 + "%\n";
                newText += "Affliction Chance: TBD\n";
                newText += "\n";

                newText += "Buy/Sell Price: " + weapon.scrapCost + "/" + weapon.scrapCost / 2 + "\n";

                WeaponStats.Text = newText;
            }
            else
            {
                WeaponStats.Text = "Weapon blueprint not found.";
            }
        }
    }
}
