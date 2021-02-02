using UnityEngine;
using System.Collections;

namespace Assets.Scripts.UI.UIAnimations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UITwinOpacity : UITwin
    {

        private CanvasGroup cg;

        private void Awake()
        {
            InitComponent();
        }

        private void InitComponent()
        {
            cg = GetComponent<CanvasGroup>();
            cg.alpha = 0f;
        }

        protected override void Animate(float curvePos)
        {
            if (!cg)
                InitComponent();

            cg.alpha = curvePos;
        }

        protected override void ResetToDefault()
        {
            cg.alpha = 0f;
        }
    }
}