using System.Collections.Generic;
using System.Text;

namespace o2.Runtime.ScriptGeneration {

    /// <summary>
    /// Interface for objects that can be built into strings of code.
    /// </summary>
    public interface IBuildable {
        
        /// <summary>
        /// Builds the code representation of the object.
        /// </summary>
        /// <returns>A string representing the built code.</returns>
        string Build();

        /// <summary>
        /// Builds the code for multiple IBuildable objects and joins them with an optional newline.
        /// </summary>
        /// <param name="buildables">A collection of IBuildable objects to build.</param>
        /// <param name="addNewLine">If true, adds a newline between each built code string. Default is true.</param>
        /// <returns>A string representing the combined code of all buildable objects.</returns>
        public static string BuildMultiple(IEnumerable<IBuildable> buildables, bool addNewLine = true) {
            StringBuilder builder = new();
            foreach (var buildable in buildables)
            {
                builder.Append(buildable.Build());
                if (addNewLine)
                    builder.Append("\n");
            }

            return builder.ToString();
        }
    }
}