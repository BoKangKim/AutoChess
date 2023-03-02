
public class GolemMagicianSkill : SkillEffect
{
    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Golem_Magician_Skill");
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
        return false;
    }

    protected override void specialLogic()
    {
    }
}
