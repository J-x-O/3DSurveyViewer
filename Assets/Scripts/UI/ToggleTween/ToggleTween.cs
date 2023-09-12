using System.Collections;
using System.Collections.Generic;
using JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween;
using UnityEngine;
using UnityEngine.Events;

namespace UI {
    public class ToggleTween : MonoBehaviour {

        public UnityEvent<bool> OnToggleStateChange;
    
        public bool IsInspectorOpen { get; private set; }

        [SerializeField] private bool _defaultState = false;
        [SerializeField] private float _transitionDuration = 0.5f;
        [Space] 
        private Coroutine _transitionRoutine;
        
        [SerializeReference, SubclassSelector] private List<IToggleTweenHandler> _handlers = new();

        private void Awake() {
            foreach (IToggleTweenHandler tweenHandler in _handlers) {
                tweenHandler.FinalizeState(_defaultState);
            }
            OnToggleStateChange.Invoke(_defaultState);
        }

        public void ToggleOpen() => SetOpen(!IsInspectorOpen);

        public void Open() => SetOpen(true);
        public void Close() => SetOpen(false);
    
        public void SetOpen(bool state) {
            if (IsInspectorOpen == state) return;
            IsInspectorOpen = state;
            OnToggleStateChange?.Invoke(state);
            TryTransition(state);
        }

        public void SetOpenBypassListeners(bool state) {
            if (IsInspectorOpen == state) return;
            IsInspectorOpen = state;
            TryTransition(state);
        }

        private void TryTransition(bool state) {
            if (_transitionRoutine != null) StopCoroutine(_transitionRoutine);
            _transitionRoutine = StartCoroutine(TransitionToState(state));
        }

        private IEnumerator TransitionToState(bool targetState) {

            foreach (IToggleTweenHandler tweenHandler in _handlers) 
                tweenHandler.OnStart(targetState);
            
            float passedTime = 0;
            while (passedTime < 1) {
                passedTime += Time.deltaTime / _transitionDuration;
                float progress = TweenInfo.EaseQuadInOut(passedTime);
                foreach (IToggleTweenHandler tweenHandler in _handlers) {
                    tweenHandler.OnUpdate(targetState, progress);
                }
                yield return null;
            }

            foreach (IToggleTweenHandler tweenHandler in _handlers) {
                tweenHandler.FinalizeState(targetState);
            }
        }
    }
}
