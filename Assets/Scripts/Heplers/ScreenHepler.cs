using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Heplers
{
    public static class ScreenHepler
    {
        private static Rect worldLimits;
        private static bool wlSet = false;

        public static Rect WorldLimits 
        { 
            get 
            {
                if (!wlSet)
                    SetUpWorldLimits();
                return worldLimits; 
            } 
        }

        private static void SetUpWorldLimits()
        {
            Vector2 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            worldLimits = new Rect(-topRightCorner.x, -topRightCorner.y, topRightCorner.x, topRightCorner.y);
            wlSet = true;
        }
    }
}