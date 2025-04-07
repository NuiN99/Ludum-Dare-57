using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Dead")]
public class EnemyState_Dead : EnemyState
{
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        
        context.Health.Die();
    }
}