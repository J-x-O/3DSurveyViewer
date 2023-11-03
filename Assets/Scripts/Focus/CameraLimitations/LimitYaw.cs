
using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    [Serializable]
    public class LimitYaw : ILimitation {
        
        [SerializeField] private float _minYaw = -180;
        [SerializeField] private float _maxYaw = 180;
        
        public SphericalPosition Apply(SphericalPosition position) {
            position.Yaw = Mathf.Clamp(position.Yaw, _minYaw * Mathf.Deg2Rad, _maxYaw * Mathf.Deg2Rad);
            return position;
        }
        
        
        public void DrawGizmos(SphericalPosition sphericalPosition, Vector3 worldPosition) {
            Vector3 dong = Vector3.back * sphericalPosition.Radius;
            Gizmos.DrawLine(worldPosition, worldPosition + Quaternion.Euler(0, _minYaw, 0) * dong);
            Gizmos.DrawLine(worldPosition, worldPosition + Quaternion.Euler(0, _maxYaw, 0) * dong);
        }
    }
}