using System;
using UnityEngine;

namespace Focus.CameraLimitations {
    
    [Serializable]
    public class LimitCylinder : ILimitation {
        
        [SerializeField] private float _radius;
        
        public SphericalPosition Apply(SphericalPosition position, Vector3 worldPosition) {
            Vector3 cartesian = position.ToCartesian();
            if(new Vector2(cartesian.x, cartesian.z).magnitude < _radius) {
                // find the next legal pitch for the current radius
                position.Pitch = Mathf.Sign(position.Pitch) * Mathf.Acos(_radius / position.Radius);
            }
            return position;
        }
        
        public void DrawGizmos(SphericalPosition sphericalPosition, Vector3 worldPosition) {
            float height = sphericalPosition.Radius;
            Vector3 topCenter = Vector3.up * height;
            Vector3 bottomCenter = Vector3.down * height;
            
            Matrix4x4 old = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(worldPosition + topCenter, Quaternion.identity, new Vector3(1,0,1));
            Gizmos.DrawWireSphere(Vector3.zero, _radius);
            Gizmos.matrix = Matrix4x4.TRS(worldPosition + bottomCenter, Quaternion.identity, new Vector3(1,0,1));
            Gizmos.DrawWireSphere(Vector3.zero, _radius);
            
            Gizmos.matrix = Matrix4x4.TRS(worldPosition, Quaternion.identity, Vector3.one);
            Gizmos.DrawLine(topCenter + Vector3.left * _radius, bottomCenter + Vector3.left * _radius);
            Gizmos.DrawLine(topCenter + Vector3.back * _radius, bottomCenter + Vector3.back * _radius);
            Gizmos.DrawLine(topCenter + Vector3.right * _radius, bottomCenter + Vector3.right * _radius);
            Gizmos.DrawLine(topCenter + Vector3.forward * _radius, bottomCenter + Vector3.forward * _radius);
            Gizmos.matrix = old;
        }
    }
}