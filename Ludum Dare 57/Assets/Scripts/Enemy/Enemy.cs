using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyHealth Health { get; private set; }
    [field: SerializeField] public EnemyTargeting Targeting { get; private set; }
    [field: SerializeField] public EnemyAttacking Attacking { get; private set; }
    [field: SerializeField] public Rigidbody RB { get; private set; }
    
    
}