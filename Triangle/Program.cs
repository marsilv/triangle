using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Triangle
{
    class Program
    {
        private static int[,] readInput(string filename)
        {
            string line;
            string[] linePieces;
            int lines = 0;

            StreamReader r = new StreamReader(filename);
            while ((line = r.ReadLine()) != null)
            {
                lines++;
            }

            int[,] inputTriangle = new int[lines, lines];
            r.BaseStream.Seek(0, SeekOrigin.Begin);

            int j = 0;
            while ((line = r.ReadLine()) != null)
            {
                linePieces = line.Split(' ');
                for (int i = 0; i < linePieces.Length; i++)
                {
                    inputTriangle[j, i] = int.Parse(linePieces[i]);
                }
                j++;
            }
            r.Close();
            return inputTriangle;
        }

        static void Main(string[] args)
        {
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\input.txt";
            int[,] inputTriangle = readInput(filename);

            int posSolutions = (int)Math.Pow(2, inputTriangle.GetLength(0) - 1);
            int largestSum = 0;
            int tempSum, index;
            bool isEven, isPath;
            string bestPath = null;

            for (int i = 0; i <= posSolutions; i++)
            {
                isEven = inputTriangle[0, 0] % 2 == 0 ? true : false;
                isPath = true;
                tempSum = inputTriangle[0, 0];
                index = 0;
                string path = inputTriangle[0, 0].ToString();
                for (int j = 0; j < inputTriangle.GetLength(0) - 1; j++)
                {
                    index = index + (i >> j & 1);
                    int node = inputTriangle[j + 1, index];

                    isEven = !isEven;
                    bool isNodeEven = node % 2 == 0 ? true : false;
                    
                    if (isEven != isNodeEven)
                    {
                        isPath = false;
                        break; 
                    }
                    tempSum += node;
                    path += ", " + node;
                }
                if (isPath && tempSum > largestSum)
                {
                    largestSum = tempSum;
                    bestPath = path;
                }
            }

            Console.WriteLine("Max sum: " + largestSum);
            Console.WriteLine("Path: " + bestPath);
            Console.WriteLine("Press any key to end...");
            Console.ReadKey();
            

        }
    }
}
