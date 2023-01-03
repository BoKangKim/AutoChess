using System;

namespace BehaviorTree 
{
    public class IfElseActionNode : ILeafNode
    {
        public Func<bool> Condition { get; protected set; }
        public Action IfAction { get; protected set; }
        public Action ElseAction { get; protected set; }

        public IfElseActionNode(Func<bool> condition, Action ifAction, Action elseAction)
        {
            this.Condition = condition;
            this.IfAction = ifAction;
            this.ElseAction = elseAction;
        }

        public bool Run()
        {
            bool result = Condition();

            if (result)
            {
                IfAction();
            }
            else
            {
                ElseAction();
            }

            return result;
        }
    }
}

