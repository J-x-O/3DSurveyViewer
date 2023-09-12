using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    [Serializable]
    public class LimitRadius : ILimitation {
        
        [SerializeField] private float _minRadius = 0.01f;
        [SerializeField] private float _maxRadius = 1;
        
        public SphericalPosition Apply(SphericalPosition position) {
            position.Radius = Mathf.Clamp(position.Radius, _minRadius, _maxRadius);
            return position;
        }
        
        public void DrawGizmos(SphericalPosition sphericalPosition, Vector3 worldPosition) {
            Gizmos.DrawWireSphere(worldPosition, _minRadius);
            Gizmos.DrawWireSphere(worldPosition, _maxRadius);
        }
    }
}