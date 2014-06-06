using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTree
{
    public interface IBeforAfterVisitor<T>
    {
        /// <summary>
        /// Pre-visit action.
        /// </summary>
        /// <param name="item">The item.</param>
        void BeforVisit(T item);

        /// <summary>
        /// Post-visit action.
        /// </summary>
        /// <param name="item">The item.</param>
        void AfterVisit(T item);
    }
}
