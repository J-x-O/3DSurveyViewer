using System;

namespace UI {
    
    public interface IToggleTweenHandler {
        
        public void OnStart(bool state);
        public void OnUpdate(bool state, float progress);
        public void FinalizeState(bool state);
    }
}