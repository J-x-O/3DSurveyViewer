using System;
using UnityEngine;

namespace Focus {
    public class PivotCameraDisplacement : MonoBehaviour {
        
        [SerializeField] private Vector3 _displacement;
        [SerializeField] private bool _rotate;
        [SerializeField] private PivotCameraMover _mover;

        public void LateUpdate() {
            transform.localPosition = _displacement * _mover.CurrentPosition.Radius;
            if (_rotate) {
                transform.LookAt(_mover.CurrentFocus.WorldPosition);
                transform.localRotation = Quaternion.Euler(0,0, transform.localRotation.eulerAngles.z);
            }
        }
    }
}