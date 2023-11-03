using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    [Serializable]
    public class LimitPitchDefault : ILimitation {
        
        private const float EDGE = Mathf.PI / 2 * 0.99f;
        
        public SphericalPosition Apply(SphericalPosition position) {
            position.Pitch = Mathf.Clamp(position.Pitch, -EDGE, EDGE);
            return position;
        }
    }
}