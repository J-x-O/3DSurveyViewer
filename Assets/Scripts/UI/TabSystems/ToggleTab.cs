using UnityEngine;

namespace Resources.Scripts.Content.UI.Utility {

    /// <summary>
    /// Small utility to extend tab systems to have a fallback tab
    /// the tab is used if the user switches to the tab but the tab is already active
    /// </summary>
    public class ToggleTab : MonoBehaviour {

        [Tooltip("The tab system that is targeted")]
        [SerializeField] private TabSystem _tabSystem;

        [Tooltip("The tab that will be activated if the targeted Tab is already active")]
        [SerializeField] private int _fallbackTab;

        public void ActivateTabWithFallback(int targetTab) {
            _tabSystem.SwitchToTab(_tabSystem.ActiveTab == targetTab ? _fallbackTab : targetTab);
        }
    }

}