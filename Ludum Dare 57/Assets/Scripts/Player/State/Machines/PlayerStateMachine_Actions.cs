using NuiN.NExtensions;

public class PlayerStateMachine_Actions : StateMachine<Player>
{
    void Start()
    {
        Initialize(Context);
    }
}