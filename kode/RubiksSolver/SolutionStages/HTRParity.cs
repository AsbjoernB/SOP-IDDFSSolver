using RubiksSolver.CubeModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.SolutionStages
{
    /// <summary>
    /// Solves just the corners after Half-Turn Reductiong.
    /// This is the easiest way to check the parity (two swapped corners can't be solved with only double moves) that I could think of.
    /// Any corner state can be solved in just 4 moves during HTR if it is valid.
    /// </summary>
    class HTRParity : ReductionStep
    {

        private Move[] _allowedMoves = new Move[]
        {
            Move.U2,
            Move.F2,
            Move.R2,
            Move.B2,
            Move.L2,
            Move.D2,
        };

        public override Move[] allowedMoves
        {
            get
            {
                return _allowedMoves;
            }
        }

        /// <summary>
        /// This check is nearly the same a <see cref="ReductionToE"/>, except only the corners are relevant, so only every other color is checked. 
        /// </summary>
        public override bool isReduced(CubeState cubeState)
        {
            for (int i = 0; i < 40; i += 2)
            {
                if (cubeState.state[i] != (Color)(i / 8))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
