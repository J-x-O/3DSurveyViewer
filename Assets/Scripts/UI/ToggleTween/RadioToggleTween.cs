using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class RadioToggleTween : MonoBehaviour {
        [SerializeField] private List<ToggleTween> _radioList;

        private void Start() {
            for (int i = 0; i < _radioList.Count; i++) {
                int scopedI = i;
                _radioList[i].OnToggleStateChange.AddListener(state => {
                    if (!state) return;
                    // Opened Radio Component
                    for (int j = 0; j < _radioList.Count; j++) {
                        if (j != scopedI && _radioList[j].IsInspectorOpen) {
                            // Trying to Close other Radio Component
                            _radioList[j].SetOpen(false);
                        }
                    }
                });
            }
        }
    }
}
