using RubiksSolver.CubeModels;
using RubiksSolver.SolutionStages;
using System;
using System.Diagnostics;

namespace RubiksSolver.IDDFS
{
    public class IDDFSSolver
    {
        /// <summary>
        /// The ReductionStep. Sets the rules for what moves to do and when a state is "solved"
        /// </summary>
        ReductionStep reductionStep;
        /// <summary>
        /// Move count for this instance. Used for stats
        /// </summary>
        private int moveCount = 0;
        /// <summary>
        /// Total movecount from all instances of IDDFSSolver. Used for stats
        /// </summary>
        public static int moveTotal = 0;

        /// <summary>
        /// Uses IDDFS and DLS to solve the cube. 
        /// </summary>
        /// <param name="cubestate">The starting cubestate. The solved cubestate will be assigned to this variable.</param>
        /// <param name="reductionStep">The ReductionStep to adhere to.</param>
        /// <param name="maxdepth">The max depth to search. Is int.MaxValue (essentially infinite) by default</param>
        /// <param name="verbose">Whether the method should write to console</param>
        /// <returns>Returns the solution as a string</returns>
        public string Solve(CubeState cubestate, ReductionStep reductionStep, int maxdepth = int.MaxValue, bool verbose = true)
        {
            if (verbose)
                Console.WriteLine("\nsolving...");

            // initializes the solver
            this.reductionStep = reductionStep;
            moveCount = 0;

            string output = "";

            // creates a root node
            IDDFSNode node = new IDDFSNode(cubestate, Move.NONE, null);

            bool IsSolved = false;

            // starts a stopwatch. This is for stats
            Stopwatch sw = Stopwatch.StartNew();

            // does IDDFS at increasing depths until maxdepth
            for (int depth = 0; depth <= maxdepth; depth++)
            {
                if (verbose)
                    Console.WriteLine("depth: " + depth);

                // runs DLS to get a result containing whether the goal was found and what node is the goal. result is a tuple. Item1 is the bool and Item2 is the IDDFSNode
                (bool, IDDFSNode) result = DLS(node, depth);

                // is the goal was find, node is set to the goal and IDDFS ends. otherwise IDDFS continues at higher depths.
                if (result.Item1 == true)
                {
                    node = result.Item2;
                    IsSolved = true;
                    break;
                }
            }

            // stops the stopwatch
            sw.Stop();

            if (IsSolved)
            {
                // sets the original cubestate to the goal cubestate. This way the caller can proceed with solving the other steps with the new state
                cubestate.state = node.state.state;

                // prints stats and finds the moves done to find the goal only if verbose is true.
                // NOTE: this isn't great practice, as the return value depends on this, otherwise it'll just return "\n".
                // However, the only time this is called with verbose=false is to check HTR-parity, which happens many times a second
                // so it's just an optimization.
                if (verbose)
                {
                    Console.WriteLine("Solved! Solution took: " + sw.Elapsed);

                    // goes backwards through the chain of references from node until the starting node and adds the move to the start of the output string.
                    while (node.previousNode != null)
                    {
                        output = node.move + " " + output;
                        node = node.previousNode;
                    }
                    output += "\n";

                    // prints stats
                    output = output.Replace("p", "'");
                    Console.WriteLine("solution: " + output);
                    Console.WriteLine("moveCount: " + moveCount + " (" + (float)moveCount / sw.ElapsedMilliseconds * 1000 + " per second)");
                    Console.WriteLine("state after solving:");
                    Console.WriteLine(node.state);
                    Console.WriteLine();

                    moveTotal += moveCount;
                }
            }
            else
            {
                return null;
            }

            return output;
        }


        /// <summary>
        /// Does Depth-Limited Search with a given depth starting at a given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        (bool, IDDFSNode) DLS(IDDFSNode node, int depth)
        {
            // if the depth is 0 it's at the "last" node in the chain, and it should check if it's solved according to the ReductionStep
            // if it is, it returns true which unwinds the recursion
            if (depth <= 0)
            {
                if (reductionStep.isReduced(node.state))
                {
                    return (true, node);
                }
                return (false, null);
            }

            // Loops through all the allowed moves as descibed by ReductionStep and generates a cubestate for each of them.
            // In other words it discovers every child node of the current node.
            foreach (Move move in reductionStep.allowedMoves)
            {
                // purges some moves to avoid searching redundant states (such as doing R R', etc.)
                if (!purge(node, move))
                {
                    moveCount++;

                    // creates the new node
                    IDDFSNode newnode = new IDDFSNode(CubePermuter.permute(node.state, move), move, node);

                    // calls DLS at the next depth. If it returns true, this DLS also returns true, and so on. This unwinds the recursion.
                    (bool, IDDFSNode) result = DLS(newnode, depth - 1);
                    if (result.Item1 == true)
                    {
                        return result;
                    }
                }
            }

            // returns false if the goal wasn't found.
            return (false, null);
        }

        /// <summary>
        /// purges moves depending on the previous moves that were done. Avoids (most) redundant states.
        /// Essentially any move will be purged if the previous move was on the same face.
        /// If commutative moves have been done (moves on opposite faces), a third move on either face shouldn't happen. 
        /// </summary>
        /// <param name="node">The node to check moves from</param>
        /// <param name="move">The move that is about to be done</param>
        /// <returns>Returns whether the move should be skipped or not</returns>
        static bool purge(IDDFSNode node, Move move)
        {
            // move is the move that is being checked. node.move is the previous move
            switch (move)
            {
                case Move.Rp:
                    goto case Move.R;
                case Move.R2:
                    goto case Move.R;
                case Move.R:
                    if (node.move == Move.R ||
                        node.move == Move.R2 ||
                        node.move == Move.Rp)
                        return true;
                    break;

                case Move.Lp:
                    goto case Move.L;
                case Move.L2:
                    goto case Move.L;
                case Move.L:
                    if (node.move == Move.R ||
                        node.move == Move.R2 ||
                        node.move == Move.Rp ||

                        // purges commutative moves
                        node.move == Move.L ||
                        node.move == Move.L2 ||
                        node.move == Move.Lp)
                        return true;
                    break;




                case Move.Fp:
                    goto case Move.F;
                case Move.F2:
                    goto case Move.F;
                case Move.F:
                    if (node.move == Move.F ||
                        node.move == Move.F2 ||
                        node.move == Move.Fp)
                        return true;
                    break;

                case Move.Bp:
                    goto case Move.B;
                case Move.B2:
                    goto case Move.B;
                case Move.B:
                    if (node.move == Move.B ||
                        node.move == Move.B2 ||
                        node.move == Move.Bp ||

                        // purges commutative moves
                        node.move == Move.F ||
                        node.move == Move.F2 ||
                        node.move == Move.Fp)
                        return true;
                    break;


                case Move.Up:
                    goto case Move.U;
                case Move.U2:
                    goto case Move.U;
                case Move.U:
                    if (node.move == Move.U ||
                        node.move == Move.U2 ||
                        node.move == Move.Up)
                        return true;
                    break;

                case Move.Dp:
                    goto case Move.D;
                case Move.D2:
                    goto case Move.D;
                case Move.D:
                    if (node.move == Move.D ||
                        node.move == Move.D2 ||
                        node.move == Move.Dp ||

                        // purges commutative moves
                        node.move == Move.U ||
                        node.move == Move.U2 ||
                        node.move == Move.Up)
                        return true;
                    break;
            }
            return false;
        }
    }

}
