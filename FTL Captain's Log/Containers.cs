using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTL_Captain_s_Log
{

    public enum StatBoostType
    {

    }

    public class StatBoost
    {
        // Internal identifier
        public int id = 0;

        // Universal stats
        public StatBoostType boostType;
    }

    public enum ChainType
    {
        COOLDOWN,
        DAMAGE
    }

    public class Crew
    {
        // Internal identifier
        public int id = 0;

        // Universal stats
        public bool isDrone = false;
    }

    public enum WeaponType
    {
        LASER,
        BEAM,
        MISSILES,
        BOMB,
        BURST
    }

    public class Weapon
    {
        // Internal identifier
        public int id = 0;

        // Universal stats
        public WeaponType weaponType = WeaponType.LASER;
        public string blueprintName = "";
        public string weaponName = "";
        public string description = "";
        public int powerCost = 0;
        public int shotCount = 0;
        public int chargeCount = 1;
        public float cooldown = 0;
        public int droneTargetability = 0;
        public int accuracyModifier = 0;
        public int chainCap = 0;
        public float chainAmount = 0;
        public ChainType chainType = ChainType.COOLDOWN;
        public int baseDamage = 0;
        public int extraPersDamage = 0;
        public int extraSysDamage = 0;
        public int ionDamage = 0;
        public int shieldPiercing = 0;
        public bool hullBust = false;
        public bool lockdown = false;
        public int fireChance = 0;
        public int breachChance = 0;
        public int stunChance = 0;
        public int stunLength = 3;
        public int statBoostChance = 0;
        public List<StatBoost> statBoosts = new List<StatBoost>();
        public List<Crew> spawnedCrew = new List<Crew>();
        public int crewSpawnChance = 0;
        public int missileCost = 0;
        public int freeMissileChance = 0;
        public int scrapCost = 0;
        public int rarity = 0;
        public int shotSpeed = 0;
        public string weaponImage;
        public string explosionImage;
        public List<string> launchSounds = new List<string>();

        // Beam stats
        public int length = 1;

        // Burst stats
        public int radius = 0;
        public int projectilesPerShot = 1;
    }

    public class Drone
    {
        // Internal identifier
        public int id = 0;
    }

    public class Augment
    {
        // Internal identifier
        public int id = 0;

        public bool hidden = false;
    }

    public enum SystemType
    {
        SHIELDS,    //0
        ENGINES,    //1
        OXYGEN,     //2
        WEAPONS,    //3
        DRONES,     //4
        MEDBAY,     //5
        PILOT,      //6
        SENSORS,    //7
        DOORS,      //8
        TELEPORTER, //9
        CLOAKING,   //10
        ARTILLERY,  //11
        BATTERY,    //12
        CLONEBAY,   //13
        MIND,       //14
        HACKING,    //15
        TEMPORAL = 20,
    }


    public class Door
    {
        public int x = 0;
        public int y = 0;
        public int roomId1 = 1; // -1 for airlocks
        public int roomId2 = 2;
    }

    public class Room
    {
        public int roomId = 0;
        public int width = 1;
        public int height = 1;
        public int x = 0;
        public int y = 0;
        public bool resistant = false;
        public List<SystemType> systems = new List<SystemType>();
    }

    public class Ship
    {
        // Internal identifier
        public int id = 0;

        public string blueprintName = "";
        public string shipName = "";
        public int reactorMax = 25;
        public int startingReactor = 0;
        public int missiles = 0;
        public int droneParts = 0;
        public int fuel = 0;
        public List<Crew> crew = new List<Crew>();
        public List<Weapon> weapons = new List<Weapon>();
        public List<Drone> drones = new List<Drone>();
        public List<Augment> augments = new List<Augment>();
        public List<Room> rooms = new List<Room>();
        public List<Door> doors = new List<Door>();
    }

    public class Event
    {
        // Internal identifier
        public int id = 0;

        public string eventName = "";
    }
}
