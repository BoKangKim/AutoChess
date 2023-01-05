
using System;

namespace BehaviorTree
{
    public class IfSelectorNode : DecoratorCompositeNode
    {
        public IfSelectorNode(Func<bool> condition, params INode[] nodes) : base(condition, new SelectorNode(nodes)) { }
    }
}

