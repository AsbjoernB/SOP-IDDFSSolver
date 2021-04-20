using RubiksSolver.CubeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksSolver.IDDFS
{
    /// <summary>
    /// Represents a node in the Iterative Deepening Depth First Search algorithm. It holds a cubestate, the move that was done last to get to the node and the preivous node.
    /// </summary>
    public class IDDFSNode
    {
        /// <summary>
        /// The cubesstate in this node.
        /// </summary>
        public readonly CubeState state;
        /// <summary>
        /// Reference to the previous node. This is used to unwind the IDDFS recursion
        /// </summary>
        public readonly IDDFSNode previousNode;
        /// <summary>
        /// The last move used to get to this state.
        /// </summary>
        public readonly Move move;


        public IDDFSNode(CubeState state, Move move, IDDFSNode previousNode)
        {
            this.state = state;
            this.move = move;
            this.previousNode = previousNode;
        }
    }
}
