using System;
using UnityEngine;

namespace Survey.Handler {
    public abstract class SurveyHandler : MonoBehaviour {
        
        public abstract Type GetSurveyType();
        
        public abstract void AssignElementData(ISurveyElement element);
        
    }
}