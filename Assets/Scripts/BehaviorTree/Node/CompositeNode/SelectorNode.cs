namespace BehaviorTree 
{
    public class SelectorNode : CompositeNode
    {
        public SelectorNode(params INode[] nodes) : base(nodes) { }

        public override bool Run()
        {
            for(int i = 0; i < ChildNodes.Count; i++)
            {
                if(ChildNodes[i].Run() == true)
                {
                    return true;
                }
            }

            return false;
        }
    }
}


