using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class Knapsack
    {
        private static int KNAPSACK_CAPACITY = 1000;
        private static int ALL_ITEMS_COUNT = 100;
        private static int MAX_UNIMPROVED_ITERATIONS = 100;
        private static int MIN_WEIGHT = 5;
        private static int MAX_WEIGHT = 50;
        private int[] itemsWeights = new int[ALL_ITEMS_COUNT];
        private bool[] genome = new bool[ALL_ITEMS_COUNT];
        private int genomeScore;
        private Random rand = new Random();

        public Knapsack()
        {
            for(int i = 0; i < ALL_ITEMS_COUNT; i++)
            {
                itemsWeights[i] = rand.Next(MIN_WEIGHT, MAX_WEIGHT+1);
                genome[i] = rand.Next() % 2 == 0;
            }

            genomeScore = Score(genome);
        }

        public void Pack()
        {
            PrintWeights();
            PrintFirstGenome();

            int mutationsCount = 0;
            int unimprovedMutationsCount = 0;

            bool[] mutatedGenome;
            int mutatedGenomeScore;
            while (unimprovedMutationsCount < MAX_UNIMPROVED_ITERATIONS)
            {
                mutationsCount++;
                mutatedGenome = Mutate();
                mutatedGenomeScore = Score(mutatedGenome);

                if (IsBetter(mutatedGenomeScore))
                {
                    genome = mutatedGenome;
                    genomeScore = mutatedGenomeScore;
                }
                else
                {
                    unimprovedMutationsCount++;
                }
            }

            PrintBestGenome();
            PrintSummary(mutationsCount);
        }

        public bool IsBetter(int mutatedGenomeScore)
        {
            if(genomeScore > KNAPSACK_CAPACITY)
            {
                return mutatedGenomeScore < genomeScore;
            }
            if(mutatedGenomeScore <= KNAPSACK_CAPACITY)
            {
                return mutatedGenomeScore > genomeScore;
            }
            return false;
        }

        public int Score(bool[] genomeToScore)
        {
            int sum = 0;
            for(int i = 0; i < ALL_ITEMS_COUNT; i++)
            {
                if(genomeToScore[i])
                {
                    sum += itemsWeights[i];
                }
            }

            return sum;
        }

        public bool[] Mutate()
        {
            bool[] newGenome = (bool[])genome.Clone();
            int index = rand.Next(0, ALL_ITEMS_COUNT);
            newGenome[index] = !newGenome[index];
            return newGenome;
        }

        private static void PrintSummary(int mutationsCount)
        {
            Console.WriteLine("Total Iterations: " + mutationsCount);
            Console.WriteLine("Best after: " + (mutationsCount - MAX_UNIMPROVED_ITERATIONS));
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void PrintBestGenome()
        {
            Console.WriteLine("Best genome: \n" + string.Join("|", genome.ToList().Select(i => i ? '+' : '-')));
            Console.WriteLine("Score: " + genomeScore);
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void PrintFirstGenome()
        {
            Console.WriteLine("First genome: \n" + string.Join("|", genome.ToList().Select(i => i ? '+' : '-')));
            Console.WriteLine("Score: " + genomeScore);
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void PrintWeights()
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("Weights: \n" + string.Join("|", itemsWeights));
            Console.WriteLine("--------------------------------------------------------------------------------");
        }
    }
}