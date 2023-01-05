using System;

namespace BehaviorTree 
{
    public static class BehaviorTreeMan
    {
        public static SelectorNode Selector(params INode[] nodes) => new SelectorNode(nodes);
        public static SequenceNode Sequence(params INode[] nodes) => new SequenceNode(nodes);
        public static ParallelNode Parallel(params INode[] nodes) => new ParallelNode(nodes);

        public static ActionNode ActionN(Action action) => new ActionNode(action);
        public static ConditionNode IF(Func<bool> condition) => new ConditionNode(condition);
        public static NotConditionNode NotIf(Func<bool> condition) => new NotConditionNode(condition);
        public static IfActionNode IfAction(Func<bool> condition, Action action) => new IfActionNode(condition, action);
        public static IfElseActionNode IfElseAction(Func<bool> condition, Action ifAction, Action elseAction) =>new IfElseActionNode(condition,ifAction,elseAction);
        public static IfNotActionNode IfNotAction(Func<bool> condition,Action action) => new IfNotActionNode(condition, action);
    }
}


