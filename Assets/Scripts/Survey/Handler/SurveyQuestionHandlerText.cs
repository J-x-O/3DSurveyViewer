using System;
using TMPro;
using UnityEngine;

namespace Survey.Handler {
    public class SurveyQuestionHandlerText : GenericSurveyQuestionHandler<SurveyQuestionText> {
        
        public override event Action OnValueChanged;
        public override event Action OnInputValid;
        public override event Action OnInputInvalid;
        
        [SerializeField] private TMP_InputField _inputField;

        private void OnEnable() => _inputField.onValueChanged.AddListener(HandleValueChanged);
        private void OnDisable() => _inputField.onValueChanged.RemoveListener(HandleValueChanged);

        private void HandleValueChanged(string arg0) {
            OnValueChanged?.Invoke();
            if (string.IsNullOrWhiteSpace(arg0)) OnInputInvalid?.Invoke();
            else OnInputValid?.Invoke();
        }

        public override void AssignQuestionData(SurveyQuestionText data) { }


        public override bool IsInputValid() {
            return !string.IsNullOrWhiteSpace(_inputField.text);
        }

        public override void ResetInput() {
            _inputField.text = "";
        }

        public override string ExtractEnteredData() {
            return _inputField.text;
        }
    }
}