using System.Linq;
using NuiN.NExtensions;
using UnityEngine;

public class ConditionGroup<TContext> : Condition<TContext>
{
    [field: SerializeField] public EvaluationMethod Evalutation { get; private set; }
    [field: SerializeField] public ConditionContainer<TContext>[] Conditions { get; private set; }

    protected override bool IsConditionMet(TContext context)
    {
        return Evalutation switch
        {
            EvaluationMethod.AnyTrue => Conditions.Any(condition => condition.Evaluate(context)),
            EvaluationMethod.AllTrue => Conditions.All(condition => condition.Evaluate(context)),
            _ => false
        };
    }
}
