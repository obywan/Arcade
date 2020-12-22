using UnityEngine;

namespace Assets.Scripts.Heplers
{
    public class ProjectileLaunchHepler
    {
        private Vector2[] trail;
        private int valsCount;
        private int arraySize;

        public ProjectileLaunchHepler(int maxSamples)
        {
            arraySize = maxSamples;
            trail = new Vector2[arraySize];
            Reset();
        }

        public void AddToPath(Vector2 p)
        {
            for (int i = arraySize - 1; i > 0; i--)
            {
                trail[i] = trail[i - 1];
            }
            trail[0] = p;
            valsCount = Mathf.Clamp(valsCount + 1, 0, arraySize - 1);
        }

        public float GetAvarage()
        {
            if (valsCount == 0)
                return 0f;

            float res = 0f;
            for (int i = 1; i <= valsCount; i++)
            {
                res += Vector2.Distance(trail[i - 1], trail[i]);
            }
            return res / (valsCount - 1);
        }

        public Vector2 GetDirection()
        {
            if (valsCount < 2)
                return Vector2.zero;

            return trail[0] - trail[1];
        }

        public void ClearPath()
        {
            Reset();
        }

        private void Reset()
        {
            for (int i = 0; i < arraySize; i++)
            {
                trail[i] = Vector2.zero;
            }
            valsCount = 0;
        }
    }
}