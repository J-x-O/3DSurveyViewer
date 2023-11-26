using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survey {
    
    [Serializable]
    public class SurveyQuestionDropdown : SurveyQuestion {
        
        [field:SerializeField] public List<string> Options { get; private set; }
        [field:SerializeField] public bool RandomizeOrder { get; private set; }
        
    }
}