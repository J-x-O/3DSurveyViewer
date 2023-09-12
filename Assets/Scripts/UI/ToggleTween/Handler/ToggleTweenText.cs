using System;
using TMPro;
using UnityEngine;

namespace UI {
    
    [Serializable, AddTypeMenu("Text")]
    public class ToggleTweenText : IToggleTweenHandler {

        [SerializeField] private TMP_Text _text;

        private Color _startColor;
        
        public void OnStart(bool state) {
            _startColor = _text.color;
        }

        public void OnUpdate(bool state, float progress) {
            Color target = state ? Color.white : Color.clear;
            _text.color = Color.Lerp(_startColor, target, progress);
        }

        public void FinalizeState(bool state) {
            _text.color = state ? Color.white : Color.clear;
        }
    }
}