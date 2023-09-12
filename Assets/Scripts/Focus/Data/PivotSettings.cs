using System;
using System.Collections.Generic;
using Focus.CameraLimitations;
using UnityEngine;

namespace Focus.Data {
    
    [Serializable]
    public class PivotSettings {
        [field:SerializeField] public SphericalPosition StartPosition { get; private set; }
        public IReadOnlyList<ILimitation> Limitations => _limitations;
        [SerializeReference, SubclassSelector] public List<ILimitation> _limitations;
        
        public void DrawGizmos(Vector3 worldPosition) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(worldPosition, StartPosition.Radius);
            Gizmos.DrawSphere(worldPosition - StartPosition.ToCartesian(), StartPosition.Radius / 10);
            
            Gizmos.color = new Color(1,0,0,0.5f);
            foreach (ILimitation limitation in Limitations) {
                limitation?.DrawGizmos(StartPosition, worldPosition);
            }
        }
    }
}