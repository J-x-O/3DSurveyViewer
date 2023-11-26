using System;
using UnityEngine;

namespace Focus {
    public class PivotCameraDisplacement : MonoBehaviour {
        
        [SerializeField] private Vector3 _displacement;
        [SerializeField] private bool _rotate;
        [SerializeField] private float _heightInfluence = 1;
        [SerializeField] private PivotCameraMover _mover;

        public void LateUpdate() {
            float heightInfluence = Mathf.Lerp(1, Mathf.Cos(_mover.CurrentPosition.Pitch), _heightInfluence); 
            transform.localPosition = _displacement * (_mover.CurrentPosition.Radius * heightInfluence);
            if (_rotate) {
                transform.LookAt(_mover.CurrentFocus.WorldPosition);
                transform.localRotation = Quaternion.Euler(0,0, transform.localRotation.eulerAngles.z);
            }
        }
    }
}