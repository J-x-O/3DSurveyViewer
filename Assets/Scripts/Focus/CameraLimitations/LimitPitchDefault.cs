using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    [Serializable]
    public class LimitPitchDefault : ILimitation {
        public SphericalPosition Apply(SphericalPosition position) {
            position.Pitch = Mathf.Clamp(position.Pitch, -Mathf.PI / 2, Mathf.PI / 2);
            return position;
        }
    }
}