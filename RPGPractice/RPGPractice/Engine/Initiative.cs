using RPGPractice.Engine.MobClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGPractice.Engine
{
    public class Initiative
    {
        Node head;
        Node currentNode;
        int index;

        public Initiative(Mob[] heroes, Mob[] villians)
        {
            index = 0;

            //Make Initiative list
            foreach (Mob mob in heroes)
            {
                addNode(mob);
            }
            foreach (Mob mob in villians)
            {
                addNode(mob);
            }
        }

        /// <summary>
        /// Determine which MobID should be taking the next turn
        /// </summary>
        public int NextTurn()
        {

            //Cycle to next Node
            //If at end of initiative, restart
            if (currentNode == null || currentNode.Next == null)
            {
                currentNode = head;
            }
            //else return next in initiative
            else
            {
                currentNode = currentNode.Next;
            }
            System.Diagnostics.Debug.WriteLine($"Current Turn: {currentNode.Data}");

            //get the next MobID for return
            return currentNode.Data;
        }

        private void addNode(Mob mob)
        {
            //make node
            Node newNode = new Node(mob);

            // Check if newNode should become the new head
            if (head == null || HasHigherInitiative(head, newNode))
            {
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                //Otherwise, compare to all items in list
                Node current = head;

                //Loop until end of list OR next node would be a lower initiative
                while (current.Next != null && current.Next.Initiative > newNode.Initiative)
                {
                    current = current.Next;
                }
                if (current.Next != null)
                {
                    newNode.Next = current.Next;
                }
                current.Next = newNode;
            }
        }

        /// <summary>
        /// Returns true if mob2's initiative is lower than Mob1
        /// </summary>
        /// <param name="placedNode"></param>
        /// <param name="newNode"></param>
        /// <returns>Returns true determining if newNode should replace placedNode</returns>
        private bool HasHigherInitiative(Node placedNode, Node newNode)
        {
            //get initiatives and determine if newNode has higher initiative
            int initiative1 = placedNode.Initiative;
            int initiative2 = newNode.Initiative;
            return (initiative2 > initiative1);
        }

        internal class Node
        {
            Node next;
            int data;
            int priority;

            public Node(Mob mob)
            {
                this.data = mob.UniqueID;
                this.Initiative = mob.RollInitiative();
            }

            public int Data { get => data; set => data = value; }
            public int Initiative { get => priority; set => priority = value; }
            internal Node Next { get => next; set => next = value; }
        }
    }
}
