using System;
using Attributes;
using UnityEngine;

namespace Focus {
    
    [Serializable]
    public struct SphericalPosition {

        [Degree] public float Yaw;
        [Degree] public float Pitch;
        public float Radius;
    
        public Vector3 ToCartesian(){
            Vector3 result = new Vector3();
            result.x = Mathf.Sin(Yaw) * Mathf.Cos(Pitch);
            result.y = Mathf.Sin(Pitch);
            result.z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch);
            return (result * Radius);
        }

        public static SphericalPosition Lerp(SphericalPosition a, SphericalPosition b, float t) {
            SphericalPosition result = new SphericalPosition();
            a.Yaw %= (2 * Mathf.PI);
            b.Yaw %= (2 * Mathf.PI);
            if (Mathf.Abs(a.Yaw - b.Yaw) > Mathf.PI) {
                if (a.Yaw > b.Yaw) b.Yaw += 2 * Mathf.PI;
                else a.Yaw += 2 * Mathf.PI;
            }
            result.Yaw = Mathf.Lerp(a.Yaw, b.Yaw, t);
            
            result.Pitch = Mathf.Lerp(a.Pitch, b.Pitch, t);
            result.Radius = Mathf.Lerp(a.Radius, b.Radius, t);
            return result;
        }
    }
}