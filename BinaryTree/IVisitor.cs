using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTree
{
    public interface IVisitor<in T>
    {
        bool HasCompleted { get; }

        void Visit(T obj);
    }
}
