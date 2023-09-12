using System;
using UnityEngine;

namespace UI {
    
    [Serializable, AddTypeMenu("Rotation")]
    public class ToggleTweenRotation : IToggleTweenHandler {
        
        [SerializeField] private RectTransform _self;
        [SerializeField] private Vector3 _onState;
        [SerializeField] private Vector3 _offState;

        private Quaternion _start;
        
        public void OnStart(bool state) {
            _start = _self.rotation;
        }

        public void OnUpdate(bool state, float progress) {
            Vector3 target = state ? _onState : _offState;
            _self.rotation = Quaternion.Lerp(_start, Quaternion.Euler(target), progress);
        }

        public void FinalizeState(bool state) {
            Vector3 target = state ? _onState : _offState;
            _self.rotation = Quaternion.Euler(target);
        }
    }
}
