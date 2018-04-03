using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAFourQueens
{
    class Board
    {
        private struct QueenLocation {
            public int row { get; set; }
            public int col { get; set; }

            public double fitness { get; set; }
        }

        private static readonly Random rdm = new Random();

        private int boardSize = 4;
        private int[,] board;
        private QueenLocation[] queenSpots;
        private int numPopulation;

        public Board()
        {
            board = new int[boardSize, boardSize];
            queenSpots = new QueenLocation[boardSize];
            numPopulation = 1;

            setInitialQueenLocations();
            computeFitness();
            printQueenLocations();

            //crossover();
           // mutation();
            //updateQueenLocations();
            //computeFitness();
            Console.Read();
        }

        private void computeFitness()
        {
            for(int i = 0; i < queenSpots.Length; i++)
            {
                int curRow = queenSpots[i].row;
                int curCol = queenSpots[i].col;
                int curClashes = 0;

                for(int j = 0; j < queenSpots.Length; j++)
                {
                    // making sure the two queens in comparison are not the same queen
                    if(i != j)
                    {
                        // checking to see if both queens are in the same row or column, increment clashes if so
                        if (curRow == queenSpots[j].row || curCol == queenSpots[j].col)
                            curClashes++;
                        else
                        {
                            int rowDiff = Math.Abs(curRow - queenSpots[j].row);
                            int colDiff = Math.Abs(curCol - queenSpots[j].col);

                            // checking diagonals
                            if (rowDiff <= 1 && colDiff <= 1)
                                curClashes++;
                        }
                    }
                }
                // calculates fitness for current queen
                queenSpots[i].fitness = 3 - curClashes;
            } 
        }

        private void crossover()
        {
            int[] rdmRows = { 0, 1, 2, 3 };
            shuffle(rdmRows);

            int rdmCrossPt = rdm.Next(1, 4);
            crossWithPartner(rdmRows[0], rdmRows[1], rdmCrossPt);

            rdmCrossPt = rdm.Next(1, 4);
            crossWithPartner(rdmRows[2], rdmRows[3], rdmCrossPt);
        }

        private void crossWithPartner(int row1, int row2, int crossPoint)
        {
            for(int i = crossPoint; i < 4; i++)
            {
                swap(board[row1, i], board[row2, i]);
            }
        }

        private void swap<T>(T a, T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private void mutation()
        {
            double mutationProbability = 0.1;

            // calculating positions for mutations
            for(int i = 0; i < 4; i++)
            {
                double possibleMut = rdm.NextDouble();
                if (possibleMut <= mutationProbability) {
                    int queenRowCount = 0;

                    for(int j = 0; j < 4; j++)
                    {
                        if (board[i, j] == 1)
                            queenRowCount++;
                        board[i, j] = 0;
                    }

                    while(queenRowCount > 0)
                    {
                        int mutPosition = rdm.Next(0, 3);
                        // checking to see if position has already been mutated
                        if(board[i, mutPosition] != 1)
                        {
                            board[i, mutPosition] = 1;
                            queenRowCount--;
                        }
                    }
                }
            }
        }

        // shuffling function for randomly selecting mating row pairs
        private void shuffle(int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                int index = rdm.Next(0, 4);

                swap(arr[i], arr[index]);
            }
        }

        private void updateQueenLocations()
        {
            int queenCounter = 0;

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(board[i, j] == 1)
                    {
                        queenSpots[queenCounter].row = i;
                        queenSpots[queenCounter].col = j;
                        queenCounter++;
                    }
                }
                if (queenCounter == 4)
                    break;
            }
        }

        private void setInitialQueenLocations()
        {
            for(int i = 0; i < 4; i++)
            {
                int col;
                int row;

                // making sure no two Queens reside in the same location
                do {
                    col = rdm.Next(0, 4);
                    row = rdm.Next(0, 4);
                } while (board[row, col] == 1);

                board[row, col] = 1;

                QueenLocation ql = new QueenLocation();
                ql.row = row;
                ql.col = col;
                queenSpots[i] = ql;
            }
        }

        public void printQueenLocations()
        {
            for(int i = 0; i < boardSize; i++)
            {
                for(int j = 0; j < boardSize; j++)
                {
                    Console.Write(board[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            foreach(QueenLocation q in queenSpots)
            {
                int actualRow = q.row + 1;
                int actualCol = q.col + 1;
                Console.WriteLine(actualRow + ", " + actualCol);
                Console.WriteLine("Fitness: " + q.fitness);
            }
        }
    }
}
