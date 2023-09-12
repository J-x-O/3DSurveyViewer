using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DependentToggleTween : ToggleTween
    {
        [SerializeField, WarnNull] private ToggleTween _parent;
        [SerializeField] private bool _requiredParentState = true;

        private void Start() {
            _parent?.OnToggleStateChange.AddListener(state => {
                if (state != _requiredParentState && IsInspectorOpen) {
                    Close();
                }
            });
        }
    }
}

