using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

public class DwarfMagicianSkill : SkillEffect
{
    [SerializeField] GameObject explosionEffect = null;
    private GameObject inst = null;
    private bool isExplosion = false;

    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Dwarf_Magician_Skill");
    }

    protected override float setSpeed()
    {
        return 15f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return false;
    }

    protected override float setDestroyTime()
    {
        return 1.5f;
    }

    public override void setDirection(Vector3 targetPosition)
    {
        this.gameObject.transform.position = targetPosition + (Vector3.up * 8f);
        Vector3 target = targetPosition;
        base.direction = (target - gameObject.transform.position).normalized;
    }

    private void OnDestroy()
    {
        Destroy(this.inst.gameObject);
    }

    protected override void specialLogic()
    {
        if(transform.position.y <= 0f && isExplosion == false)
        {
            inst = Instantiate(explosionEffect);
            inst.transform.position = new Vector3(transform.position.x, 0 ,transform.position.z);
            isExplosion = true;
        }

        
    }
}
