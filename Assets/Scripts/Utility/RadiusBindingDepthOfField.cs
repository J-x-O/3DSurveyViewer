using System;
using Focus;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Utility {
    public class RadiusBindingDepthOfField : MonoBehaviour {
        
        [SerializeField] private PivotCameraMover _pivotCameraMover;
        [SerializeField] private Volume _volume;
        [SerializeField] private float _offset = 5f;
        [SerializeField] private float _range = 10f;
        private DepthOfField _depthOfField;

        private void Awake() {
            if (_volume == null) return;
            _volume.profile.TryGet(out _depthOfField);
        }

        private void Update() {
            if (_pivotCameraMover == null || _depthOfField == null) return;
            float radius = _pivotCameraMover.CurrentPosition.Radius;
            _depthOfField.gaussianStart.value = radius + _offset;
            _depthOfField.gaussianEnd.value = radius + _offset + _range;
        }
    }
}