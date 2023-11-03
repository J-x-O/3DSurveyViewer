using System;
using System.Collections.Generic;
using Beta.Devtools;
using Focus;
using JescoDev.Utility.EventUtility.EventUtility;
using UnityEngine;
using UnityEngine.Serialization;

namespace ModelViewing {
    public class ModelRowManager : MonoBehaviour {

        public event Action OnNewSet;
        public event Action OnFinish;
        
        [SerializeField] private PivotCameraMover _mover;
        [SerializeField] private Optional<float> _distance;
        
        public FocusPointGroup ActiveGroup => _instances[CurrentIndex];
        public FocusPointGroup[] Instances => _instances;
        [SerializeField] private FocusPointGroup[] _instances;
        public int CurrentIndex { get; private set; }

        public void OnValidate() {
            if (!_distance.Enabled) return;
            for (int index = 0; index < _instances.Length; index++) {
                if (_instances[index] == null) continue;
                _instances[index].transform.localPosition = Vector3.right * _distance.Value * index;
            }
        }
        
        public void Start() {
            EnableValidFocusPoints(0);
            _mover.InstantJump(ActiveGroup.PrimaryFocus);
        }

        public void SwitchToNext() {
            CurrentIndex++;
            if (CurrentIndex >= _instances.Length) {
                OnFinish.TryInvoke();
                return;
            }
            EnableValidFocusPoints(CurrentIndex);
            OnNewSet.TryInvoke();
            _mover.StartTransition(ActiveGroup.PrimaryFocus);
        }
        
        private void EnableValidFocusPoints(int index) {
            for (int i = 0; i < _instances.Length; i++) {
                // can only focus selected
                foreach (IFocusable focusPoint in _instances[index].FocusPoints) {
                    focusPoint.IsFocusable = i == CurrentIndex;
                }
            }
        }
        
        public void ResetView() => _mover.StartTransition(ActiveGroup.PrimaryFocus);
    }
}