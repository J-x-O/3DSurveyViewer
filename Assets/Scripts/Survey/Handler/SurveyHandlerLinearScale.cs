using System;
using Beta.Devtools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survey.Handler {
    public class SurveyHandlerLinearScale : GenericSurveyHandler<SurveyQuestionLinearScale> {

        public override event Action OnValueChanged;
        public override event Action OnInputValid;
        public override event Action OnInputInvalid;
        
        public int Value => (int) _slider.value;
        
        [SerializeField] private TMP_Text _labelFrom;
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _labelTo;

        private bool _anyInput;
        
        private void OnEnable() => _slider.onValueChanged.AddListener(HandleValueChanged);

        private void HandleValueChanged(float arg0) {
            _anyInput = true;
            OnValueChanged?.Invoke();
            OnInputValid?.Invoke();
        }

        public override void AssignQuestionData(SurveyQuestionLinearScale data) {
            ApplyLabel(_labelFrom, data.LabelFrom);
            _slider.wholeNumbers = true;
            _slider.minValue = data.From;
            _slider.maxValue = data.To;
            ApplyLabel(_labelTo, data.LabelTo);
            ResetInput();
        }
        
        private void ApplyLabel(TMP_Text target, Optional<string> data) {
            if (data.Enabled) target.text = data.Value;
            else target.gameObject.SetActive(false);
        }

        public override bool IsInputValid() {
            return _anyInput;
        }

        public override void ResetInput() {
            _slider.value = _slider.minValue + (_slider.maxValue - _slider.minValue) / 2;
            _anyInput = false;
            OnInputInvalid?.Invoke();
        }

        public override string ExtractEnteredData() {
            return Value.ToString();
        }

        
    }
}