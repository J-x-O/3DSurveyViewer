using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    [Serializable]
    public class LimitLowerWorld : ILimitation {
        
        [SerializeField] private float _lowerBound;
        
        public SphericalPosition Apply(SphericalPosition position, Vector3 worldPosition) {
            float height = position.ToCartesian().y;
            if (height < _lowerBound - worldPosition.y)
                position.Pitch = Mathf.Asin((_lowerBound - worldPosition.y) / position.Radius);
            return position;
        }
        
        public void DrawGizmos(SphericalPosition sphericalPosition, Vector3 worldPosition) {
            float size = sphericalPosition.Radius * 2;
            worldPosition.y = 0;
            Gizmos.DrawCube(worldPosition + Vector3.up * _lowerBound, new Vector3(size, 0.0001f, size));
        }
    }
}