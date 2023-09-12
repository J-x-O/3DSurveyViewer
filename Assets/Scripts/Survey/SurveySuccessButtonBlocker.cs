using System;
using System.Linq;
using Survey.Handler;
using UnityEngine;
using UnityEngine.UI;

namespace Survey {
    public class SurveySuccessButtonBlocker : MonoBehaviour {
        
        [SerializeField] private QuestionSpawner _handler;
        [SerializeField] private Button _target;

        private void OnEnable() => _handler.OnHandlersSpawned += HandleHandlersSpawned;
        private void OnDisable() => _handler.OnHandlersSpawned -= HandleHandlersSpawned;

        private void HandleHandlersSpawned() {
            foreach (SurveyHandler handler in _handler.Handlers) {
                handler.OnValueChanged += Reevaluate;
            }
            Reevaluate();
        }

        private void Reevaluate() {
            _target.interactable = _handler.Handlers.All(handler => handler.IsInputValid());
        }
    }
}