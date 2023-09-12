using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Focus {
    public class PivotCameraRayCaster : MonoBehaviour {
        
        [SerializeField] private bool _enableMouse = false;
        [SerializeField] private Camera _camera;
        [NotNull, SerializeField] private PivotCameraMover _cameraMover;
        

        private void Update() {
             if(!_enableMouse || _cameraMover.IsSwitchingTargets) return;
             if (Mouse.current.leftButton.isPressed) SwitchFocus();
        }

        private void SwitchFocus(){
            Vector3 mousePos = Mouse.current.position.ReadValue();
            if (!Physics.Raycast(_camera.ScreenPointToRay(mousePos), out RaycastHit hit)) { } // Remember to change Camera
            
            IFocusable target = hit.collider.gameObject.GetComponent<IFocusable>();
            if (target == null) return;
            if (target == _cameraMover.CurrentFocus) return;
            _cameraMover.StartTransition(target);
        }
    }
}
