using NuiN.NExtensions;
using UnityEngine;

public class LeviathanRaycaster : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Transform headPos;
    [SerializeField] float raycastDist;

    [SerializeField] LayerMask layer;

    [SerializeField] float adjustSpeed = 1f;
    
    void Update()
    {
        if (Physics.Raycast(headPos.position, Vector3.down, raycastDist, layer))
        {
            Vector3 newAttackDir = Vector3.MoveTowards(enemy.Attacking.AttackDir, enemy.Attacking.AttackDir.With(2).normalized, adjustSpeed);
            enemy.Attacking.SetAttackDir(enemy.Attacking.AttackDir.Add(y: adjustSpeed * Time.deltaTime));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(headPos.position, Vector3.down * raycastDist);
    }
}
