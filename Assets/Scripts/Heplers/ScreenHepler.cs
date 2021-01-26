using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Heplers
{
    public static class ScreenHepler
    {
        //left, right, top, bottom input offset (percentage from screen size)
        private const int INPUT_OFFSET_L = 0;
        private const int INPUT_OFFSET_R = 0;
        private const int INPUT_OFFSET_T = 60;
        private const int INPUT_OFFSET_B = 0;

        private static Rect worldLimits;
        private static bool wlSet = false;

        private static Rect inputLimits;
        private static bool ilSet = false;

        private static float deadlineY = 0f;

        public static Rect WorldLimits 
        { 
            get 
            {
                if (!wlSet)
                    SetUpWorldLimits();
                return worldLimits; 
            } 
        }
        
        public static Rect InputLimits 
        { 
            get 
            {
                if (!ilSet)
                    SetUpInputLimits();
                return inputLimits; 
            } 
        }

        public static float DeadlineY { get => deadlineY; }

        private static void SetUpWorldLimits()
        {
            Vector2 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            worldLimits = new Rect(-topRightCorner.x, -topRightCorner.y, topRightCorner.x, topRightCorner.y);
            wlSet = true;
        }
        
        private static void SetUpInputLimits()
        {
            inputLimits = new Rect(
                Screen.width * (INPUT_OFFSET_L / 100f), 
                Screen.height * (INPUT_OFFSET_B / 100f), 
                Screen.width - (Screen.width * INPUT_OFFSET_R / 100f), 
                Screen.height - (Screen.height * INPUT_OFFSET_T / 100f));

            deadlineY = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, inputLimits.height + 50f)).y;

            ilSet = true;
        }
    }
}