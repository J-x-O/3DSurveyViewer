using System;
using Focus.Data;
using JescoDev.Utility.EventUtility.EventUtility;
using UnityEngine;

namespace Focus {
    public class FocusPointExternalSettings : MonoBehaviour, IFocusable {
        
        public event Action OnFocus;
        public PivotSettings Settings => _settings != null ? _settings.Settings : null;
        [SerializeField] private PivotSettingsExternal _settings;

        [field:SerializeField] public bool IsFocusable { get; set; }
        public Vector3 WorldPosition => transform.position;
        
        public void HandleFocus() => OnFocus.TryInvoke();
        
        private void OnDrawGizmosSelected() {
            Settings?.DrawGizmos(transform.position);
        }
    }
}