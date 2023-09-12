using System;
using ModelViewing;
using UnityEngine;

namespace UI {
    public class OpenFinalizePopupOnFinish : MonoBehaviour {
        
        [SerializeField] private ModelRowManager _manager;
        [SerializeField] private GameObject _target;

        private void OnEnable() => _manager.OnFinish += HandleFinished;
        private void OnDisable() => _manager.OnFinish -= HandleFinished;
        
        private void Start() => _target.SetActive(false);

        private void HandleFinished() => _target.SetActive(true);
    }
}