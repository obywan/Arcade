using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Heplers
{
    public static class EnemyColorHelper
    {
        private const int minH = 50;
        private const int maxH = 359;

        public static Color GetColor(int h)
        {
            //Debug.Log(Mathf.Lerp(minH, maxH, h / 24f) / maxH - minH);
            //Debug.Log(h / 24f);
            return Color.HSVToRGB(Mathf.Lerp(minH, maxH, h / 24f) / 360f, 0.6f, 1f);
        }

    }
}