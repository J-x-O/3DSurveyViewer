using System;
using System.Collections;
using System.Collections.Generic;
using Focus;
using JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class UIPivotRepresentation : MonoBehaviour {

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private float _fadeDuration = 0.25f;

        private PivotCameraMover _camera;
        private IFocusable _target;
        private bool _dying;
        private TweenInfo _fadeTween;

        private void OnEnable() => _button.onClick.AddListener(FocusTarget);
        private void OnDisable() => _button.onClick.RemoveListener(FocusTarget);
        
        private void FocusTarget() {
            if(_camera != null) _camera.StartTransition(_target);
        }

        private void Start() {
            float alpha = _image.color.a;
            _image.color = new Color(1, 1, 1, 0);
            _fadeTween = SmoothBrainTween.Value(0, alpha, _fadeDuration)
                .SetOnUpdate(value => _image.color = new Color(1, 1, 1, value));
        }
        
        public void Update() {
            if (_target is not MonoBehaviour mono) return;
            Vector3 screenPos = _camera.Camera.WorldToScreenPoint(mono.transform.position);
            transform.position = screenPos;
        }

        public void BindToObject(PivotCameraMover cam, IFocusable target) {
            _camera = cam;
            _target = target;
        }

        public void SetSprite(Sprite icon) {
            _image.sprite = icon;
        }

        public void Die() {
            if(_dying) return;
            _dying = true;
            SmoothBrainTween.Cancel(_fadeTween);
            SmoothBrainTween.Value(_image.color.a, 0, _fadeDuration)
                .SetOnUpdate(value => _image.color = new Color(1, 1, 1, value))
                .SetOnFinish(() => Destroy(gameObject));
        }
    }
}