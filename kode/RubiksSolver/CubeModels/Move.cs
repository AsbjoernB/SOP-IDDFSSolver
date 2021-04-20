using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.CubeModels
{
    /// <summary>
    /// enum of all the possible moves on a cube, used to identify moves.
    /// </summary>
    public enum Move
    {
        U, U2, Up, // U
        F, F2, Fp, // F
        R, R2, Rp, // R
        B, B2, Bp, // B
        L, L2, Lp, // L
        D, D2, Dp, // D
        NONE // used for the first node in searching algorithms to avoid including a random move to the solution
    }
}
