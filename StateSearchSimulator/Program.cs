using System;
using System.Collections.Generic;

namespace StateSearchSimulator
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            StateSearchSimulator s = new StateSearchSimulator();
            s.Simulate();
        }
    }

    class StateSearchSimulator
    {
        // Initital State
        int x = 0;
        int y = 0;

        // Empty Lists - Open, Closed, Temporary
        List<int[]> oList = new List<int[]>();
        List<int[]> cList = new List<int[]>();
        List<int[]> tList = new List<int[]>();

        #region Logic

        public void Simulate()
        {
            while(true)
            {
                // Print state
                #region Print

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("State");
                Console.WriteLine("{ [" + x + "," + y + "] }");
                Console.WriteLine("");

                #endregion

                // Clear temporary list
                ClearList();

                // Add values to closed list
                AddToClosedList(x, y);

                // Print actions
                #region Print

                Console.WriteLine("");
                Console.WriteLine("Actions");

                #endregion

                // Perform actions on state
                PerformActions(x, y);

                // Add values to open list
                AddToOpenListDFS();

                // Print state
                #region Print

                Console.WriteLine("");
                Console.WriteLine("State");
                Console.WriteLine("{ [" + x + "," + y + "] }");

                #endregion

                // Print lists
                #region Print

                Console.WriteLine("");
                Console.WriteLine("Open List");
                PrintList(oList);

                Console.WriteLine("");
                Console.WriteLine("Closed List");
                PrintList(cList);

                #endregion

                // If solution found...
                if (x == 5 || y == 5)
                {
                    // Print solution
                    #region Print

                    Console.WriteLine("");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine("Soln");
                    Console.WriteLine("");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("");

                    #endregion
                }

                // If list empty...
                if (oList.Count == 0)
                {
                    // Print empty
                    #region Print

                    Console.WriteLine("");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine("Empty List");
                    Console.WriteLine("");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("");

                    #endregion
                    break;
                }

                // Remove from empty list
                RemoveFromOpenList();
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Press Enter to exit..");
            Console.ReadLine();
        }

        #endregion

        #region Actions

        private void Fill7(int _x, int _y)
        {
            if (_x < 7)
            {
                _x = 7;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }

        }

        private void Fill4(int _x, int _y)
        {
            if (_y < 4)
            {
                _y = 4;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }
        }

        private void Empty7Into4(int _x, int _y)
        {
            if (_x > 0 && _x + _y <= 4)
            {
                _y = _x + _y;
                _x = 0;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }
        }

        private void Empty4Into7(int _x, int _y)
        {
            if (_y > 0 && _x + _y <= 7)
            {
                _x = _x + _y;
                _y = 0;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }
        }

        private void Fill7From4(int _x, int _y)
        {
            if (_x < 7 && _x + _y > 7)
            {
                _y = _x + _y - 7;
                _x = 7;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }
        }

        private void Fill4From7(int _x, int _y)
        {
            if (_y < 4 && _x + _y > 4)
            {
                _x = _x + _y - 4;
                _y = 4;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }
        }

        private void Empty7(int _x, int _y)
        {
            if (_x > 0)
            {
                _x = 0;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }
        }

        private void Empty4(int _x, int _y)
        {
            if (_y > 0)
            {
                _y = 0;

                Console.WriteLine("[" + _x + "," + _y + "]");

                AddToTempList(_x, _y);
            }
            else
            {
                Console.WriteLine("    X");
            }
        }

        #endregion

        #region Methods

        private void PerformActions(int x, int y)
        {
            Fill7(x, y);

            Fill4(x, y);

            Empty7Into4(x, y);

            Empty4Into7(x, y);

            Fill7From4(x, y);

            Fill4From7(x, y);

            Empty7(x, y);

            Empty4(x, y);
        }

        private void ClearList()
        {
            tList.Clear();
        }

        private void AddToOpenListDFS()
        {
            int count = 0;

            // Prepends temp list to open list (Depth First Search)
            foreach (int[] state in tList)
            {
                if (!CheckIfAlreadyOnList(oList, state) && !CheckIfAlreadyOnList(cList, state))
                {
                    oList.Insert(count, state);
                    count++;
                }
            }
        }

        private void AddToClosedList(int _x, int _y)
        {
            int[] state = new int[] { _x, _y };
            
            if (!CheckIfAlreadyOnList(cList, state))
            {
                cList.Add(state);
            }
        }

        private void AddToTempList(int _x, int _y)
        {
            int[] state = new int[] { _x, _y };

            tList.Add(state);
        }

        private void RemoveFromOpenList()
        {
            if (oList.Count > 0)
            {
                x = oList[0][0];
                y = oList[0][1];

                oList.RemoveAt(0);
            }
        }

        private bool CheckIfAlreadyOnList(List<int[]> list, int[] state)
        {
            foreach (int[] item in list)
            {
                if (item[0] == state[0] && item[1] == state[1])
                {
                    return true;
                }
            }

            return false;
        }

        private void PrintList(List<int[]> list)
        {
            Console.Write("{ ");

            foreach (int[] item in list)
            {
                Console.Write("[" + item[0] + "," + item[1] + "] ");
            }

            Console.Write("}");
            Console.Write("\n");
        }

        #endregion
    }
}
