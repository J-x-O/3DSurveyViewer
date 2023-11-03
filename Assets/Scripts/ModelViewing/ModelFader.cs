using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModelViewing {
    public class ModelFader : MonoBehaviour {
        
        [SerializeField] private ModelRowManager _manager;
        [SerializeField] private List<MeshRenderer[]> _renderer;
        [SerializeField] private string _fadeProperty = "_Fade";
        [SerializeField] private float _fadeDuration = 0.25f;
        
        private void Awake() {
            _renderer = _manager.Instances
                .Select(focus => focus != null ? focus.GetComponentsInChildren<MeshRenderer>() : null)
                .ToList();

            EnableTarget(0);
        }
        
        private void OnEnable() => _manager.OnNewSet += FadeIn;
        private void OnDisable() => _manager.OnNewSet -= FadeIn;

        private void FadeIn() {
            StopAllCoroutines();
            StartCoroutine(FadeToRoutine(_manager.CurrentIndex));
        }

        private IEnumerator FadeToRoutine(int target) {

            // enable target
            foreach (MeshRenderer meshRenderer in _renderer[target]) {
                meshRenderer.enabled = true;
            }
            
            float time = 0;
            float[][] start = _renderer
                .Select(list => 
                    list.Select(renderer => renderer.material.GetFloat(_fadeProperty))
                        .ToArray())
                .ToArray();
            while (time < _fadeDuration) {
                time += Time.deltaTime;
                float t = time / _fadeDuration;
                for (int element = 0; element < _renderer.Count; element++) {
                    float targetValue = element == target ? 0 : 1;
                    for (int sub = 0; sub < _renderer[element].Length; sub++) {
                        float lerp = Mathf.Lerp(start[element][sub], targetValue, t);
                        _renderer[element][sub].material.SetFloat(_fadeProperty, lerp);
                    }
                }
                yield return null;
            }
            
            EnableTarget(target);
        }

        private void EnableTarget(int target) {
            for (int element = 0; element < _renderer.Count; element++) {
                for (int sub = 0; sub < _renderer[element].Length; sub++) {
                    if (element != target) _renderer[element][sub].enabled = false;
                    else _renderer[element][sub].material.SetFloat(_fadeProperty, 0);
                }
            }
        }
    }
}