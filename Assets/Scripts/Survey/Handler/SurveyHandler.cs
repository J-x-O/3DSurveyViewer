using System;
using TMPro;
using UnityEngine;

namespace Survey.Handler {
    public abstract class SurveyHandler : MonoBehaviour {

        public abstract event Action OnValueChanged;
        public abstract event Action OnInputValid;
        public abstract event Action OnInputInvalid;
        
        public string Question => _questionField.text;
        [SerializeField] protected TMP_Text _questionField;
        
        public abstract Type GetSurveyType();
        public abstract bool IsInputValid();
        public abstract void AssignQuestionData(SurveyQuestion data);
        public abstract void ResetInput();
        public abstract string ExtractEnteredData();
        
    }
    
    public abstract class GenericSurveyHandler<T> : SurveyHandler where T : SurveyQuestion {
        public override Type GetSurveyType() => typeof(T);
        public override void AssignQuestionData(SurveyQuestion data) {
            if(data is not T t) throw new Exception("Invalid Question Type");
            _questionField.text = t.Question;
            AssignQuestionData(t);
        }
        
        public abstract void AssignQuestionData(T data);
    }
}