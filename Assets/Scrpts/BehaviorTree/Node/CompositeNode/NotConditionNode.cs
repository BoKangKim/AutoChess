using System;

namespace BehaviorTree 
{
    public class NotConditionNode : ILeafNode
    {
        public Func<bool> condition { get; protected set; }

        public NotConditionNode(Func<bool> condition)
        {
            this.condition = () => !condition();
        }

        public bool Run() => condition();
    }
}

