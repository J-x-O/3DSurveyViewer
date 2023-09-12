using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    [Serializable, AddTypeMenu("Image")]
    public class ToggleTweenImage : IToggleTweenHandler {

        [SerializeField] private Image _image;
        
        private Color _start;

        public void OnStart(bool state) {
            _start = _image.color;
        }

        public void OnUpdate(bool state, float progress) {
            Color target = state ? Color.white : Color.clear;
            _image.color = Color.Lerp(_start, target, progress);
        }

        public void FinalizeState(bool state) {
            _image.color = state ? Color.white : Color.clear;
        }
    }
}