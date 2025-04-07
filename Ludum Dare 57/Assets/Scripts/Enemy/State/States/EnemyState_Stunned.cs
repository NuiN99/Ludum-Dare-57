using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Stunned")]
public class EnemyState_Stunned : EnemyState
{
    [SerializeField] float rotForce = 100f;
    [SerializeField] bool affectRotation;
    
    public override void Enter(Enemy context)
    {
        base.Enter(context);

        if(context.Health.CurHealth <= 0 || affectRotation) return;
        
        context.RB.constraints = RigidbodyConstraints.None;
        context.RB.angularVelocity = Random.insideUnitSphere.normalized * rotForce;
    }

    public override void Exit(Enemy context)
    {
        base.Exit(context);
        context.RB.constraints = RigidbodyConstraints.FreezeRotation;
    }
}