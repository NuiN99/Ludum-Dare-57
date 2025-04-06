using NuiN.NExtensions;
using UnityEngine;

public class EnemyStateMachine : StateMachine<Enemy>
{
    [SerializeField, InjectComponent] Enemy enemy;

    void Awake()
    {
        Initialize(enemy);
    }
}
