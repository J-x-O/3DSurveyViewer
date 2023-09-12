using System;
using UnityEngine;

namespace Focus {
    public class PivotCameraDisplacement : MonoBehaviour {
        
        [SerializeField] private Vector3 _displacement;
        [SerializeField] private PivotCameraMover _mover;

        public void Update() => transform.localPosition = _displacement * _mover.CurrentPosition.Radius;
    }
}