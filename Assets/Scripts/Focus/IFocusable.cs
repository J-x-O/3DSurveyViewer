using System;
using Focus.Data;
using UnityEngine;

namespace Focus {
    
    public interface IFocusable {

        public event Action OnFocus;
        public event Action OnUnfocus;
        
        public PivotSettings Settings { get; }
        public bool IsFocusable { get; set; }
        public bool IsSelected { get; }
        public Vector3 WorldPosition { get; }

        public void HandleFocus();
        public void HandleUnfocus();
    }
}
