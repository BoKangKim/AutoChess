using Battle.AI;

public class ExampleAI : ParentBT, Unit
{
    protected override string initializingMytype()
    {
        return typeof(Unit).ToString();
    }
   
}
