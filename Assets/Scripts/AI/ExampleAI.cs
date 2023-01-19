using Battle.AI;
using UnityEngine;

public class ExampleAI : ParentBT, Unit
{
    


    protected override string initializingMytype()
    {
        attackRange = 3f;
        return typeof(Unit).ToString();
    }
   
}
