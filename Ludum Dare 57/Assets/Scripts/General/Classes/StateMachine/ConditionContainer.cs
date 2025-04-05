using System;
using UnityEngine;

namespace NuiN.NExtensions
{
    [Serializable]
    public class ConditionContainer<TContext>
    {
        [field: SerializeField] public Condition<TContext> Condition { get; protected set; }
        [field: SerializeField] public BoolValue Value { get; protected set; }

        public bool Evaluate(TContext context)
        {
            return Value switch
            {
                BoolValue.True => Condition.Evaluate(context),
                BoolValue.False => !Condition.Evaluate(context),
                _ => false
            };
        }
    }
}