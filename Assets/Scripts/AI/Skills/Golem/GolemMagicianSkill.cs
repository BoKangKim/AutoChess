
public class GolemMagicianSkill : SkillEffect
{
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
