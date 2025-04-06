using System;
using System.Collections.Generic;
using UnityEngine;

public class PriorityAnimator
{
    public Animator Animator { get; private set; }
    Dictionary<int, Layer> _layerDictionary;
    
    public PriorityAnimator(Animator animator)
    {
        _layerDictionary = new Dictionary<int, Layer>();
        Animator = animator;
    }
    
    public void Play(string animation, int priority, int layerIndex = 0, float crossFade = 0f)
    {
        if (string.IsNullOrEmpty(animation))
            return;
        
        if (!_layerDictionary.TryGetValue(layerIndex, out Layer layer))
        {
            layer = new Layer(layerIndex);
            _layerDictionary.Add(layerIndex, layer);
        }
        
        layer.TrySetAnimation(Animator, animation, priority, crossFade);
    }

    public void Play(AnimationPlayData animationData)
    {
        Play(animationData.Animation, animationData.Priority, animationData.LayerIndex, animationData.CrossFade);
    }

    public void ResetLayer(int layerIndex)
    {
        if (_layerDictionary.TryGetValue(layerIndex, out Layer layer))
        {
            layer.ResetPriority();
        }
    }

    public string GetCurrentAnimation(int layerIndex)
    {
        if (_layerDictionary.TryGetValue(layerIndex, out Layer layer))
        {
            return layer.CurrentAnimation;
        }

        return string.Empty;
    }
    
    public class Layer
    {
        public readonly int layerIndex;
        public string CurrentAnimation { get; private set; }
        public int CurrentAnimationPriority { get; private set; } = int.MinValue;
        
        public Layer(int layerIndex)
        {
            this.layerIndex = layerIndex;
        }

        public void TrySetAnimation(Animator animator, string animation, int priority, float crossFade)
        {
            if (priority < CurrentAnimationPriority)
                return;
            
            CurrentAnimation = animation;
            CurrentAnimationPriority = priority;
            
            animator.CrossFadeInFixedTime(animation, crossFade, layerIndex);
            //animator.Update(0);

            /*if (!animator.GetCurrentAnimatorStateInfo(layerIndex).loop)
            {
                // reset priority when animation finishes
                SpleenTween.DoWhen(() => animator == null || animator.NormalizedTime() >= 1f, ResetPriority).StopIf(() => animator == null);
            }*/
        }

        public void ResetPriority()
        {
            CurrentAnimationPriority = int.MinValue;
            CurrentAnimation = string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Layer layer) return false;
            return layer.layerIndex == layerIndex;
        }

        protected bool Equals(Layer other) => layerIndex == other.layerIndex && CurrentAnimation == other.CurrentAnimation;
        public override int GetHashCode() => layerIndex.GetHashCode();
    }

    [Serializable]
    public struct AnimationPlayData
    {
        [field: SerializeField] public string Animation { get; private set; }
        [field: SerializeField] public int Priority { get; private set; }
        [field: SerializeField] public int LayerIndex { get; private set; }
        [field: SerializeField] public float CrossFade { get; private set; }

        public AnimationPlayData(string animation, int priority, int layerIndex = 0, float crossFade = 0f)
        {
            Animation = animation;
            Priority = priority;
            LayerIndex = layerIndex;
            CrossFade = crossFade;
        }
    }
}