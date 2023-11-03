using System;
using System.Collections;
using System.Collections.Generic;
using Focus;
using JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class UIPivotRepresentation : MonoBehaviour {

        [SerializeField] private CanvasGroup _group;
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
            _group.alpha = 0;
        }
        
        public void Update() {
            if (_target is not MonoBehaviour mono) return;
            Vector3 screenPos = _camera.Camera.WorldToScreenPoint(mono.transform.position);
            transform.position = screenPos;
            float targetAlpha = _target.IsSelected || _dying ? 0 : 1;
            _group.alpha = Mathf.MoveTowards(_group.alpha, targetAlpha, Time.deltaTime / _fadeDuration);
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
            Destroy(gameObject, _fadeDuration);
        }
    }
}