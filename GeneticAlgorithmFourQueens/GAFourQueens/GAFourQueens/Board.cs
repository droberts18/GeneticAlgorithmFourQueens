using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAFourQueens
{
    class Board
    {
        private static readonly Random rdm = new Random();
        private int[,] queenRowLocationsAndFitness;

        public Board()
        {
            // 1st element is row location in 1st column, 2nd element is row location in 2nd column, etc.
            queenRowLocationsAndFitness = new int[4,4];

            setInitialQueenLocations();
            computeFitness();

            int populationCounter = 1;
            while (!currentQueenLocationsIsSolution())
            {
                Console.WriteLine("Population Counter: " + populationCounter);
                selection();
                mutation();
                computeFitness();
                populationCounter++;
            }
            printQueenLocations();
        }

        private void computeFitness()
        {
            for(int i = 0; i < 4; i++)
            {
                int curClashes = 0;
                // checking for same row
                for(int j = 0; j < 4; j++)
                {
                    if(i != j)
                    {
                        // if two queens exist in the same row
                        if (queenRowLocationsAndFitness[i,0] == queenRowLocationsAndFitness[j,0])
                            curClashes++;
                    }
                }

                // checking for diagonals
                int prevQueenDiff = int.MaxValue;
                int nextQueenDiff = int.MaxValue;

                // if not the first queen
                if (i != 0)
                    prevQueenDiff = Math.Abs(queenRowLocationsAndFitness[i, 0] - queenRowLocationsAndFitness[i - 1, 0]);

                // if not the last queen
                if (i != 3)
                    nextQueenDiff = Math.Abs(queenRowLocationsAndFitness[i, 0] - queenRowLocationsAndFitness[i + 1, 0]);

                if (prevQueenDiff == 1)
                    curClashes++;
                if (nextQueenDiff == 1)
                    curClashes++;

                queenRowLocationsAndFitness[i, 1] = 3 - curClashes;
            } 
        }

        private bool currentQueenLocationsIsSolution()
        {
            for(int i = 0; i < 4; i++)
            {
                if (queenRowLocationsAndFitness[i, 1] != 3)
                    return false;
            }
            return true;
        }

        private void selection()
        {
            int totalFitness = 0;

            // adding total fitness
            for (int i = 0; i < 4; i++)
                totalFitness += queenRowLocationsAndFitness[i, 1] + 1;

            // new population
            int[,] newPop = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                int rdmString = rdm.Next(0, totalFitness) + 1;

                // the randomly chosen row from the selection process
                int chosenRow;

                // if first string is chosen
                if (rdmString <= queenRowLocationsAndFitness[0, 0])
                    chosenRow = queenRowLocationsAndFitness[0, 0];
                // if second string in chosen
                else if (rdmString <= queenRowLocationsAndFitness[0, 0] + queenRowLocationsAndFitness[1, 0])
                    chosenRow = queenRowLocationsAndFitness[1, 0];
                // if third string is chosen
                else if (rdmString <= queenRowLocationsAndFitness[0, 0] + queenRowLocationsAndFitness[1, 0] + queenRowLocationsAndFitness[2, 0])
                    chosenRow = queenRowLocationsAndFitness[2, 0];
                // else fourth string must have been chosen
                else
                    chosenRow = queenRowLocationsAndFitness[3, 0];

                // setting the queen in the new population's current column to chosen row position
                newPop[i, 0] = chosenRow; 
            }
        }

        private void mutation()
        {
            double mutationProbability = 0.1;

            // calculating positions for mutations
            for(int i = 0; i < 4; i++)
            {
                double possibleMut = rdm.NextDouble();
                if (possibleMut <= mutationProbability) {
                    int rdmMutPoint = rdm.Next(0, 4);
                    queenRowLocationsAndFitness[i, 0] = rdmMutPoint;
                }
            }
        }

        private void setInitialQueenLocations()
        {
            for(int i = 0; i < 4; i++)
            {
                // row position within the i column
                int rowPos;

                rowPos = rdm.Next(0, 4);
                queenRowLocationsAndFitness[i, 0] = rowPos;
            }
        }

        public void printQueenLocations()
        {
            Console.WriteLine();
            // solution array is constructed by rows
            int[,] solution = new int[4,4];
            
            for(int i = 0; i < 4; i++)
            {
                int queenRow = queenRowLocationsAndFitness[i, 0];
                solution[queenRow, i] = 1;
            }

            // printing board
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Console.Write("\t" + solution[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine("Column #" + (i + 1));
                Console.WriteLine("\tRow Position: " + (queenRowLocationsAndFitness[i, 0]+1));
                Console.WriteLine("\tFitness: " + queenRowLocationsAndFitness[i, 1]);
                Console.WriteLine();
            }
        }
    }
}
