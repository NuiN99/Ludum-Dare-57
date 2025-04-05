using UnityEngine;

namespace NuiN.NExtensions
{
    public abstract class Condition<TContext> : ScriptableObject
    {
        protected abstract bool IsConditionMet(TContext context);
        public bool Evaluate(TContext context) => IsConditionMet(context);
    }
}