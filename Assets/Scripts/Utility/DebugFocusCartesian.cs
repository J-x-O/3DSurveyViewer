using Focus;
using UnityEngine;

namespace Utility {
    public class DebugFocusCartesian : MonoBehaviour {

        [SerializeField] private PivotCameraMover _mover;
        [SerializeField] private Vector3 _cartesian;

        private void Update() {
            _cartesian = _mover.CurrentPosition.ToCartesian();
        }
        
    }
}