using NuiN.NExtensions;
using UnityEngine;

public class EnemyState : State<Enemy>
{
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        Debug.Log(context.name + "entered state: " + name);
    }
    
}
