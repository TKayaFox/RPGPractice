using RPGPractice.Engine.MobClasses.EnemyMobs;
using RPGPractice.Engine.MobClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Core.Enumerations;
using RPGPractice.Core;

namespace RPGPractice.Engine
{
    public class Encounter
    {
        private const int MIN_ENEMIES = 1;
        private Mob[] heroes;
        private Mob[] enemies;
        private Random random;
        private int maxEnemies;
        private int enemyCount;

        public Mob[] Heroes { get => heroes; set => heroes = value; }
        public Mob[] Enemies { get => enemies; set => enemies = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxEnemies">Maximum number of enemies in encounter</param>
        /// <param name="random">Randomizer for encounter variation</param>
        public Encounter(int maxEnemies, Random random)
        {
            this.maxEnemies = maxEnemies;
            this.random = random;
        }

        /// <summary>
        /// Initializes an array of Mobs for a combat encounter
        /// </summary>
        /// <returns>MobID[] array of enemies/NPCs</returns>
        public void GenerateEncounter(EventManager eventManager, int combatLevel)
        {
            List<Mob> enemyList = new List<Mob>();
            int numBandits = 0;
            int numOgres = 0;
            int numDragons = 0;

            EnemyType[] encounterTypes = new EnemyType[maxEnemies];

            //loop until created mobs are between minimum and maximum values
            int enemyCount = 0;
            do
            {
                int min = 0;
                int max = 0;

                //get the minumum and maximum number of that mob for the encounter level
                //  then randomly generate a number
                (min, max) = Bandit.EncounterData(combatLevel);
                numBandits = random.Next(min, max+1);
                (min, max) = Ogre.EncounterData(combatLevel);
                numOgres = random.Next(min, max + 1);
                (min, max) = Dragon.EncounterData(combatLevel);
                numDragons = random.Next(min, max + 1);

                enemyCount = numBandits + numOgres + numDragons;
            } while (enemyCount > maxEnemies);

            //Generate Mobs and add to list
            //TODO: Refactor: I'm sure theres a way to do this with a single method using a generics, but i cant make it work
            for (int i = 0; i < numBandits; i++)
            {
                Mob enemy = new Bandit("Bandit");
                BuildMob(eventManager, enemyList, i, enemy);
            }
            for (int i = 0; i < numOgres; i++)
            {
                Mob enemy = new Ogre("Ogre");
                BuildMob(eventManager, enemyList, i, enemy);
            }
            for (int i = 0; i < numDragons; i++)
            {
                Mob enemy = new Dragon("Dragon");
                BuildMob(eventManager, enemyList, i, enemy);
            }

            //convert list to array
            //TODO: Modify other code to use Mob lists instead of arrays it would greatly simplify life
            enemies = new Mob[enemyCount];

            for (int i = 0; i < enemyCount; i++)
            {
                enemies[i] = enemyList[i];
            }
        }

        /// <summary>
        /// Does important initialization for generic Mobs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventManager"></param>
        /// <param name="enemyList"></param>
        /// <param name="i"></param>
        /// <param name="mob"></param>
        private static void BuildMob<T>(EventManager eventManager, List<Mob> enemyList, int i, T mob) where T : Mob
        {
            enemyList.Add(mob);
            mob.ManageEvents(eventManager);
            mob.UniqueID = i + 100;
        }
    }
}
