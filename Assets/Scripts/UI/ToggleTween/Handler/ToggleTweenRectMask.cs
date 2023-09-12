using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    [Serializable, AddTypeMenu("RectMask")]
    public class ToggleTweenRectMask : IToggleTweenHandler {
        
        [SerializeField] private RectMask2D _target;
        [SerializeField] private Slider.Direction _direction;

        private Vector4 _start;
        
        public void OnStart(bool state) {
            _start = _target.padding;
        }

        public void OnUpdate(bool state, float progress) {
            Vector4 target = ResolveTargetState(state);
            _target.padding = Vector4.Lerp(_start, target, progress);
        }
        
        public Vector4 ResolveTargetState(bool state) {
            if(state) return Vector4.zero;
            Rect rect = _target.rectTransform.rect;
            return _direction switch {
                Slider.Direction.LeftToRight => new Vector4(rect.width, 0, 0, 0),
                Slider.Direction.RightToLeft => new Vector4(0, 0, rect.width, 0),
                Slider.Direction.BottomToTop => new Vector4(0, rect.height, 0, 0),
                Slider.Direction.TopToBottom => new Vector4(0, 0, 0, rect.height),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void FinalizeState(bool state) {
            _target.padding = ResolveTargetState(state);
        }

        private void GetPaper() {
            Debug.Log("GetPaper");
        }
    }
}