using System;

namespace BehaviorTree 
{
    public class DecoratorCompositeNode : IDecoratorNode
    {
        public Func<bool> Condition { get; protected set; }
        public CompositeNode Composite { get; protected set; }

        public DecoratorCompositeNode(Func<bool> condition, CompositeNode composite)
        {
            this.Condition = condition;
            this.Composite = composite;
        }

        public bool Run()
        {
            if(!Condition())
            {
                return false;
            }

            return Composite.Run();
        }
    }
}

