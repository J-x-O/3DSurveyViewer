using System;
using UnityEngine;

namespace UI {
    
    [Serializable, AddTypeMenu("Canvas Group")]
    public class ToggleTweenCanvasGroup : IToggleTweenHandler {
        
        [SerializeField] private CanvasGroup _target;
        
        private float _start;
        
        public void OnStart(bool state) {
            _start = _target.alpha;
        }

        public void OnUpdate(bool state, float progress) {
            float target = state ? 1f : 0f;
            _target.alpha = Mathf.Lerp(_start, target, progress);
        }

        public void FinalizeState(bool state) {
            _target.alpha = state ? 1f : 0f;
        }
    }
}