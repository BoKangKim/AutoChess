using Battle.AI;
using UnityEngine;
using Battle.AI.Effect;

public class ExampleAI : ParentBT, Unit
{
    [SerializeField] Transform effectStartPos = null;

    protected override string initializingMytype()
    {
        attackRange = 3f;
        return typeof(Unit).ToString();
    }

    public override void StartEffect()
    {
        Ranger _effect = Instantiate<GameObject>(effect,effectStartPos.position,Quaternion.identity).GetComponent<Ranger>();

        _effect.setNickName(nickName);
        _effect.setDirection((target.transform.position - transform.position).normalized);
        _effect.gameObject.transform.LookAt(target.transform.position);
    }

    
}
