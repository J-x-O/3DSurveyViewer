using System;
using UnityEngine;

namespace UI {
    
    [Serializable, AddTypeMenu("Rect")]
    public class ToggleTweenRect : IToggleTweenHandler {
        
        [SerializeField] private RectTransform _self;
        [SerializeField] private RectTransform _onState;
        [SerializeField] private RectTransform _offState;

        private Vector2 _start;
        private Vector2 _startSize;
        private Vector2 _startPivot;
        private Vector2 _startAnchorMin;
        private Vector2 _startAnchorMax;
        
        public void OnStart(bool state) {
            _start = _self.anchoredPosition;
            _startSize = _self.sizeDelta;
            _startPivot = _self.pivot;
            _startAnchorMin = _self.anchorMin;
            _startAnchorMax = _self.anchorMax;
        }

        public void OnUpdate(bool state, float progress) {
            RectTransform target = state ? _onState : _offState;
            _self.anchoredPosition = Vector2.Lerp(_start, target.anchoredPosition, progress);
            _self.sizeDelta = Vector2.Lerp(_startSize, target.sizeDelta, progress);
            _self.pivot = Vector2.Lerp(_startPivot, target.pivot, progress);
            _self.anchorMin = Vector2.Lerp(_startAnchorMin, target.anchorMin, progress);
            _self.anchorMax = Vector2.Lerp(_startAnchorMax, target.anchorMax, progress);
        }

        public void FinalizeState(bool state) {
            RectTransform target = state ? _onState : _offState;
            _self.anchoredPosition = target.anchoredPosition;
            _self.sizeDelta = target.sizeDelta;
            _self.pivot = target.pivot;
            _self.anchorMin = target.anchorMin;
            _self.anchorMax = target.anchorMax;
        }
    }
}