using System;
using UnityEngine;

namespace Survey {
    
    [Serializable]
    public class SurveyLabel : ISurveyElement {
        
        [field:SerializeField, TextArea(3, 10)] public string Text { get; private set; }
        
    }
}