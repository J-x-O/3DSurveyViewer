using System;
using Focus.Data;
using JescoDev.Utility.EventUtility.EventUtility;
using UnityEngine;

namespace Focus {

    public class FocusPoint : MonoBehaviour, IFocusable {
        public event Action OnFocus;
        public event Action OnUnfocus;
        [field:SerializeField] public PivotSettings Settings { get; private set; }
        [field:SerializeField] public bool IsFocusable { get; set; }
        
        public bool IsSelected { get; private set; }
        public Vector3 WorldPosition => transform.position;
        
        public void HandleFocus() {
            IsSelected = true;
            OnFocus.TryInvoke();
        }

        public void HandleUnfocus() {
            IsSelected = false;
            OnUnfocus.TryInvoke();
        }

        private void OnDrawGizmosSelected() => Settings.DrawGizmos(transform.position);
        
    }

}
