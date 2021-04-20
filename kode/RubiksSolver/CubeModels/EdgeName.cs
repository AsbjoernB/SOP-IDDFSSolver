using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.CubeModels
{
    public enum EdgeName
    {
        UF, UR, UB, UL, // top
        FR, BR, BL, FL, // equator
        DF, DR, DB, DL, // bottom
    }
}
