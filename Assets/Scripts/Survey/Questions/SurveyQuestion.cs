using System;
using UnityEngine;

namespace Survey {
    
    [Serializable]
    public abstract class SurveyQuestion: ISurveyElement {
        
        [field:SerializeField] public string Question { get; protected set; }
        [field:SerializeField] public bool Optional { get; protected set; } = false;

    }
}