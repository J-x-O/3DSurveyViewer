using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.TabSystems.Handler {
    
    [Serializable]
    public class OnOffMultiple : IOnOffHandler {
        
        [SerializeReference, SubclassSelector] private List<IOnOffHandler> _handler = new();
        
        public void Activate() {
            foreach (IOnOffHandler handler in _handler) handler?.Activate();
        }

        public void Deactivate() {
            foreach (IOnOffHandler handler in _handler) handler?.Deactivate();
        }
    }
}