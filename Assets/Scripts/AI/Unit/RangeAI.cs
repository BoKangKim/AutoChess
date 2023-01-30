using Battle.AI;
using UnityEngine;
using Battle.EFFECT;

public class RangeAI : UnitAI
{
    [SerializeField] Transform effectStartPos = null;
    [SerializeField] Effect projectile = null;

    public override void StartEffect()
    {
        mana += 5f;
        if (mana > maxMana)
        {
            return;
        }

        Effect flash = null;
        Effect project = null;
        Instantiate(standardAttackEffect.gameObject, effectStartPos.transform.position, Quaternion.identity).TryGetComponent<Effect>(out flash);
        flash.setOwnerName(nickName);

        Instantiate(projectile.gameObject,effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<Effect>(out project);
        project.setOwnerName(nickName);
        project.setDirection(target.transform.position);
    }

    public override void StartSkillEffect()
    {
        if(myAni.GetParameter(2).name.CompareTo("activeSkill") == 0)
        {
            myAni.SetTrigger("activeSkill");
        }
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject,effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill);
        skill.setOwnerName(nickName);
        skill.setDirection(target.transform.position);
    }

    protected override float setAttackRange()
    {
        return 3f;
    }
}
