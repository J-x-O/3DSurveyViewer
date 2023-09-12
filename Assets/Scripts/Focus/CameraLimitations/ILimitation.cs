using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    public interface ILimitation {
        public SphericalPosition Apply(SphericalPosition position);
        
        public void DrawGizmos(SphericalPosition sphericalPosition, Vector3 worldPosition) {}
    }
}