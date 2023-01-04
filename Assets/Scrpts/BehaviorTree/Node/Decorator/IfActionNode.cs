using System;

namespace BehaviorTree 
{
    public class IfActionNode : ILeafNode
    {
        public Func<bool> Condition { get; protected set; }
        public Action IfAction { get; protected set; }

        public IfActionNode(Func<bool> condition, Action ifAction)
        {
            this.Condition = condition;
            this.IfAction = ifAction;
        }

        public bool Run()
        {
            bool result = Condition();

            if (result)
            {
                IfAction();
            }

            return result;
        }
    }
}

