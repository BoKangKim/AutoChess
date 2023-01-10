using Battle.AI;

public class ExampleAI : ParentBT, Unit
{
    private string nickName = "";
    public string getNickName { get => nickName; set { } }

    protected override string initializingMytype()
    {
        return typeof(Unit).ToString();
    }
   
}
