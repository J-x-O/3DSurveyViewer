using System;
using Beta.Devtools;
using UnityEngine;

namespace Survey {
    
    [Serializable]
    public class SurveyQuestionLinearScale : SurveyQuestion {

        [field: SerializeField] public int From { get; private set; } = 1;
        [field:SerializeField] public Optional<string> LabelFrom { get; private set; }
        [field: Space]
        [field: SerializeField] public int To { get; private set; } = 5;
        [field:SerializeField] public Optional<string> LabelTo { get; private set; }
        
    }
}