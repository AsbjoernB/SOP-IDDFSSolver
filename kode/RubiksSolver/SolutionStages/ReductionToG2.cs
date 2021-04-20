using RubiksSolver.CubeModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.SolutionStages
{
    class ReductionToG2 : ReductionStep
    {
        private static Move[] _allowedMoves = new Move[]
        {
            Move.U, Move.U2, Move.Up,
            Move.F2,
            Move.R, Move.R2, Move.Rp,
            Move.B2,
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

        public override int getHeuristic(CubeState cubeState)
        {
            throw new NotImplementedException();
        }

        // the 4 edges in the equator slice. It's all the edges not in the top or bottom layer
        private static readonly EdgeName[] equatorEdges = new EdgeName[]
        {
            EdgeName.FR,
            EdgeName.BR,
            EdgeName.BL,
            EdgeName.FL,
        };

        /// <summary>
        /// The goal is to do Domino Reduction, meaning the only needed moves on F, B, L, and R are double moves.
        /// It checks whether all the top and bottom colors are white and yellow and whether the equator edges are in the equator slice
        /// </summary>
        public override bool isReduced(CubeState cubeState)
        {
            // checks if the edges in the equator have white or yellow
            foreach (EdgeName edgeName in equatorEdges)
            {
                Edge e = new Edge(cubeState, edgeName);

                if (e.colors.Values.Contains(Color.W) || e.colors.Values.Contains(Color.Y))
                    return false;
            }
            
            // top corner colors. The edges don't need to be checked as they'll be oriented from the previous step,
            // so as long as they aren't equator edges they're correct
            for (int i = 0; i < 8; i += 2)
            {
                if (cubeState.state[i] != Color.W && cubeState.state[i] != Color.Y)
                    return false;
            } 
            // bottom corner colors. Same rules apply.
            for (int i = 40; i < 48; i += 2)
            {
                if (cubeState.state[i] != Color.W && cubeState.state[i] != Color.Y)
                    return false;
            }
            
            return true;

        }
    }
}
