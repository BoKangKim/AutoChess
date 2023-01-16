using System;

namespace BehaviorTree 
{
    public class IfNotActionNode : ActionNode
    {
        public Func<bool> Condition { get; protected set; }

        public IfNotActionNode(Func<bool> condition, Action action) : base(action)
        {
            this.Condition = condition;
        }


        public override bool Run()
        {
            bool result = !Condition();
            if (result == true)
            {
                action();
            }

            return result;
        }
    }
}

