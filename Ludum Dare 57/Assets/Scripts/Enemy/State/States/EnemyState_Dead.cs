using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Dead")]
public class EnemyState_Dead : EnemyState
{
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        Debug.Log($"Enemy {context.name}: Dead");
        
        context.Health.Die();
    }
}