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
    public class CombatEncounter
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
        ///     Initializes an array of Mobs for a combat CombatEncounter
        /// </summary>
        /// <param name="maxEnemies">Maximum number of enemies in encounter</param>
        /// <param name="random">Randomizer for encounter variation</param>
        public CombatEncounter(int maxEnemies,int combatLevel, Random random)
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
                numBandits = random.Next(min, max + 1);
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
                Mob enemy = new Bandit("Bandit",random);
                enemyList.Add(enemy);
                enemy.UniqueID = i + 100;
            }
            for (int i = 0; i < numOgres; i++)
            {
                Mob enemy = new Ogre("Ogre",random);
                enemyList.Add(enemy);
                enemy.UniqueID = i + 200;
            }
            for (int i = 0; i < numDragons; i++)
            {
                Mob enemy = new Dragon("Dragon", random);
                enemyList.Add(enemy);
                enemy.UniqueID = i + 300;
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
        private void BuildMob<T>(List<Mob> enemyList, int i, T mob) where T : Mob
        {
        }
    }
}
