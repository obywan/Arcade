using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.UIAnimations
{
    [RequireComponent(typeof(RectTransform))]
    public class UITwin : MonoBehaviour
    {
        public enum MOTION_TYPE { ONCE, LOOP, BOUNCE }

        public AnimationCurve curve;
        public float duration = 1f;
        public float startDelay = 0f;
        public MOTION_TYPE motionType;
        public bool playOnEnable = false;
        public bool resetOnDisable = false;

        private bool animationNeeded = false;
        private float timer = 0f;
        private int direction = 1;

        protected void OnEnable()
        {
            if (playOnEnable)
                PlayForward();
        }

        protected void OnDisable()
        {
            if (resetOnDisable)
                Stop();
        }

        protected void Update()
        {
            if (!animationNeeded)
                return;
            DoTransition();
        }

        protected virtual void Animate(float curvePos)
        { //do something with curvePos value in child classes
        }

        protected virtual void ResetToDefault()
        {
        }

        public void PlayForward()
        {
            timer = 0f;
            direction = 1;
            if (startDelay > float.Epsilon)
                Invoke("EnableAnimation", startDelay);
            else
                animationNeeded = true;
        }

        public void PlayBackward()
        {
            timer = duration;
            direction = -1;
            animationNeeded = true;
        }
        public void Stop()
        {
            animationNeeded = false;
            ResetToDefault();
        }

        private void EnableAnimation()
        {
            animationNeeded = true;
        }

        private void DoTransition()
        {
            switch (motionType)
            {
                case MOTION_TYPE.ONCE:
                    if (timer >= duration)
                    {
                        animationNeeded = false;
                        timer = 0f;
                        break;
                    }
                    else
                    {
                        ProceedWithAnimation();
                    }
                    break;

                case MOTION_TYPE.LOOP:
                    if (timer >= duration)
                    {
                        timer = 0f;
                    }
                    ProceedWithAnimation();
                    break;

                case MOTION_TYPE.BOUNCE:
                    if (timer >= duration && direction == 1)
                    {
                        direction = -1;
                    }
                    else if (timer <= 0 && direction == -1)
                    {
                        direction = 1;
                    }
                    ProceedWithAnimation();
                    break;
            }

        }

        private void ProceedWithAnimation()
        {
            Animate(curve.Evaluate(timer / duration));
            timer += Time.deltaTime * direction;
        }


    }
}
