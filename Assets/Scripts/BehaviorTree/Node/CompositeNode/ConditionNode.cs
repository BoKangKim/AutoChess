using System;

namespace BehaviorTree 
{
    public class ConditionNode : ILeafNode
    {
        public Func<bool> condition { get; protected set; }

        public ConditionNode(Func<bool> condition) => this.condition = condition;

        public static implicit operator ConditionNode(Func<bool> condition) => new ConditionNode(condition);
        public static implicit operator Func<bool>(ConditionNode condition) => new Func<bool>(condition.condition);


        public IfActionNode Action(Action action)
            => new IfActionNode(condition, action);

        public IfSequenceNode Sequence(params INode[] nodes)
            => new IfSequenceNode(condition, nodes);

        public IfSelectorNode Selector(params INode[] nodes)
            => new IfSelectorNode(condition, nodes);

        public IfParallelNode Parallel(params INode[] nodes)
            => new IfParallelNode(condition, nodes);

        public bool Run()
        {
            return condition();
        }
    }
}

