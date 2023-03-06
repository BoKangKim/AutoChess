using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrcWarriorSkill : SkillEffect
{
    private Animator ownerAni = null;
    private void Start()
    {
        owner.TryGetComponent<Animator>(out ownerAni);
    }
    protected override float setDestroyTime()
    {
        return 2f;
    }

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }

    protected override void specialLogic()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine("CO_EnterOwner");
        GameManager.Inst.soundOption.SFXPlay("Orc_Warrior_Skill");
    }

    IEnumerable CO_EnterOwner()
    {
        yield return new WaitUntil(() => owner != null);
        owner.getAnimator().speed += 0.07f;
    }

}
