using System;
using System.Collections.Generic;
using System.Linq;
using Focus;
using TNRD;
using UnityEngine;

namespace ModelViewing {
    public class FocusPointGroup : MonoBehaviour {
        
        public IFocusable PrimaryFocus => _primaryFocus.Value;
        [SerializeField] private SerializableInterface<IFocusable> _primaryFocus;

        public IEnumerable<IFocusable> FocusPoints => _focusPoints
            .Append(_primaryFocus)
            .Select(focus => focus.Value);
        [SerializeField] private List<SerializableInterface<IFocusable>> _focusPoints;
        
    }
}