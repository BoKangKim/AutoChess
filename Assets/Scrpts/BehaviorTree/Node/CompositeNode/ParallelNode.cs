namespace BehaviorTree 
{
    public class ParallelNode : CompositeNode
    {
        public ParallelNode(params INode[] nodes) : base(nodes) { }

        public override bool Run()
        {
            for(int i = 0; i < ChildNodes.Count; i++)
            {
                ChildNodes[i].Run();
            }

            return true;
        }
    }
}

