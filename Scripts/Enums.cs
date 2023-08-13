using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextSizeFilter
{
    /// <summary>
    /// The size fit modes avaliable to use.
    /// </summary>
    public enum FitMode
    {
        /// <summary>
        /// Don't perform any resizing.
        /// </summary>
        Unconstrained,
        /// <summary>
        /// Resize to the minimum size of the text.
        /// </summary>
        MinSize,
        /// <summary>
        /// Resize to the preferred size of the text.
        /// </summary>
        PreferredSize
    }
}