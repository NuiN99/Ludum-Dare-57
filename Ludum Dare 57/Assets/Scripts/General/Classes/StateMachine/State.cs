using UnityEngine;

namespace NuiN.NExtensions
{
    public abstract class State<TContext> : ScriptableObject
    {
        public virtual void Enter(TContext context) { }
        public virtual void Exit(TContext context) { }
        public virtual void FrameUpdate(TContext context) { }
        public virtual void PhysicsUpdate(TContext context) { }
        public virtual void LateFrameUpdate(TContext context) { }
    
        public virtual void CollisionEnter(TContext context, Collision other) { }
        public virtual void CollisionStay(TContext context, Collision other) { }
        public virtual void CollisionExit(TContext context, Collision other) { }
        public virtual void TriggerEnter(TContext context, Collider other) { }
        public virtual void TriggerStay(TContext context, Collider other) { }
        public virtual void TriggerExit(TContext context, Collider other) { }
        public virtual void DrawGizmos(TContext context) { }
    }
}
