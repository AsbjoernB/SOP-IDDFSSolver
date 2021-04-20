using RubiksSolver.CubeModels;
using RubiksSolver.IDDFS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.SolutionStages
{
    class ReductionToG3 : ReductionStep
    {
        private static Move[] _allowedMoves = new Move[]
        {
            Move.U, Move.U2, Move.Up,
            Move.F2,
            Move.R2,
            Move.B2,
            Move.L2,
            Move.D, Move.D2, Move.Dp,
        };

        public override Move[] allowedMoves
        {
            get
            {
                return _allowedMoves;
            }
        }

        /// <summary>
        /// The goal is to do Half-Turn Reduction. a cubestate in HTR will only have the correct or opposite color on each side
        /// </summary>
        public override bool isReduced(CubeState cubeState)
        {
            for (int i = 0; i < 48; i++)
            {
                switch (i / 8)
                {
                    // white
                    case 0:
                        if (cubeState.state[i] != Color.W && cubeState.state[i] != Color.Y)
                            return false;
                        break;

                    // green
                    case 1:
                        if (cubeState.state[i] != Color.G && cubeState.state[i] != Color.B)
                            return false;
                        break;

                    // orange
                    case 2:
                        if (cubeState.state[i] != Color.O && cubeState.state[i] != Color.R)
                            return false;
                        break;

                    // blue
                    case 3:
                        if (cubeState.state[i] != Color.G && cubeState.state[i] != Color.B)
                            return false;
                        break;

                    // red
                    case 4:
                        if (cubeState.state[i] != Color.O && cubeState.state[i] != Color.R)
                            return false;
                        break;

                    // yellow
                    case 5:
                        if (cubeState.state[i] != Color.W && cubeState.state[i] != Color.Y)
                            return false;
                        break;
                }
            }

            // if two corners are swapped, it's impossible to solve with only double moves.
            // Without a good way to check if two corners are swapped, an IDDFS is run to solve just the corners.
            // Every corner state can be solved in 4 moves or less, so maxDepth is set to 4.

            IDDFSSolver parityCheckSolver = new IDDFSSolver();
            if (parityCheckSolver.Solve(new CubeState(cubeState.state), new HTRParity(), 4, false) != null)
                return true;

            return false;
        }
    }
}
