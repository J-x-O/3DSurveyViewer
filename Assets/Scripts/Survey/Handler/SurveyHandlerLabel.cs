using System;
using TMPro;
using UnityEngine;

namespace Survey.Handler {
    public class SurveyHandlerLabel : SurveyHandler {
        
        [SerializeField] private TMP_Text _textField;
        
        public override Type GetSurveyType() => typeof(SurveyLabel);

        public override void AssignElementData(ISurveyElement element) {
            if(element is not SurveyLabel label) throw new Exception("Invalid Question Type");
            _textField.text = label.Text;
        }
    }
}