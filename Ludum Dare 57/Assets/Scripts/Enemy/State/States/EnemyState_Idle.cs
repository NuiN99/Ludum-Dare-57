using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Idle")]
public class EnemyState_Idle : EnemyState
{
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        Debug.Log($"Enemy {context.name}: Idle");
    }

    public override void FrameUpdate(Enemy context)
    {
        base.FrameUpdate(context);
        context.Targeting.TryDetectTarget();
    }
}