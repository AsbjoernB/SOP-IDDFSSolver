using RubiksSolver.CubeModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.SolutionStages
{
    class ReductionToE : ReductionStep
    {
        private static Move[] _allowedMoves = new Move[]
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
        /// Checking is a cube is solved is very easy, as everything has to be placed correctly.
        /// </summary>
        public override bool isReduced(CubeState cubeState)
        {
            // only the first 40 colors are checked. This is the first 5 sides. If 5 sides are solved, all 6 are (near-insignificant optimization)
            for (int i = 0; i < 40; i++)
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
