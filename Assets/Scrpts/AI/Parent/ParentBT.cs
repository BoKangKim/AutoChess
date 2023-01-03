using UnityEngine;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;

public abstract class ParentBT : MonoBehaviour
{
    private INode root = null;

    private void Awake()
    {
        initializingRootNode();
    }

    private void Update()
    {
        root.Run();
    }

    protected abstract void initializingRootNode();
}
