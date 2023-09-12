using System;
using UnityEngine;

namespace Attributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class DegreeAttribute : PropertyAttribute { }
}