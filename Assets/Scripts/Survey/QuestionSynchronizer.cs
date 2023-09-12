using System;
using ModelViewing;
using UnityEngine;

namespace Survey {
    public class QuestionSynchronizer : MonoBehaviour {

        [SerializeField] private ModelRowManager _rowManager;
        [SerializeField] private QuestionSpawner _questionSpawner;

        [Serializable]
        private struct IdentifiedQuestionSet {
            [field:SerializeField] public string Title { get; private set; }
            [field:SerializeField] public QuestionSet Question { get; private set; }
        }
        
        [SerializeField] private IdentifiedQuestionSet[] _questions;
        
        private void OnEnable() => _rowManager.OnNewSet += UpdateQuestions;
        private void OnDisable() => _rowManager.OnNewSet -= UpdateQuestions;

        private void Start() => UpdateQuestions();

        private void UpdateQuestions() {
            IdentifiedQuestionSet set = _questions[_rowManager.CurrentIndex];
            _questionSpawner.SetTitle(set.Title);
            _questionSpawner.Spawn(set.Question.Questions);
        }
    }
}