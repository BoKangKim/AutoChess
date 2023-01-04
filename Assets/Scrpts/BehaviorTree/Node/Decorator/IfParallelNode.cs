using System;

namespace BehaviorTree 
{
    public class IfParallelNode : DecoratorCompositeNode
    {
        public IfParallelNode(Func<bool> condition,params INode[] nodes) : base(condition, new ParallelNode(nodes)){ }
    }

}
