using System;
using System.Collections.Generic;
using Focus;
using JescoDev.Utility.EventUtility.EventUtility;
using UnityEngine;
using UnityEngine.Serialization;

namespace ModelViewing {
    public class ModelRowManager : MonoBehaviour {

        public event Action OnNewSet;
        public event Action OnFinish;
        
        [SerializeField] private PivotCameraMover _mover;
        [SerializeField] private FocusPointGroup[] _prefabs;
        [SerializeField] private float _distance;
        
        public FocusPointGroup ActiveGroup => _instances[CurrentIndex];
        private FocusPointGroup[] _instances;
        public int CurrentIndex { get; private set; }

        public void Awake() {
            _instances = new FocusPointGroup[_prefabs.Length];
            for (int index = 0; index < _prefabs.Length; index++) {
                _instances[index] = Instantiate(_prefabs[index], transform);
                _instances[index].transform.localPosition = Vector3.right * _distance * index;
            }
        }
        
        public void Start() {
            ApplyFocusable(0);
            _mover.InstantJump(ActiveGroup.PrimaryFocus);
        }

        public void SwitchToNext() {
            CurrentIndex++;
            if (CurrentIndex >= _instances.Length) {
                OnFinish.TryInvoke();
                return;
            }
            ApplyFocusable(CurrentIndex);
            OnNewSet.TryInvoke();
            _mover.StartTransition(ActiveGroup.PrimaryFocus);
        }
        
        private void ApplyFocusable(int index) {
            for (int i = 0; i < _prefabs.Length; i++) {
                // can only focus selected
                foreach (IFocusable focusPoint in _instances[index].FocusPoints) {
                    focusPoint.IsFocusable = i == CurrentIndex;
                }
            }
        }
        
        public void ResetView() => _mover.StartTransition(ActiveGroup.PrimaryFocus);
    }
}