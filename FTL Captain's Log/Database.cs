using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace FTL_Captain_s_Log
{
    public static class Database
    {
        public static List<Crew> allCrew = new List<Crew>();
        public static List<Weapon> allWeapons = new List<Weapon>();
        public static List<Drone> allDrones = new List<Drone>();
        public static List<Augment> allAugments = new List<Augment>();
        public static List<Ship> allShips = new List<Ship>();
        public static List<StatBoost> allStatBoosts = new List<StatBoost>();
        public static List<Event> allEvents = new List<Event>();
        public static Dictionary<string, string> allTextIds = new Dictionary<string, string>();
        public static MainWindow mainWindow;
        public static UIElement currentPage;

        public static void Parse()
        {
            ParseCrew();
            ParseWeapons();
            ParseDrones();
            ParseAugments();
            ParseShips();
            ParseStatBoosts();
            ParseEvents();
            //ParseTextIDs();
        }

        public static void ParseCrew()
        {

        }

        public static void ParseWeapons()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            XmlReader reader = XmlReader.Create(".\\Unpacker\\unpackedFiles\\data\\blueprints.xml", settings);
            ParseWeaponFile(reader);
            reader = XmlReader.Create(".\\Unpacker\\unpackedFiles\\data\\dlcBlueprints.xml", settings);
            ParseWeaponFile(reader);
        }

        public static void ParseWeaponFile(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.EndElement && reader.Name == "weaponBlueprint")
                {
                    Weapon weapon = new Weapon();
                    bool exit = false;

                    reader.MoveToAttribute("name");
                    weapon.blueprintName = reader.Value;
                    reader.MoveToElement();

                    while (!exit)
                    {
                        if (reader.NodeType != XmlNodeType.EndElement)
                        {
                            if (reader.Name == "type")
                            {
                                Enum.TryParse<WeaponType>(reader.ReadElementContentAsString(), out weapon.weaponType);
                            }
                            else if (reader.Name == "title")
                            {
                                weapon.weaponName = reader.ReadElementContentAsString(); // TODO: handle text IDs, probably by parsing them separately then using dictionary
                            }
                            else if (reader.Name == "desc")
                            {
                                weapon.description = reader.ReadElementContentAsString();
                            }
                            else if (reader.Name == "drone_targetable")
                            {
                                weapon.droneTargetability = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "speed")
                            {
                                weapon.shotSpeed = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "damage")
                            {
                                weapon.baseDamage = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "persDamage")
                            {
                                weapon.extraPersDamage = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "sysDamage")
                            {
                                weapon.extraSysDamage = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "ion")
                            {
                                weapon.ionDamage = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "sp")
                            {
                                weapon.shieldPiercing = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "accuracyMod")
                            {
                                weapon.accuracyModifier = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "missiles")
                            {
                                weapon.missileCost = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "length")
                            {
                                weapon.length = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "shots")
                            {
                                weapon.shotCount = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "radius")
                            {
                                weapon.radius = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "chargeLevels")
                            {
                                weapon.chargeCount = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "fireChance")
                            {
                                weapon.fireChance = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "breachChance")
                            {
                                weapon.breachChance = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "stunChance")
                            {
                                weapon.stunChance = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "stun")
                            {
                                weapon.stunChance = 10;
                                weapon.stunLength = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "cooldown")
                            {
                                weapon.cooldown = float.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "hullBust")
                            {
                                weapon.hullBust = Convert.ToBoolean(int.Parse(reader.ReadElementContentAsString()));
                            }
                            else if (reader.Name == "lockdown")
                            {
                                weapon.lockdown = Convert.ToBoolean(int.Parse(reader.ReadElementContentAsString()));
                            }
                            else if (reader.Name == "power")
                            {
                                weapon.powerCost = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "cost")
                            {
                                weapon.scrapCost = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "rarity")
                            {
                                weapon.rarity = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "boost")
                            {
                                reader.Read();
                                if (reader.ReadElementContentAsString() == "damage")
                                {
                                    weapon.chainType = ChainType.DAMAGE;
                                }
                                weapon.chainAmount = float.Parse(reader.ReadElementContentAsString());
                                weapon.chainCap = int.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "weaponArt")
                            {
                                weapon.weaponImage = reader.ReadElementContentAsString();
                            }
                            else
                            {
                                reader.Read();
                            }
                        }
                        else if (reader.Name != "weaponBlueprint")
                        {
                            reader.Read();
                        }

                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "weaponBlueprint")
                        {
                            exit = true;

                            // compensate for default values
                            if (weapon.shotSpeed == 0)
                            {
                                if (weapon.weaponType == WeaponType.MISSILES || weapon.weaponType == WeaponType.BURST)
                                {
                                    weapon.shotSpeed = 35;
                                }
                                else if (weapon.weaponType == WeaponType.LASER)
                                {
                                    weapon.shotSpeed = 60;
                                }
                                else if (weapon.weaponType == WeaponType.BEAM)
                                {
                                    weapon.shotSpeed = 5;
                                }
                            }

                            if (weapon.shieldPiercing == 0)
                            {
                                if (weapon.weaponType == WeaponType.MISSILES)
                                {
                                    weapon.shieldPiercing = 5;
                                }
                            }

                            if (weapon.blueprintName != "") // weapons can't be nameless, so a nameless weapon is a fake weapon that slipped through somehow
                            {
                                bool found = false;
                                for (int i = 0; i < allWeapons.Count; ++i)
                                {
                                    if (allWeapons[i].blueprintName == weapon.blueprintName)
                                    {
                                        allWeapons[i] = weapon; // only last version of weapon is relevant for what shows up ingame
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                allWeapons.Add(weapon);
                            }
                        }
                    }
                }
            }
        }

        public static void ParseDrones()
        {

        }

        public static void ParseAugments()
        {

        }

        public static void ParseShips()
        {

        }

        public static void ParseStatBoosts()
        {

        }

        public static void ParseEvents()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            DirectoryInfo myDirectory = new DirectoryInfo(@".\\Unpacker\\unpackedFiles\\data");
            FileInfo[] eventFiles = myDirectory.GetFiles("events???????????????????????????????.xml");
            for (int i = 0; i < eventFiles.Length; ++i)
            {
                XmlReader reader = XmlReader.Create(eventFiles[i].FullName, settings);

                try
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType != XmlNodeType.EndElement && reader.Name == "event")
                        {
                            Event FTLevent = new Event();
                            bool exit = false;

                            FTLevent.eventName = reader.GetAttribute("name");
                            if (FTLevent.eventName != null)
                            {
                                while (!exit)
                                {
                                    if (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        if (reader.Name == "text" && reader.Depth == 2)
                                        {
                                            FTLevent.eventText = reader.ReadElementContentAsString();
                                        }
                                        else if (reader.Name == "item_modify" && reader.Depth == 2)
                                        {
                                            if (reader.GetAttribute("steal") == "true")
                                            {
                                                FTLevent.stealResources = true;
                                            }
                                            reader.Read(); // read to the start of item modifying
                                            bool doneModifying = false;
                                            while (!doneModifying)
                                            {
                                                if (reader.Name == "item")
                                                {
                                                    if (reader.GetAttribute("type") == "scrap")
                                                    {
                                                        FTLevent.scrapModifierMin = int.Parse(reader.GetAttribute("min"));
                                                        FTLevent.scrapModifierMax = int.Parse(reader.GetAttribute("max"));
                                                    }
                                                    else if (reader.GetAttribute("type") == "fuel")
                                                    {
                                                        FTLevent.fuelModifierMin = int.Parse(reader.GetAttribute("min"));
                                                        FTLevent.fuelModifierMax = int.Parse(reader.GetAttribute("max"));
                                                    }
                                                    else if (reader.GetAttribute("type") == "missiles")
                                                    {
                                                        FTLevent.missilesModifierMin = int.Parse(reader.GetAttribute("min"));
                                                        FTLevent.missilesModifierMax = int.Parse(reader.GetAttribute("max"));
                                                    }
                                                    else if (reader.GetAttribute("type") == "drones")
                                                    {
                                                        FTLevent.dronePartsModifierMin = int.Parse(reader.GetAttribute("min"));
                                                        FTLevent.dronePartsModifierMax = int.Parse(reader.GetAttribute("max"));
                                                    }
                                                }
                                                else
                                                {
                                                    doneModifying = true;
                                                }
                                                reader.Read();
                                            }
                                        }
                                        else if (reader.Name == "weapon" && reader.Depth == 2)
                                        {
                                            string blueprintName = reader.GetAttribute("name");
                                            Weapon weapon = null;
                                            if (blueprintName != null)
                                            {
                                                bool weaponFound = false;
                                                for (int j = 0; j < allWeapons.Count; ++j)
                                                {
                                                    if (blueprintName == allWeapons[j].blueprintName)
                                                    {
                                                        weapon = allWeapons[j];
                                                        weaponFound = true;
                                                        break;
                                                    }
                                                }
                                                if (!weaponFound)
                                                {
                                                    weapon = new Weapon();
                                                    weapon.weaponName = "Invalid Weapon/Weapon list";
                                                    weapon.blueprintName = blueprintName;
                                                }
                                            }
                                            FTLevent.weaponReward = weapon;
                                            reader.Read();
                                        }
                                        else if (reader.Name == "drone" && reader.Depth == 2)
                                        {
                                            string blueprintName = reader.GetAttribute("name");
                                            Drone drone = null;
                                            if (blueprintName != null)
                                            {
                                                bool weaponFound = false;
                                                for (int j = 0; j < allDrones.Count; ++j)
                                                {
                                                    if (blueprintName == allDrones[j].blueprintName)
                                                    {
                                                        drone = allDrones[j];
                                                        weaponFound = true;
                                                        break;
                                                    }
                                                }
                                                if (!weaponFound)
                                                {
                                                    drone = new Drone();
                                                    drone.droneName = "Invalid Weapon/Weapon list";
                                                    drone.blueprintName = blueprintName;
                                                }
                                            }
                                            FTLevent.droneReward = drone;
                                            reader.Read();
                                        }
                                        else if (reader.Name == "choice" && reader.Depth == 2)
                                        {
                                            Choice choice = new Choice();
                                            if (reader.GetAttribute("hidden") == "true")
                                            {
                                                choice.hidden = true;
                                            }
                                            if (reader.GetAttribute("req") != null)
                                            {
                                                choice.req = reader.GetAttribute("req");
                                            }
                                            if (reader.GetAttribute("blue") == "true")
                                            {
                                                choice.blue = true;
                                            }
                                            if (reader.GetAttribute("lvl") != null)
                                            {
                                                choice.minReqLevel = int.Parse(reader.GetAttribute("lvl"));
                                            }
                                            if (reader.GetAttribute("max_lvl") != null)
                                            {
                                                choice.maxReqLevel = int.Parse(reader.GetAttribute("max_lvl"));
                                            }
                                            if (reader.GetAttribute("max_group") != null)
                                            {
                                                choice.maxGroup = int.Parse(reader.GetAttribute("max_group"));
                                            }
                                            reader.Read();
                                            bool doneWithChoice = false;
                                            while (!doneWithChoice)
                                            {
                                                if (reader.Name == "text")
                                                {
                                                    choice.text = reader.ReadElementContentAsString();
                                                }
                                                // parse recursive events here


                                                reader.Read();
                                                // check for exit



                                                doneWithChoice = true;
                                            }
                                            FTLevent.choices.Add(choice);
                                        }
                                        else
                                        {
                                            reader.Read();
                                        }
                                    }
                                    else if (reader.Name != "event")
                                    {
                                        reader.Read();
                                    }
                                    else
                                    {
                                        exit = true;
                                    }
                                }

                                bool found = false;
                                for (int j = 0; j < allEvents.Count; ++j)
                                {
                                    if (allEvents[j].eventName == FTLevent.eventName)
                                    {
                                        allEvents[j] = FTLevent; // only last version of event is relevant for what shows up ingame
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                allEvents.Add(FTLevent);
                            }
                        }
                    }
                }
                catch (XmlException e)
                {
                    // invalid file, but that's fine; the game wouldn't run it anyways, so we don't care very much
                }

                //reader = XmlReader.Create(".\\Unpacker\\unpackedFiles\\data\\hyperspace.xml", settings);

                //while (reader.Read())
                //{
                // read the hyperspace event data somehow and put it into the original, maybe use something besides a list (map or dictionary?) for everything so it's efficient? (allEvents["STORAGE_CHECK"] as an example?
                //}
            }
        }
    }
}
