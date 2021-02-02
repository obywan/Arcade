using UnityEngine;
using System.Collections;

namespace Assets.Scripts.UI.UIAnimations
{

    public class UITwinScale : UITwin
    {

        private RectTransform rt;
        private Vector3 originalScale;

        private void Awake()
        {
            InitComponent();
        }

        private void InitComponent()
        {
            rt = GetComponent<RectTransform>();
            originalScale = rt.localScale;
        }

        protected override void Animate(float curvePos)
        {
            if (!rt)
                InitComponent();

            rt.localScale = curvePos * originalScale;
        }

        protected override void ResetToDefault()
        {
            rt.localScale = originalScale;
        }
    }
}