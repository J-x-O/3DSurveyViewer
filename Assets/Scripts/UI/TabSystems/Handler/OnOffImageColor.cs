using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TabSystems.Handler {
    
    [Serializable]
    public class OnOffImageColor {
        
        [SerializeField] private Image _target;
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;
        
        public void Activate() {
            if(_target != null) _target.color = _onColor;
        }

        public void Deactivate() {
            if(_target != null) _target.color = _offColor;
        }
    }
}