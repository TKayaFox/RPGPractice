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
        /// Determine which Mob should be taking the next turn
        /// </summary>
        public Mob NextTurn()
        {

            //Cycle to next Node
            //If at end of initiative, restart
            if (currentNode == null)
            {
                currentNode = head;
            }
            //else return next in initiative
            else
            {
                currentNode = currentNode.Next;
            }

            //get the next Mob for return
            return currentNode.Data;
        }

        private void addNode(Mob mob)
        {
            //make node
            Node newNode = new Node(mob);

            // Check if newNode should become the new head
            if (head == null || ReplaceCheck(head, newNode))
            {
                newNode.Next = head;
                head = newNode;
                currentNode = head;
            }
            else
            {
                //Otherwise, compare to all items in list
                Node tempNode = head;
                Node prevNode = head;
                bool placed = false;

                while (tempNode != null && !placed)
                {
                    //check if new mob should replace current mob in Initiative order
                    if (ReplaceCheck(tempNode, newNode))
                    {
                        ReplaceNode(tempNode, newNode, prevNode);
                        placed = true;
                    }
                    //If there is no next mob then put current mob next
                    else if (tempNode.Next == null)
                    {
                        tempNode.Next = newNode;
                        placed = true;
                    }
                    //else keep looping
                    else
                    {
                        prevNode = tempNode;
                        tempNode = tempNode.Next;
                    }
                }
            }
        }

        private void ReplaceNode(Node oldNode, Node newNode, Node prevNode)
        {
            prevNode.Next = newNode;
            newNode.Next = oldNode;
        }

        /// <summary>
        /// Returns true if mob2's initiative is lower than Mob1
        /// </summary>
        /// <param name="placedNode"></param>
        /// <param name="newNode"></param>
        /// <returns>Returns true determining if newNode should replace placedNode</returns>
        private bool ReplaceCheck(Node placedNode, Node newNode)
        {
            //get initiatives and determine if newNode has higher initiative
            int initiative1 = placedNode.Data.RollInitiative();
            int initiative2 = newNode.Data.RollInitiative();
            return (initiative2 > initiative1);
        }

        internal class Node
        {
            Node next;
            Mob data;

            public Node(Mob mob)
            {
                this.data = mob;
            }

            public Mob Data { get => data; set => data = value; }
            internal Node Next { get => next; set => next = value; }
        }
    }
}
