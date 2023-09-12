using System;
using UnityEngine;

namespace UI.TabSystems.Handler {
    
    [Serializable]
    public class OnOffGameObject : IOnOffHandler {

        [SerializeField] private GameObject _target;
        
        public void Activate() {
            if(_target != null) _target.SetActive(true);
        }

        public void Deactivate() {
            if(_target != null) _target.SetActive(false);
        }
    }
}