using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Focus.CameraDisable {
    
    [Serializable]
    public class DisableCamSchwurbelRatio : IDisableCam {
        
        [SerializeField] private float _schwurbelRatio = 0.33f;
        [SerializeField] private Scrollbar.Direction _direction;
        
        public bool ShouldDisableCamera() {
            Vector2 pos = Mouse.current.position.ReadValue();
            Vector2 size = new Vector2(Screen.width, Screen.height);
            return _direction switch {
                Scrollbar.Direction.LeftToRight => pos.x / size.x < _schwurbelRatio,
                Scrollbar.Direction.RightToLeft => pos.x / size.x > 1 - _schwurbelRatio,
                Scrollbar.Direction.BottomToTop => pos.y / size.y < _schwurbelRatio,
                Scrollbar.Direction.TopToBottom => pos.y / size.y > 1 - _schwurbelRatio,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}