using System;
using System.Collections.Generic;
using UnityEngine;

namespace NuiN.NExtensions
{
    public abstract class StateMachine<TContext> : MonoBehaviour
    {
        [Serializable]
        public class StateTransitionPair
        {
            [field: SerializeField] public State<TContext> State { get; private set; }
            [field: SerializeField] public Transition<TContext>[] Transitions { get; protected set; }
        }
        
        [field: ShowInInspector] public State<TContext> CurrentState { get; private set; }

        [SerializeField, HideInPlayMode] State<TContext> initialState;
        [field: SerializeField] public TContext Context { get; private set; }

        [SerializeField] List<StateTransitionPair> stateTransitions;
        [SerializeField] List<StateTransitionPair> extraStateTransitions;
        
        Dictionary<State<TContext>, State<TContext>> _prefabInstanceDictionary = new();
        Dictionary<State<TContext>, Transition<TContext>[]> _transitionsDictionary = new();

        public virtual void Initialize(TContext context) 
        {
            Context = context;
            
            foreach (var stateTransitionPair in stateTransitions)
            {
                if (stateTransitionPair.State != null)
                {
                    _transitionsDictionary.Add(stateTransitionPair.State, stateTransitionPair.Transitions);
                }
            }
            
            foreach (var stateTransitionPair in extraStateTransitions)
            {
                if (stateTransitionPair.State != null)
                {
                    _transitionsDictionary.Add(stateTransitionPair.State, stateTransitionPair.Transitions);
                }
            }

            Transition(initialState);
        }

        void Transition(State<TContext> newState)
        {
            CurrentState?.Exit(Context);
            CurrentState = newState;
            CurrentState.Enter(Context);
        }
        
        public void ResetToInitialState() => Transition(initialState);

        public void Update()
        {
            if (CurrentState == null) return;
            
            //check if we should transition before iterating the current state
            if (_transitionsDictionary.TryGetValue(CurrentState, out Transition<TContext>[] transitions))
            {
                foreach (Transition<TContext> transition in transitions)
                {
                    if (transition.ShouldTransition(Context))
                    {
                        Transition(transition.TargetState);
                        return;
                    }
                }
            }

            CurrentState.FrameUpdate(Context);
        }

        public void FixedUpdate() => CurrentState?.PhysicsUpdate(Context);

        public void LateUpdate() => CurrentState?.LateFrameUpdate(Context);

        void OnCollisionEnter(Collision other) => CurrentState?.CollisionEnter(Context, other);
        void OnCollisionStay(Collision other) => CurrentState?.CollisionStay(Context, other);
        void OnCollisionExit(Collision other) => CurrentState?.CollisionExit(Context, other);

        void OnTriggerEnter(Collider other) => CurrentState?.TriggerEnter(Context, other);
        void OnTriggerStay(Collider other) => CurrentState?.TriggerStay(Context, other);
        void OnTriggerExit(Collider other) => CurrentState?.TriggerExit(Context, other);
        void OnDrawGizmos() => CurrentState?.DrawGizmos(Context);
    }
}