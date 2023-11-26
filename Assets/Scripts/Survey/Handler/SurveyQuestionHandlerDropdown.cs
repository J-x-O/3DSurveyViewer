using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Utility;

namespace Survey.Handler {
    public class SurveyQuestionHandlerDropdown : GenericSurveyQuestionHandler<SurveyQuestionDropdown> {

        public override event Action OnValueChanged;
        public override event Action OnInputValid;
        public override event Action OnInputInvalid;
        
        public int Option => _dropdown.value;
        public string OptionText => _dropdown.options[_dropdown.value].text;
        
        [SerializeField] private TMP_Dropdown _dropdown;

        private void OnEnable() => _dropdown.onValueChanged.AddListener(HandleValueChanged);
        private void OnDisable() => _dropdown.onValueChanged.RemoveListener(HandleValueChanged);

        private void HandleValueChanged(int arg0) {
            OnValueChanged?.Invoke();
            if (arg0 == 0) OnInputInvalid?.Invoke();
            else OnInputValid?.Invoke();
        }

        public override void AssignQuestionData(SurveyQuestionDropdown data) {
            _dropdown.options = data.Options
                .Select(option => new TMP_Dropdown.OptionData(option))
                .ShuffleIf(data.RandomizeOrder)
                .Prepend(new TMP_Dropdown.OptionData("Select an option"))
                .ToList();
        }

        public override bool IsInputValid() {
            return _dropdown.value != 0;
        }

        public override void ResetInput() {
            _dropdown.value = 0;
        }

        public override string ExtractEnteredData() {
            return OptionText;
        }
    }
}