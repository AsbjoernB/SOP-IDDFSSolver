using RubiksSolver.CubeModels;
using RubiksSolver.IDDFS;
using RubiksSolver.SolutionStages;
using System;
using System.Diagnostics;
using System.Linq;

namespace RubiksSolver
{
    class Program
    {

        static void Main(string[] args)
        {

            // creates a new solved cube, accepts a scramble and scrambles the cube.
            CubeState c = CubeState.solvedState;

            Console.WriteLine("Enter scramble (uppercase space-seperated):");
            string scramble = Console.ReadLine(); //"B F2 L B2 R B' F' D U L' B2 R B2 L2 U' L' U D2 B2 R'"; 

            c = CubePermuter.scramble(c, scramble);

            Console.WriteLine("starting state:");
            Console.WriteLine(c);


            // starts a timer to tell how lon the solution took to find.
            Stopwatch watch = Stopwatch.StartNew();

            // creates a string to store the solution
            string solution = "";

            // solves each step seperately and appends the partial solutions to the final solution
            IDDFSSolver solver = new IDDFSSolver();
            solution += solver.Solve(c, new ReductionToG1());
            solution += solver.Solve(c, new ReductionToG2());
            solution += solver.Solve(c, new ReductionToG3());
            solution += solver.Solve(c, new ReductionToE());

            watch.Stop();

            // prints solution
            int solutionLength = solution.Split().Count(x => !string.IsNullOrEmpty(x));
            Console.WriteLine("Final solution (" + solutionLength + " moves):");
            Console.WriteLine(solution);

            // returns stats about the solve
            Console.WriteLine("time elapsed: " + watch.Elapsed);
            Console.WriteLine("total moves: " + IDDFSSolver.moveTotal);
            Console.WriteLine("average moves per second: " + (float)IDDFSSolver.moveTotal / watch.ElapsedMilliseconds * 1000);

            // prevents the console from closing before the user closes it or preses enter.
            Console.WriteLine("\nPress Enter to close...");
            Console.ReadLine();
        }
    }
}
