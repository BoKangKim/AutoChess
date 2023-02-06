using Battle.AI;
using UnityEngine;
using Battle.EFFECT;

public class RangeAI : UnitAI
{
    [SerializeField] protected Transform effectStartPos = null;
    [SerializeField] protected Effect projectile = null;

    public override void StartEffect()
    {
        mana += manaRecovery;
        if (mana > maxMana)
        {
            return;
        }

        Effect flash = null;
        Effect project = null;
        Instantiate(standardAttackEffect.gameObject, effectStartPos.transform.position, Quaternion.identity).TryGetComponent<Effect>(out flash);
        flash.setOwner(this);

        Instantiate(projectile.gameObject,effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<Effect>(out project);
        project.setOwner(this);
        project.setDirection(target.transform.position);
    }

    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject,effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
        skill.setDirection(target.transform.position);
    }
}
