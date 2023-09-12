using System;
using UnityEngine;

namespace Survey {
    
    [Serializable]
    public abstract class SurveyQuestion {
        
        [field:SerializeField] public string Question { get; protected set; }
        
    }
}