using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses.EnemyMobs;
using RPGPractice.Engine.MobClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Core.Enumerations;

namespace RPGPractice.Engine
{
    int maxEnemies;

    public class Encounter
    {
        private const int MIN_MOB = 1;
        private Mob[] heroes;
        private Mob[] enemies;
        private Random random;
        private int maxEnemies;

        public Mob[] Heroes { get => heroes; set => heroes = value; }
        public Mob[] Enemies { 
            get
            {
                //convert list to array
                //TODO: modify all classes to store Mobs as List instead
                Mob[] enemyArr = new Mob[enemyCount];

                for (int i = 0; i < enemyCount; i++)
                {
                    enemyArr[i] = enemies[i];
                }

                return enemyArr;
            }
            set => enemies = value; }

        public void Encounter(Random random, int maxEnemies)
        {

        }

        /// <summary>
        /// Initializes an array of Mobs for a combat encounter
        /// </summary>
        /// <returns>MobID[] array of enemies/NPCs</returns>
        public void GenerateEncounter(EventManager eventManager, int combatLevel)
        {
            List<Mob> enemies = new List<Mob>();

            EnemyType[] encounterTypes = new EnemyType[maxEnemies];

            //loop until minimum number of mobs (1) is created
            int enemyAdded = 0;
            while (enemyAdded < MIN_MOB)
            {
                int min = 0;
                int max = 0;

                //determine how many of each MobType is added
                //for each enemy type
                //  get the minumum and maximum number of that mob for the encounter level
                //  and get a randomized number between those values
                //TODO: automatically work with each class listed in EnemyType enum if possible?
                (min, max) = Bandit.EncounterData(combatLevel);
                int numBandits = random.Next(min, max);
                (min, max) = Ogre.EncounterData(combatLevel);
                int numOgre = random.Next(min, max);
                (min, max) = Dragon.EncounterData(combatLevel);
                int numDragon = random.Next(min, max);
            }

            //TODO: Implement actuall encounter scaling
            //      depending on Combat Level
            // = new Mob[1];

            //for (int i = 0; i < enemies.Length; i++)
            //{
            //    Mob enemy = new Bandit($"Bandit {i+1}");
            //    enemy.ManageEvents(eventManager);
            //    enemy.UniqueID = i + 100;
            //    enemies[i] = enemy;
            //    enemyCount++;
            //}

        }
    }
}
