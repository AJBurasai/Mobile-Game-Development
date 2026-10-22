using UnityEngine;

/* This script handles the safe area for game ui, script taken directly
   from week 3 powerpoint slides. */
/*
namespace CMP5402.Week3
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        [Tooltip("Turn an edge off to let that side ignore the safe area, for example a bottom bar that should sit under the home indicator.")]
        [SerializeField] bool applyLeft = true, applyRight = true, applyTop = true, applyBottom = true;

        RectTransform panel;
        Rect lastSafe;
        Vector2Int lastSize;

        void Awake() => panel = GetComponent<RectTransform>();

        void OnEnable() => lastSize = Vector2Int.zero; // force a refresh

        void Update()
        {
            var size = new Vector2Int(Screen.width, Screen.height);
            Rect safe = Screen.safeArea;
            if (safe == lastSafe && size == lastSize) return;
            if (size.x <= 0 || size.y <= 0) return;

            lastSafe = safe;
            lastSize = size;

            // Convert pixel rectangle to 0-1 anchors.
            Vector2 min = safe.min, max = safe.max;
            min.x = applyLeft ? min.x / size.x : 0f;
            min.y = applyBottom ? min.y / size.y : 0f;
            max.x = applyRight ? max.x / size.x : 1f;
            max.y = applyTop ? max.y / size.y : 1f;

            panel.anchorMin = min;
            panel.anchorMax = max;
            panel.offsetMin = Vector2.zero;
            panel.offsetMax = Vector2.zero;
        }
    }
}

*/


namespace CMP5402.Week3
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        [Header("Safe Area Edges")]
        [Tooltip("Turn an edge off if you want that side to ignore the safe area.")]
        [SerializeField] private bool applyLeft = true;
        [SerializeField] private bool applyRight = true;
        [SerializeField] private bool applyTop = true;
        [SerializeField] private bool applyBottom = true;

        private RectTransform panel;

        private void Awake()
        {
            panel = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            ApplySafeArea();
        }

        private void LateUpdate()
        {
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            if (panel == null)
                return;

            if (Screen.width <= 0 || Screen.height <= 0)
                return;

            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.min;
            Vector2 anchorMax = safeArea.max;

            // Convert pixel coordinates into 0-1 anchor coordinates.
            anchorMin.x = applyLeft
                ? anchorMin.x / Screen.width
                : 0f;

            anchorMin.y = applyBottom
                ? anchorMin.y / Screen.height
                : 0f;

            anchorMax.x = applyRight
                ? anchorMax.x / Screen.width
                : 1f;

            anchorMax.y = applyTop
                ? anchorMax.y / Screen.height
                : 1f;

            // Reapply the safe area if another UI component,
            // layout system or animation has modified it.
            if (panel.anchorMin != anchorMin)
                panel.anchorMin = anchorMin;

            if (panel.anchorMax != anchorMax)
                panel.anchorMax = anchorMax;

            if (panel.offsetMin != Vector2.zero)
                panel.offsetMin = Vector2.zero;

            if (panel.offsetMax != Vector2.zero)
                panel.offsetMax = Vector2.zero;
        }
    }
}