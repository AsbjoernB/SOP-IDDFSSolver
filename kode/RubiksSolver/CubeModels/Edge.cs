using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.CubeModels
{
    /// <summary>
    /// Represents a corner cubie. Taking a cubestate and a position, it'll create a dictionary with faces as keys and colors as values.
    /// </summary>
    public struct Edge
    {
        public Dictionary<FaceName, Color> colors;
        public EdgeName position;

        //private EdgeNames getSolvedPosition { get; };

        public Edge(CubeState cubeState, EdgeName edgePosition)
        {
            position = edgePosition;

            // Switch statement that populates the dictionary depending on the cubie position
            switch (edgePosition)
            {
                case EdgeName.UF:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[5],
                        [FaceName.Green] = cubeState.state[9],
                    };
                    break;
                case EdgeName.UR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[3],
                        [FaceName.Red] = cubeState.state[17],
                    };
                    break;
                case EdgeName.UB:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[1],
                        [FaceName.Blue] = cubeState.state[25],
                    };
                    break;
                case EdgeName.UL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[7],
                        [FaceName.Orange] = cubeState.state[33],
                    };
                    break;
                case EdgeName.FR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Green] = cubeState.state[11],
                        [FaceName.Red] = cubeState.state[23],
                    };
                    break;
                case EdgeName.BR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Blue] = cubeState.state[31],
                        [FaceName.Red] = cubeState.state[19],
                    };
                    break;
                case EdgeName.BL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Blue] = cubeState.state[27],
                        [FaceName.Orange] = cubeState.state[39],
                    };
                    break;
                case EdgeName.FL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Green] = cubeState.state[15],
                        [FaceName.Orange] = cubeState.state[35],
                    };
                    break;
                case EdgeName.DF:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[41],
                        [FaceName.Green] = cubeState.state[13],
                    };
                    break;
                case EdgeName.DR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[43],
                        [FaceName.Red] = cubeState.state[21],
                    };
                    break;
                case EdgeName.DB:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[45],
                        [FaceName.Blue] = cubeState.state[29],
                    };
                    break;
                case EdgeName.DL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[45],
                        [FaceName.Orange] = cubeState.state[37],
                    };
                    break;

                // won't ever happen
                default:
                    colors = null;
                    break;
            }

        }
    }
}
