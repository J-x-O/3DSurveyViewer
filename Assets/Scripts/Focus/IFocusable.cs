using System;
using Focus.Data;
using UnityEngine;

namespace Focus {
    
    public interface IFocusable {

        public event Action OnFocus;
        
        public PivotSettings Settings { get; }
        public bool IsFocusable { get; set; }
        public Vector3 WorldPosition { get; }

        public void HandleFocus();
    }
}
