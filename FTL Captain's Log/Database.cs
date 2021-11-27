using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void Parse()
        {
            ParseCrew();
            ParseWeapons();
            ParseDrones();
            ParseAugments();
            ParseShips();
            ParseStatBoosts();
            ParseEvents();
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
                                weapon.weaponName = reader.ReadElementContentAsString(); // TODO: handle text IDs and account for duplicates
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
                            else if (reader.Name == "shots")
                            {
                                weapon.shotCount = int.Parse(reader.ReadElementContentAsString());
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
                            else if (reader.Name == "cooldown")
                            {
                                weapon.cooldown = float.Parse(reader.ReadElementContentAsString());
                            }
                            else if (reader.Name == "hullbust")
                            {
                                weapon.hullBust = bool.Parse(reader.ReadElementContentAsString());
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
                            allWeapons.Add(weapon);
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

        }
    }
}
