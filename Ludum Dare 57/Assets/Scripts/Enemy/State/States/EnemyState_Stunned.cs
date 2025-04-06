using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Stunned")]
public class EnemyState_Stunned : EnemyState
{
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        Debug.Log($"Enemy {context.name}: Stunned");
    }
}