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
    /// This stage is the first. The goal is to solve edge orientation.
    /// </summary>
    class ReductionToG1 : ReductionStep
    {
        /// <summary>
        /// The initial scramble can be anything, so every move is allowed
        /// </summary>
        private static Move[] _allowedMoves = new Move[]
        {
            Move.U, Move.U2, Move.Up,
            Move.F, Move.F2, Move.Fp,
            Move.R, Move.R2, Move.Rp,
            Move.B, Move.B2, Move.Bp,
            Move.L, Move.L2, Move.Lp,
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
        /// The goal is to make every edge be oriented correctly so F, F', B and B' are no longer needed.
        /// For a reference for how to identify Edge orientation, see "EO reference.png" ("good" means oriented, "bad" means misoriented)
        /// </summary>
        public override bool isReduced(CubeState cubeState)
        {
            // loops through every edge
            for (int i = 0; i < 12; i++)
            {
                Edge e = new Edge(cubeState, (EdgeName)i);

                foreach (FaceName f in e.colors.Keys)
                {
                    // if side color is on top or bottom
                    if (f == FaceName.White || f == FaceName.Yellow)
                    {
                        if (e.colors[f] == Color.O ||
                            e.colors[f] == Color.R)
                            return false;
                    }
                    // if top or bottom color is on side
                    if (f == FaceName.Orange || f == FaceName.Red)
                    {
                        if (e.colors[f] == Color.W ||
                            e.colors[f] == Color.Y)
                            return false;
                    }
                }

                // if M-slice edges are wrong
                if (e.position == EdgeName.UF || e.position == EdgeName.DF)
                    if (e.colors[FaceName.Green] == Color.W || e.colors[FaceName.Green] == Color.Y)
                        return false;
                if (e.position == EdgeName.UB || e.position == EdgeName.DB)
                    if (e.colors[FaceName.Blue] == Color.W || e.colors[FaceName.Blue] == Color.Y)
                        return false;

                // if E-slice edges are wrong
                if (e.position == EdgeName.FR || e.position == EdgeName.FL)
                    if (e.colors[FaceName.Green] == Color.O || e.colors[FaceName.Green] == Color.R)
                        return false;
                if (e.position == EdgeName.BR || e.position == EdgeName.BL)
                    if (e.colors[FaceName.Blue] == Color.O || e.colors[FaceName.Blue] == Color.R)
                        return false;

            }

            return true;
        }
    }
}
