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

                newText += weapon.weaponName + " (" + weapon.weaponType.ToString() + ")\n\n";

                if (weapon.powerCost >= 0)
                {
                    newText += "Required Power: " + weapon.powerCost + "\n";
                }
                else
                {
                    newText += "Bonus Power: " + weapon.powerCost * -1 + "\n";
                }
                if (weapon.cooldown >= 0)
                {
                    newText += "Charge Time: " + weapon.cooldown + " seconds\n";
                    if (weapon.missileCost != 0)
                        newText += "Missile Consumption: " + weapon.missileCost + "\n";
                    if (weapon.freeMissileChance != 0)
                        newText += "Free Shot Chance: " + weapon.freeMissileChance + "%\n";
                    if (weapon.chainCap > 0)
                    {
                        if (weapon.chainType == ChainType.COOLDOWN && weapon.chainAmount > 0)
                        {
                            newText += "Chain effect: reduces cooldown by " + weapon.chainAmount + "\n--> (capped at " + (weapon.cooldown - weapon.chainAmount * weapon.chainCap) + " seconds after " + weapon.chainCap + " chains)\n";
                        }
                        else if (weapon.chainType == ChainType.COOLDOWN)
                        {
                            newText += "Chain effect: increases cooldown by " + weapon.chainAmount * -1 + "\n--> (capped at " + (weapon.cooldown + weapon.chainAmount * weapon.chainCap) + " seconds after " + weapon.chainCap + " chains)\n";
                        }
                        else
                        {
                            newText += "Chain effect: increases base damage by " + weapon.chainAmount + "\n--> (capped at " + (weapon.baseDamage + weapon.chainAmount * weapon.chainCap) + " damage after " + weapon.chainCap + " chains)\n";
                        }
                    }
                    if (weapon.accuracyModifier > 0)
                        newText += "Hit Chance Modifier: +" + weapon.accuracyModifier + "%\n";
                    if (weapon.accuracyModifier < 0)
                        newText += "Hit Chance Modifier: " + weapon.accuracyModifier + "%\n";
                    if (newText.EndsWith("\n\n"))
                    {
                        // do nothing
                    }
                    else
                    {
                        newText += "\n";
                    }

                    if (weapon.radius > 0)
                        newText += "Shot radius: " + weapon.radius + " pixels\n";
                    if (weapon.weaponType != WeaponType.BEAM)
                    {
                        newText += "Shots Per Charge: " + weapon.shotCount;
                        if (weapon.weaponType == WeaponType.BOMB && weapon.shotCount > 1)
                        {
                            newText += " (only shoots one bomb on owner's ship)";
                        }
                        newText += "\n";
                        if (weapon.projectilesPerShot > 1)
                            newText += "Projectiles Per Shot: " + weapon.projectilesPerShot + "\n";
                        if (weapon.chargeCount > 1)
                            newText += "Max Charged Shots: " + weapon.chargeCount + "\n";
                        if (weapon.weaponType != WeaponType.BOMB)
                            newText += "Shot Speed: " + weapon.shotSpeed + "\n";
                    }
                    else
                    {
                        newText += "Beam Length: " + weapon.length + " pixels\n";
                        newText += "Swipe speed: " + weapon.shotSpeed + "\n";
                    }
                    if (newText.EndsWith("\n\n"))
                    {
                        // do nothing
                    }
                    else
                    {
                        newText += "\n";
                    }

                    if (weapon.hullBust == true)
                        newText += "Deals 2x hull damage to systemless rooms\n";
                    if (weapon.baseDamage > 0)
                        newText += "Hull Damage: " + weapon.baseDamage + "\n";
                    if (weapon.baseDamage < 0)
                        newText += "Hull Repair: " + weapon.baseDamage * -1 + "\n";
                    if (weapon.extraSysDamage + weapon.baseDamage > 0)
                        newText += "Total System Damage: " + (weapon.extraSysDamage + weapon.baseDamage) + "\n";
                    if (weapon.extraSysDamage + weapon.baseDamage < 0)
                        newText += "Total System Repair: " + (weapon.extraSysDamage + weapon.baseDamage) * -1 + "\n";
                    if (weapon.extraPersDamage + weapon.baseDamage > 0)
                        newText += "Total Crew Damage: " + (weapon.extraPersDamage + weapon.baseDamage) * 15 + "\n";
                    if (weapon.extraPersDamage + weapon.baseDamage < 0)
                        newText += "Total Crew Healing: " + (weapon.extraPersDamage + weapon.baseDamage) * -15 + "\n";
                    if (weapon.ionDamage != 0)
                        newText += "Ion Damage: " + weapon.ionDamage + "\n";
                    if (weapon.shieldPiercing != 0)
                        newText += "Shield Piercing: " + weapon.shieldPiercing + "\n";
                    if (newText.EndsWith("\n\n"))
                    {
                        // do nothing
                    }
                    else
                    {
                        newText += "\n";
                    }

                    if (weapon.lockdown == true)
                        newText += "Locks down rooms on hit\n";
                    if (weapon.fireChance != 0)
                        newText += "Fire Chance: " + weapon.fireChance * 10 + "%\n";
                    if (weapon.breachChance != 0)
                        newText += "Breach Chance: " + weapon.breachChance * 10 + "%";
                    if (weapon.fireChance != 0 && weapon.breachChance != 0)
                    {
                        newText += " (Adjusted: " + (weapon.breachChance * (1 - weapon.fireChance / 10.0) * 10) + "%)\n";
                    }
                    else if (weapon.breachChance != 0)
                    {
                        newText += "\n";
                    }
                    if (weapon.stunChance != 0)
                        newText += "Stun Chance: " + weapon.stunChance * 10 + "% (" + weapon.stunLength + " seconds long)\n";
                    if (weapon.statBoostChance != 0)
                        newText += "Affliction Chance: " + weapon.statBoostChance + "%\n";
                    if (weapon.crewSpawnChance != 0)
                        newText += "Crew Spawn Chance: " + weapon.crewSpawnChance + "%\n";
                    if (newText.EndsWith("\n\n"))
                    {
                        // do nothing
                    }
                    else
                    {
                        newText += "\n";
                    }

                    newText += "Buy/Sell Price: " + weapon.scrapCost + "/" + weapon.scrapCost / 2 + " scrap\n";
                }
                else
                {
                    newText += "Cannot fire shots\n";
                }

                WeaponStats.Text = newText;
            }
            else
            {
                WeaponStats.Text = "Weapon blueprint not found.";
            }
        }
    }
}
