using System;
using System.Collections;
using Focus.CameraLimitations;
using JescoDev.Utility.EventUtility.EventUtility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Focus {
    public class PivotCameraMover : MonoBehaviour {

        public event Action<IFocusable> OnFocusChanged;
        
        [field:SerializeField] public Camera Camera { get; private set; }
        
        [Header("Settings")]
        [SerializeField] private float _sensitivity = 0.5f;
        [SerializeField] private float _zoomSpeed = 0.3f;
        public float TransitionTime => _transitionTime;
        [SerializeField] private float _transitionTime = 0.5f;
        
        // focus data, where is our camera pointed
        public IFocusable CurrentFocus { get; private set; }
        public SphericalPosition CurrentPosition => _sphericalPosition;
        [ReadOnly, SerializeField] private SphericalPosition _sphericalPosition;
        private Vector3 _targetCamPosition;
        
        // are we currently transitioning to something else
        public bool IsSwitchingTargets => _coroutine != null;
        private Coroutine _coroutine;
        
        // required for mouse delta
        private Vector2 _lastMousePosition;
        
        public void InstantJump(IFocusable newFocus) {
            if(newFocus == null) return;
            CurrentFocus?.HandleUnfocus();
            CurrentFocus = newFocus;
            _sphericalPosition = newFocus.Settings.StartPosition;
            _targetCamPosition = newFocus.WorldPosition;
            ApplyCameraPosition(_targetCamPosition, _sphericalPosition);
            CurrentFocus.HandleFocus();
            OnFocusChanged.TryInvoke(CurrentFocus);
        }

        public void StartTransition(IFocusable newFocus) {
            if(newFocus == null) return;
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Transition(newFocus));
            newFocus.HandleFocus();
            OnFocusChanged.TryInvoke(newFocus);
        }

        public void ResetCamera(bool smooth = true) {
            if (smooth) StartTransition(CurrentFocus);
            else InstantJump(CurrentFocus);
        }
        
        private void Update() {
            
            if(CurrentFocus == null) return;
            if (IsSwitchingTargets) return;
            
            UpdateYawPitch();
            UpdateRadius();
            ApplyLimitations();
            ApplyCameraPosition(_targetCamPosition, _sphericalPosition);
        }
        
        private void UpdateYawPitch() {
            
            // update Mouse position
            Vector2 currentPosition = Mouse.current.position.ReadValue();
            Vector2 deltaPosition = currentPosition - _lastMousePosition;
            _lastMousePosition = currentPosition;
            
            if(!Mouse.current.rightButton.isPressed) return;
            
            Vector2 rotation = deltaPosition * _sensitivity;

            _sphericalPosition.Yaw -= rotation.x * Mathf.Deg2Rad;
            _sphericalPosition.Pitch -= rotation.y * Mathf.Deg2Rad;
        }

        private void UpdateRadius(){
            float scroll = Mouse.current.scroll.ReadValue().y;
            float zoom = Math.Sign(scroll);
            _sphericalPosition.Radius -= zoom * _sphericalPosition.Radius * _zoomSpeed;
        }

        private void ApplyLimitations() {
            foreach (ILimitation limitation in CurrentFocus.Settings.Limitations) {
                _sphericalPosition = limitation.Apply(_sphericalPosition);
            }
        }

        private void ApplyCameraPosition(Vector3 worldPos, SphericalPosition cameraPosition) {
            transform.position = worldPos + cameraPosition.ToCartesian();
            transform.LookAt(worldPos);
        }

        private IEnumerator Transition(IFocusable newFocus) {
            CurrentFocus?.HandleUnfocus();
            CurrentFocus = newFocus;
            SphericalPosition startPosition = _sphericalPosition;
            Vector3 startWorld = _targetCamPosition;

            float timePassed = 0;
            while (timePassed < _transitionTime) {
                timePassed += Time.deltaTime;
                float progress01 = timePassed / _transitionTime;
                _sphericalPosition = SphericalPosition.Lerp(startPosition, newFocus.Settings.StartPosition, progress01);
                _targetCamPosition = Vector3.Lerp(startWorld, newFocus.WorldPosition, progress01);
                ApplyCameraPosition(_targetCamPosition, _sphericalPosition);
                yield return null;
            }
            
            ApplyCameraPosition(newFocus.WorldPosition, newFocus.Settings.StartPosition);
            _coroutine = null;
        }
    }
}

