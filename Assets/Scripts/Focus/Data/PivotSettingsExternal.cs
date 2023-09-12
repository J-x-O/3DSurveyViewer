using UnityEngine;

namespace Focus.Data {

    [CreateAssetMenu(menuName = "3D Survey Viewer/Pivot Settings")]
    public class PivotSettingsExternal : ScriptableObject {
     
        [field:SerializeField] public PivotSettings Settings { get; private set; }
    }

}