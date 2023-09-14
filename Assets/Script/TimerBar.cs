using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RPSLS
{
    public enum FillMode
    {
        Addition,
        Subtraction
    }

    public class TimerBar : MonoBehaviour
    {
        public Image timerBarImage;
        [SerializeField] private bool isFilling;
        private float timerDuration;
        private float currentTime;
        private int multiplier;

        Coroutine updateTimerCoroutine = null;

        public void StartTimer(float duration, FillMode fillMode)
        {
            Debug.Log("Starting timer......");
            if (fillMode == FillMode.Addition)
                isFilling = true;
            else
                isFilling = false;
            timerDuration = duration;
            currentTime =  isFilling? 0: duration;
            timerBarImage.fillAmount = isFilling ? 0 : 1f;
            multiplier = isFilling ? 1 : -1;
            if (updateTimerCoroutine != null)
            {
                StopCoroutine(updateTimerCoroutine);
                updateTimerCoroutine = null;
            }
            updateTimerCoroutine = StartCoroutine(UpdateTimer());
        }

        public void ResetTimer()
        {
            timerBarImage.fillAmount = isFilling ? 0 : 1f;
        }

        public float GetFillAmount()
        {
            return timerBarImage.fillAmount;
        }

        IEnumerator UpdateTimer()
        {
            //bool condition = isFilling ? currentTime < timerDuration :currentTime > 0;
            while ((isFilling && currentTime < timerDuration) || (!isFilling && currentTime > 0))
            {
                currentTime += Time.fixedDeltaTime * multiplier;

                // Calculate fill amount based on current time and duration
                float fillAmount = currentTime / timerDuration;
                timerBarImage.fillAmount = fillAmount;
                if (fillAmount < insertions) alertAction?.Invoke(fillAmount);
                yield return new WaitForFixedUpdate();
            }
            Debug.Log("Timer elapsed");
            updateTimerCoroutine = null;
            EventServices.Instance.InvokeOnTimerElapsed();
        }

        float insertions;
        Action<float> alertAction;
        public void SetAlertAt(float alert, Action<float> action)
        {
            insertions = alert;
            alertAction = action;
        }
    }
}
