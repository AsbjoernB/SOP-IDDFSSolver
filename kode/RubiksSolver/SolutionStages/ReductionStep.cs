using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RubiksSolver.CubeModels;

namespace RubiksSolver.SolutionStages
{
    /// <summary>
    /// The Solution stage holds the rules for whether a cubestate has been reduced to a given group;
    /// </summary>
    public abstract class ReductionStep
    {
        /// <summary>
        /// This readOnlyCollection holds all the moves allowed while solving a certain state.
        /// Once the cube has been reduced to a certain group, some moves can't be performed anymore.
        /// NOTE: This is a property ssince fields can't be inherited from abstract classes.
        /// This means every class that inherits from ReducitonStep has a private _allowedMoves and the inherited public AllowedMoves
        /// </summary>
        public abstract Move[] allowedMoves { get; }

        /// <summary>
        /// Tells whether a cubestate has been reduced to a certain group defined by the logic of the function.
        /// These checks tend to seem arbitrary / hard to understand, since they rely on the properties of the cubestate.
        /// </summary>
        /// <param name="cubeState">The cubestate to check</param>
        /// <returns>Whether the cube is reduced or not</returns>
        public abstract bool isReduced(CubeState cubeState);

    }
}
