using System;
using Survey.Handler;
using UnityEngine;
using UnityEngine.UI;

namespace Survey {
    public class SurveySuccessVisualizer : MonoBehaviour {

        [SerializeField] private SurveyHandler _handler;
        [SerializeField] private Image _image;
        [SerializeField] private Color _successColor;
        [SerializeField] private Color _failureColor;

        private void OnEnable() {
            _handler.OnInputValid += HandleInputValid;
            _handler.OnInputInvalid += HandleInputInvalid;
        }
        
        private void OnDisable() {
            _handler.OnInputValid -= HandleInputValid;
            _handler.OnInputInvalid -= HandleInputInvalid;
        }

        private void Start() => _image.color = _handler.IsInputValid() ? _successColor : _failureColor;

        private void HandleInputValid() => _image.color = _successColor;
        
        private void HandleInputInvalid() => _image.color = _failureColor;
    }
}