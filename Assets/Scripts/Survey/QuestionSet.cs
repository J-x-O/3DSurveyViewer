using System.Collections.Generic;
using UnityEngine;

namespace Survey {
    
    [CreateAssetMenu(menuName = "3D Survey Viewer/Question Set")]
    public class QuestionSet : ScriptableObject {
        
        public IReadOnlyList<ISurveyElement> Questions => _questions;
        [SerializeReference, SubclassSelector] private ISurveyElement[] _questions;
        
    }
}