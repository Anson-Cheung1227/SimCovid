using UnityEngine;

namespace SimCovid.UI
{
    /// <summary>
    /// Handles all tooltips
    /// </summary>
    public class TooltipSystem : MonoBehaviour
    {
        public static TooltipSystem Instance;
        [field:SerializeField] public GameObject _tooltip { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
        public static void Show(string content, string header = "")
        {
            Instance._tooltip.GetComponent<Tooltip>().SetText(content, header);
            Instance._tooltip.SetActive(true);
        }
        public static void Hide()
        {
            Instance._tooltip.SetActive(false);
        }
    }
}
