using Battle.AI;
using UnityEngine;
using Battle.EFFECT;

public class ExampleAI : ParentBT, Unit
{
    [SerializeField] Transform effectStartPos = null;
    [SerializeField] Effect projectile = null;

    protected override string initializingMytype()
    {
        attackRange = 3f;
        return typeof(Unit).ToString();
    }

    public override void StartEffect()
    {
        Effect flash = null;
        Effect project = null;
        Instantiate(standardAttackEffect.gameObject, effectStartPos.transform.position, Quaternion.identity).TryGetComponent<Effect>(out flash);
        flash.setOwnerName(nickName);

        Instantiate(projectile.gameObject,effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<Effect>(out project);
        project.setOwnerName(nickName);
        project.setDirection(target.transform.position);
    }
}
