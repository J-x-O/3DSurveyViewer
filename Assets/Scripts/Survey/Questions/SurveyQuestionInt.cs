using System;
using Beta.Devtools;
using UnityEngine;

namespace Survey {
    
    [Serializable]
    public class SurveyQuestionInt : SurveyQuestion {
        
        [field:SerializeField] public int DefaultValue { get; private set; }
        [field:SerializeField] public Optional<int> Minimum { get; private set; }
        [field:SerializeField] public Optional<int> Maximum { get; private set; }
        
    }
}