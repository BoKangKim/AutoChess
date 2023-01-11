namespace BehaviorTree 
{
    public class SequenceNode : CompositeNode
    {
        public SequenceNode(params INode[] nodes) : base(nodes) { }

        public override bool Run()
        {
            for(int i = 0; i < ChildNodes.Count; i++)
            {
                if(ChildNodes[i].Run() == false)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
