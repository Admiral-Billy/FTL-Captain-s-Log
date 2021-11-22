using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTL_Captain_s_Log
{

    enum StatBoostType
    {

    }

    public class StatBoost
    {
        // Internal identifier
        int id = 0;

        // Universal stats
        StatBoostType boostType;
    }

    public class Crew
    {
        // Internal identifier
        int id = 0;

        // Universal stats
        bool isDrone = false;
    }

    enum WeaponType
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
        int id = 0;

        // Universal stats
        WeaponType weaponType = WeaponType.LASER;
        string blueprintName = "";
        string weaponName = "";
        int powerCost = 0;
        int shotCount = 0;
        int cooldown = 0;
        int baseDamage = 0;
        int extraPersDamage = 0;
        int extraSysDamage = 0;
        int ionDamage = 0;
        int shieldPiercing = 0;
        int fireChance = 0;
        int breachChance = 0;
        int stunChance = 0;
        List<StatBoost> statBoosts = new List<StatBoost>();
        List<Crew> spawnedCrew = new List<Crew>();
        int crewSpawnChance = 0;
        int missileCost = 0;
        int scrapCost = 0;
        int rarity = 0;
        int shotSpeed = 0;
        string weaponImage;
        string explosionImage;
        List<string> launchSounds = new List<string>();

        // Beam stats
        int length = 1;

        // Burst stats
        int radius = 0;
        int projectilesPerShot = 1;
    }

    public class Drone
    {
        // Internal identifier
        int id = 0;
    }

    public class Augment
    {
        // Internal identifier
        int id = 0;

        bool hidden = false;
    }

    enum SystemType
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
        int x = 0;
        int y = 0;
        int roomId1 = 1; // -1 for airlocks
        int roomId2 = 2;
    }

    public class Room
    {
        int roomId = 0;
        int width = 1;
        int height = 1;
        int x = 0;
        int y = 0;
        bool resistant = false;
        List<SystemType> systems = new List<SystemType>();
    }

    public class Ship
    {
        // Internal identifier
        int id = 0;

        string blueprintName = "";
        string shipName = "";
        int reactorMax = 25;
        int startingReactor = 0;
        int missiles = 0;
        int droneParts = 0;
        int fuel = 0;
        List<Crew> crew = new List<Crew>();
        List<Weapon> weapons = new List<Weapon>();
        List<Drone> drones = new List<Drone>();
        List<Augment> augments = new List<Augment>();
        List<Room> rooms = new List<Room>();
        List<Door> doors = new List<Door>();
    }

    public class Event
    {
        // Internal identifier
        int id = 0;

        string eventName = "";
    }
}
