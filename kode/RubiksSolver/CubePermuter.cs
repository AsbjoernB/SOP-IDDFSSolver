using RubiksSolver.CubeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver
{
    public static class CubePermuter
    {
        /// <summary>
        /// Defines the position of the colors around the U face
        /// </summary>
        private static int[] U = new int[12]
        {
            34, 33, 32,
            26, 25, 24,
            18, 17, 16,
            10, 9, 8
        };

        /// <summary>
        /// Defines the position of the colors around the F face
        /// </summary>
        private static int[] F = new int[12]
        {
            16, 23, 22,
            42, 41, 40,
            36, 35, 34,
            6, 5, 4

        };

        /// <summary>
        /// Defines the position of the colors around the R face
        /// </summary>
        private static int[] R = new int[12]
        {
            24, 31, 30,
            44, 43, 42,
            12, 11, 10,
            4, 3, 2

        };

        /// <summary>
        /// Defines the position of the colors around the B face
        /// </summary>
        private static int[] B = new int[12]
        {
            32, 39, 38,
            46, 45, 44,
            20, 19, 18,
            2, 1, 0

        };

        /// <summary>
        /// Defines the position of the colors around the L face
        /// </summary>
        private static int[] L = new int[12]
        {
            8, 15, 14,
            40, 47, 46,
            28, 27, 26,
            0, 7, 6
        };

        /// <summary>
        /// Defines the position of the colors around the D face
        /// </summary>
        private static int[] D = new int[12]
        {
            22, 21, 20,
            30, 29, 28,
            38, 37, 36,
            14, 13, 12,
        };

        /// <summary>
        /// Wraps an integer around between the min and max value
        /// </summary>
        /// <returns></returns>
        private static int wrap(int val, int min, int max)
        {
            if (val > max)
                return min + (val - max) - 1;
            if (val < min)
                return max - (min - val) + 1;
            return val;
        }

        /// <summary>
        /// Permutes a cubestate.
        /// </summary>
        /// <param name="cubeState">The cubestate to permute</param>
        /// <param name="moveName">The name of the move to use</param>
        /// <returns>the cubestate after permuting the original cubestate</returns>
        public static CubeState permute(CubeState cubeState, Move moveName)
        {
            // index of the face to turn. 0:U, 1:F, 2:R, 3:B, 4:L, 5:D. This is used to turn the colors on the face
            int faceIndex = 0;
            // which "direction" to do the move. 1 is normal, -1 is prime and 2 is a double move.
            int moveSign = 1;
            // the list to use to permute the colors.
            int[] move;

            // this giant switch statemnt sets the previous values. Switch lookups are very fast (between O(1) and O(n))
            // This method will be called millions of times per second, so it's not too bad.
            switch (moveName)
            {
                case Move.U:
                    move = U;
                    faceIndex = 0;
                    break;
                case Move.Up:
                    move = U;
                    moveSign = -1;
                    faceIndex = 0;
                    break;
                case Move.U2:
                    move = U;
                    moveSign = 2;
                    faceIndex = 0;
                    break;

                case Move.F:
                    move = F;
                    faceIndex = 1;
                    break;
                case Move.Fp:
                    move = F;
                    moveSign = -1;
                    faceIndex = 1;
                    break;
                case Move.F2:
                    move = F;
                    moveSign = 2;
                    faceIndex = 1;
                    break;

                case Move.R:
                    move = R;
                    faceIndex = 2;
                    break;
                case Move.Rp:
                    move = R;
                    moveSign = -1;
                    faceIndex = 2;
                    break;
                case Move.R2:
                    move = R;
                    moveSign = 2;
                    faceIndex = 2;
                    break;

                case Move.B:
                    move = B;
                    faceIndex = 3;
                    break;
                case Move.Bp:
                    move = B;
                    moveSign = -1;
                    faceIndex = 3;
                    break;
                case Move.B2:
                    move = B;
                    moveSign = 2;
                    faceIndex = 3;
                    break;

                case Move.L:
                    move = L;
                    faceIndex = 4;
                    break;
                case Move.Lp:
                    move = L;
                    moveSign = -1;
                    faceIndex = 4;
                    break;
                case Move.L2:
                    move = L;
                    moveSign = 2;
                    faceIndex = 4;
                    break;

                case Move.D:
                    move = D;
                    faceIndex = 5;
                    break;
                case Move.Dp:
                    move = D;
                    moveSign = -1;
                    faceIndex = 5;
                    break;
                case Move.D2:
                    move = D;
                    moveSign = 2;
                    faceIndex = 5;
                    break;

                // shouldn't happen
                default:
                    Color[] cloneArr = new Color[48];
                    Array.Copy(cubeState.state, 0, cloneArr, 0, 48);
                    return new CubeState(cloneArr);
            }

            // copies the input state to a new array
            Color[] newState = new Color[48];
            Array.Copy(cubeState.state, 0, newState, 0, 48);

            // this next part shifts the elements (it permutes the cube).

            /// for the colors on the side of the layer

            // how long the colors on the side of the layer should move
            int shift = 3 * moveSign;
            // moves each element manually. Because of the way each position is numbered, there's not a good way of using Array.Copy,
            // The elements have to be shifted around manually one at a time
            for (int i = 0; i < 12; i++)
            {
                // This takes the number from the move array at i and gets the color at that position in the original cubestate
                // and then copies it to the new cubestate shifted "shift" amount of places around the face.
                // wrap makes i go around the move array and not out of bounds
                newState[move[wrap(i + shift, 0, 11)]] = cubeState.state[move[i]];
            }

            /// for the colors on the face. This action could be done the same way as on the side,
            /// but since all the elements on the face are right after eachother in the array
            /// it's faster to use Array.Copy to move them in bulk with just 2 actions.

            // figures out how long each color on the face should be shifted
            shift = 2 * moveSign;
            // for prime moves
            if (moveSign == -1)
            {
                // Array.Copy parameters are: sourceArray, sourceIndex, targetArray, targetIndex, length
                Array.Copy(cubeState.state, faceIndex * 8, newState, (faceIndex+1) * 8 + shift, -shift);
                Array.Copy(cubeState.state, faceIndex * 8 - shift, newState, faceIndex * 8, 8 + shift);
            }
            // for non-prime moves
            else
            {
                Array.Copy(cubeState.state, faceIndex * 8, newState, faceIndex * 8 + shift, 8 - shift);
                Array.Copy(cubeState.state, (faceIndex+1) * 8 - shift, newState, faceIndex * 8, shift);
            }


            
            // returns the new cubestate after the permutation
            return new CubeState(newState);
        }

        /// <summary>
        /// Used for scrambling the cube
        /// </summary>
        /// <param name="cubeState">The cubestate to permute. Would usually be the solved state</param>
        /// <param name="scramble">The scramble as a string</param>
        /// <returns>returns a new cubestate after the scramble</returns>
        public static CubeState scramble(CubeState cubeState, string scramble)
        {
            // splits every move in the scramble
            string[] list = scramble.Replace("'", "p").Split(' ');

            foreach (string str in list)
            {

                Move move;
                // tries to parse the string as a Move enum, and if successful, permutes the cube
                if (Enum.TryParse(str, out move))
                    cubeState = permute(cubeState, move);
                // if it can't be parsed, it'll throw an error
                else
                    throw new ArgumentException("Please enter a valid scramble");
            }
            // returns the scrambled cubestate
            return cubeState;
        }
    }
}

