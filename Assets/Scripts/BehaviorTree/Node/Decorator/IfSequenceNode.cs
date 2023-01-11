using System;

namespace BehaviorTree 
{
    public class IfSequenceNode : DecoratorCompositeNode
{
        public IfSequenceNode(Func<bool> condition, params INode[] nodes) : base(condition, new SequenceNode(nodes)) { }
    }

}
