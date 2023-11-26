using System;
using TMPro;
using UnityEngine;

namespace Survey.Handler {
    public abstract class SurveyQuestionHandler : SurveyHandler {

        public abstract event Action OnValueChanged;
        public abstract event Action OnInputValid;
        public abstract event Action OnInputInvalid;
        
        
        public string Question => _questionField.text;
        [SerializeField] protected TMP_Text _questionField;
        
        public bool Optional { get; protected set; } = false;
        
        public abstract bool IsInputValid();

        public override void AssignElementData(ISurveyElement element) {
            if(element is not SurveyQuestion question) throw new Exception("Invalid Question Type");
            _questionField.text = question.Question;
            Optional = question.Optional;
            AssignQuestionData(question);
        }

        protected abstract void AssignQuestionData(SurveyQuestion data);
        
        public abstract void ResetInput();
        public abstract string ExtractEnteredData();
        
    }
    
    public abstract class GenericSurveyQuestionHandler<T> : SurveyQuestionHandler where T : SurveyQuestion {
        public override Type GetSurveyType() => typeof(T);
        protected override void AssignQuestionData(SurveyQuestion data) {
            if(data is not T t) throw new Exception("Invalid Question Type");
            _questionField.text = t.Question;
            Optional = t.Optional;
            AssignQuestionData(t);
        }

        public abstract void AssignQuestionData(T data);
    }
}