using System.Collections.Generic;

namespace BehaviorTree 
{
    public abstract class CompositeNode : ICompositeNode
    {
        public List<INode> ChildNodes { get; protected set; }

        public CompositeNode(params INode[] nodes) => ChildNodes = new List<INode>(nodes);

        public abstract bool Run();
    }
}

