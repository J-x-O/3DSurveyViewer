using System;
using System.Collections.Generic;
using System.Linq;
using Focus;
using ModelViewing;
using UnityEngine;

namespace UI {
    
    [DefaultExecutionOrder(5)]
    public class UIPivotVisualizer : MonoBehaviour {

        [SerializeField] private ModelRowManager _manager;
        [SerializeField] private PivotCameraMover _camera;
        [SerializeField] private UIPivotRepresentation _prefab;

        private readonly List<UIPivotRepresentation> _pivots = new List<UIPivotRepresentation>();
        
        private void OnEnable() => _manager.OnNewSet += UpdatePivots;

        private void OnDisable() => _manager.OnNewSet -= UpdatePivots;

        private void Start() => UpdatePivots();

        private void Update() {
            // sort children by z position
            List<Transform> children = new(transform.childCount);
            for (int i = 0; i < transform.childCount; i++) children.Add(transform.GetChild(i));
            List<Transform> sorted = children.ToList();
            sorted.Sort((t1, t2) => {
                Vector3 pos1 = t1.position;
                Vector3 pos2 = t2.position;
                if (!Mathf.Approximately(pos1.z, pos2.z)) return pos2.z.CompareTo(pos1.z);
                if (!Mathf.Approximately(pos1.x, pos2.x)) return pos2.x.CompareTo(pos1.x);
                if (!Mathf.Approximately(pos1.y, pos2.y)) return pos2.y.CompareTo(pos1.y);
                return -1;
            });
            
            // order changed
            if(children.SequenceEqual(sorted)) return;
            foreach (Transform child in sorted) {
                child.SetAsLastSibling();
            }
        }

        private void UpdatePivots() {
            foreach (UIPivotRepresentation pivot in _pivots) {
                pivot.Die();
            }
            _pivots.Clear();
            
            foreach (IFocusable focusPoint in _manager.ActiveGroup.SecondaryFocus) {
                UIPivotRepresentation pivot = Instantiate(_prefab, transform);
                _pivots.Add(pivot);
                
                if (focusPoint is not MonoBehaviour mono) continue;
                pivot.BindToObject(_camera, focusPoint);
                
                if (!mono.TryGetComponent(out CustomPivotIcon icon)) continue;
                pivot.SetSprite(icon.Sprite);
            }
        }
    }
}