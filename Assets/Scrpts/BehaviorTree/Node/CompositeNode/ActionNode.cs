using System;

namespace BehaviorTree 
{
    public class ActionNode : ILeafNode
    {
        public Action action { get; protected set; }



        public ActionNode(Action action) => this.action = action;

        public static implicit operator ActionNode(Action action) => new ActionNode(action);
        public static implicit operator Action(ActionNode action) => new Action(action.action);

        public virtual bool Run()
        {
            action();
            return true;
        }
    }
}

