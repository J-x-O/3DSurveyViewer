using System;
using Focus.Data;
using JescoDev.Utility.EventUtility.EventUtility;
using UnityEngine;

namespace Focus {

    public class FocusPoint : MonoBehaviour, IFocusable {
        public event Action OnFocus;
        [field:SerializeField] public PivotSettings Settings { get; private set; }
        [field:SerializeField] public bool IsFocusable { get; set; }
        public Vector3 WorldPosition => transform.position;
        
        public void HandleFocus() => OnFocus.TryInvoke();

        private void OnDrawGizmosSelected() => Settings.DrawGizmos(transform.position);
        
    }

}
