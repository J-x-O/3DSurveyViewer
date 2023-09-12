using System;
using TMPro;
using UnityEngine;

namespace Survey.Handler {
    public class SurveyHandlerInt : GenericSurveyHandler<SurveyQuestionInt> {
        
        public override event Action OnValueChanged;
        public override event Action OnInputValid;
        public override event Action OnInputInvalid;
        
        public int Value => int.Parse(_inputField.text);
        
        [SerializeField] private TMP_InputField _inputField;

        private SurveyQuestionInt _data;
        
        private void OnEnable() => _inputField.onValueChanged.AddListener(HandleValueChanged);
        private void OnDisable() => _inputField.onValueChanged.RemoveListener(HandleValueChanged);

        private void HandleValueChanged(string text) {
            OnValueChanged?.Invoke();
            if(string.IsNullOrWhiteSpace(text)) {
                OnInputInvalid?.Invoke();
                return;
            }
            
            if(_data.Minimum.Enabled && int.Parse(text) < _data.Minimum.Value) {
                _inputField.SetTextWithoutNotify(_data.Minimum.Value.ToString());
            }
            if(_data.Maximum.Enabled && int.Parse(text) > _data.Maximum.Value) {
                _inputField.SetTextWithoutNotify(_data.Maximum.Value.ToString());
            }
            OnInputValid?.Invoke();
        }

        public override void AssignQuestionData(SurveyQuestionInt data) {
            _data = data;
            _inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
            ResetInput();
        }

        public override bool IsInputValid() {
            return !string.IsNullOrWhiteSpace(_inputField.text);
        }

        public override void ResetInput() {
            _inputField.text = _data.DefaultValue.ToString();
            OnInputInvalid?.Invoke();
        }

        public override string ExtractEnteredData() {
            return _inputField.text;
        }

        
    }
}