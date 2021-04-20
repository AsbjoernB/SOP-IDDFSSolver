using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.CubeModels
{
    /// <summary>
    /// Represents a corner cubie. Taking a cubestate and a position, it'll create a dictionary with faces as keys and colors as values
    /// NOTE: In the end this model wasn't used. Any use it had could be avoided with optimization. The same is probably true for the Edge.
    /// </summary>
    public struct Corner
    {
        public Dictionary<FaceName, Color> colors;
        public CornerName position;

        public Corner(CubeState cubeState, CornerName cornerPosition)
        {
            position = cornerPosition;

            // Switch statement that populates the dictionary depending on the cubie position
            switch (cornerPosition)
            {
                case CornerName.UFR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[4],
                        [FaceName.Green] = cubeState.state[10],
                        [FaceName.Red] = cubeState.state[16],
                    };
                    break;

                case CornerName.UBR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[2],
                        [FaceName.Blue] = cubeState.state[24],
                        [FaceName.Red] = cubeState.state[18],
                    };
                    break;

                case CornerName.UBL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[0],
                        [FaceName.Blue] = cubeState.state[26],
                        [FaceName.Orange] = cubeState.state[32],
                    };
                    break;

                case CornerName.UFL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.White] = cubeState.state[6],
                        [FaceName.Green] = cubeState.state[8],
                        [FaceName.Orange] = cubeState.state[34],
                    };
                    break;


                case CornerName.DFR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[42],
                        [FaceName.Green] = cubeState.state[12],
                        [FaceName.Red] = cubeState.state[22],
                    };
                    break;

                case CornerName.DBR:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[44],
                        [FaceName.Blue] = cubeState.state[30],
                        [FaceName.Red] = cubeState.state[20],
                    };
                    break;

                case CornerName.DBL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[46],
                        [FaceName.Blue] = cubeState.state[28],
                        [FaceName.Orange] = cubeState.state[38],
                    };
                    break;

                case CornerName.DFL:
                    colors = new Dictionary<FaceName, Color>()
                    {
                        [FaceName.Yellow] = cubeState.state[40],
                        [FaceName.Green] = cubeState.state[14],
                        [FaceName.Orange] = cubeState.state[36],
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
