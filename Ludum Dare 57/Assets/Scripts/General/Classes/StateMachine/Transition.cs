using System;
using System.Linq;
using UnityEngine;

namespace NuiN.NExtensions
{
    [Serializable]
    public class Transition<TContext>
    {
        [field: SerializeField] public State<TContext> TargetState { get; private set; }
        [field: SerializeField] public EvaluationMethod Evalutation { get; private set; }
        [field: SerializeField] public ConditionContainer<TContext>[] Conditions { get; private set; }

        public bool ShouldTransition(TContext context)
        {
            return Evalutation switch
            {
                EvaluationMethod.AnyTrue => Conditions.Any(condition => condition.Evaluate(context)),
                EvaluationMethod.AllTrue => Conditions.All(condition => condition.Evaluate(context)),
                _ => false
            };
        }
    }
}