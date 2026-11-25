using UnityEngine;

/* This script handles the safe area for game ui, script taken directly
   from week 3 powerpoint slides. */

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