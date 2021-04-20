using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.CubeModels
{
    /// <summary>
    /// Holds the state of a cube.
    /// </summary>
    public class CubeState
    {
        /// <summary>
        /// The color array containing the position of every color on the cube.
        /// </summary>
        public Color[] state;

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="state">a color array that represents a cube. See "cube number reference.png" for reference</param>
        public CubeState(Color[] state)
        {
            this.state = new Color[48];
            Array.Copy(state, this.state, state.Length);
        }

        /// <summary>
        /// The solved state consists of every color placed correctly.
        /// </summary>
        public static CubeState solvedState
        {
            get
            {
                return new CubeState(
                    new Color[48]
                    {
                        Color.W, Color.W, Color.W, Color.W, Color.W, Color.W, Color.W, Color.W,
                        Color.G, Color.G, Color.G, Color.G, Color.G, Color.G, Color.G, Color.G,
                        Color.R, Color.R, Color.R, Color.R, Color.R, Color.R, Color.R, Color.R,
                        Color.B, Color.B, Color.B, Color.B, Color.B, Color.B, Color.B, Color.B,
                        Color.O, Color.O, Color.O, Color.O, Color.O, Color.O, Color.O, Color.O,
                        Color.Y, Color.Y, Color.Y, Color.Y, Color.Y, Color.Y, Color.Y, Color.Y
                    });
            }
        }

        /// <summary>
        /// When printing the cubestate, it's formatted to look like an unfolded cube.
        /// This formatting is reflected in the code, though I'll admit it's hard to read.
        /// </summary>
        public override string ToString()
        {
            return
                "\n " +
                "    " + state[0] + state[1] + state[2] + "\n " +
                "    " + state[7] +    " "   + state[3] + "\n " +
                "    " + state[6] + state[5] + state[4] + "\n " +

                state[32] + state[33] + state[34] + " " + state[8]  +  state[9] + state[10] + " " + state[16] + state[17] + state[18] + " " + state[24] + state[25] + state[26] + "\n " +
                state[39] +    " "    + state[35] + " " + state[15] +    " "    + state[11] + " " + state[23] +    " "    + state[19] + " " + state[31] +    " "    + state[27] + "\n " +
                state[38] + state[37] + state[36] + " " + state[14] + state[13] + state[12] + " " + state[22] + state[21] + state[20] + " " + state[30] + state[29] + state[28] + "\n " +

                "    " + state[40] + state[41] + state[42] + "\n " +
                "    " + state[47] +    " "    + state[43] + "\n " +
                "    " + state[46] + state[45] + state[44] + "\n "
                ;
        }

    }

}
