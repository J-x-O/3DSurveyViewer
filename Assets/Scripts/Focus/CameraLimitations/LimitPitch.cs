using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    [Serializable]
    public class LimitPitch : ILimitation {
        
        [SerializeField] private float _minPitch = -89;
        [SerializeField] private float _maxPitch = 89;
        
        public SphericalPosition Apply(SphericalPosition position, Vector3 worldPosition) {
            position.Pitch = Mathf.Clamp(position.Pitch, _minPitch * Mathf.Deg2Rad, _maxPitch * Mathf.Deg2Rad);
            return position;
        }
        
        public void DrawGizmos(SphericalPosition sphericalPosition, Vector3 worldPosition) {
            Vector3 dong = Vector3.forward * sphericalPosition.Radius;
            Gizmos.DrawLine(worldPosition, worldPosition - Quaternion.Euler(_minPitch, 0, 0) * dong);
            Gizmos.DrawLine(worldPosition, worldPosition - Quaternion.Euler(_maxPitch, 0, 0) * dong);
        }
    }
}