using System;
using System.Collections;
using System.Collections.Generic;
using JescoDev.Utility.EventUtility.EventUtility;
using Survey.Handler;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Survey {
    public class QuestionSpawner : MonoBehaviour {

        public event Action OnHandlersCleared;
        public event Action OnHandlersSpawned;
        
        [SerializeField] private List<SurveyHandler> _prefabs;
        [SerializeField] private GameObject _errorPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private TMP_Text _title;
        
        public string Title => _title.text;
        
        public IReadOnlyList<SurveyHandler> Handlers => _instances;
        private readonly List<SurveyHandler> _instances = new List<SurveyHandler>();
        

        public void SetTitle(string title) => _title.text = title;

        public void Clear() {
            foreach (SurveyHandler instance in _instances) {
                Destroy(instance.gameObject);
            }
            _instances.Clear();
            OnHandlersCleared.TryInvoke();
        }
        
        public void Spawn(IEnumerable<SurveyQuestion> questions) {
            Clear();
            foreach (SurveyQuestion question in questions) {
                Spawn(question);
            }
            OnHandlersSpawned.TryInvoke();
        }
        
        private void Spawn(SurveyQuestion question) {
            SurveyHandler prefab = _prefabs.Find(p => p.GetSurveyType() == question.GetType());
            if (prefab == null) {
                Instantiate(_errorPrefab, _parent);
                Debug.LogWarning("No prefab found for question type " + question.GetType());
                return;
            }
            SurveyHandler instance = Instantiate(prefab, _parent);
            instance.AssignQuestionData(question);
            _instances.Add(instance);
        }

    }
}