using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RPSLS
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text CPUStateText;
        [SerializeField] private Image roundPanel;
        [SerializeField] private float roundPanelDelay;
        [SerializeField] private float turnTimerDelay;
        [SerializeField] private TimerBar timerBar;
        [SerializeField] private TMP_Text currentScore;

        private TMP_Text roundText;
        private Action nextRoundCallBack = null;

        // Start is called before the first frame update
        void Awake()
        {
            roundText = roundPanel.transform.GetChild(0).GetComponent<TMP_Text>();
            if(roundText == null) Debug.LogError("Round text not set");
            EventServices.Instance.OnTimerElapsed += OnLose;
            roundPanel.gameObject.SetActive(false);
        }

        public void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }


        public void SetCPUState(string stateName)
        {
            CPUStateText.text = stateName;
        }

        public void OnGameStart(int round, Action callback)
        {
            nextRoundCallBack = callback;
            OnWin(round, 0);
        }

        public void OnWin(int round, float currentScore)
        {
            try
            {
                StartCoroutine(ShowRound(round, roundPanelDelay, () => {
                    timerBar.StartTimer(turnTimerDelay, FillMode.Subtraction);
                    nextRoundCallBack?.Invoke();
                }));
            }
            catch (Exception ex)
            {
                Debug.Log($"Error in round display {ex.Message}");
                nextRoundCallBack?.Invoke();
            }

            timerBar.ResetTimer();
            timerBar.gameObject.SetActive(false);
            this.currentScore.text = currentScore.ToString();
        }

        public void OnLose()
        {
            ReloadScene();
        }

        IEnumerator ShowRound(int round, float delay, Action callback)
        {
            if(roundPanel != null)
            {
                roundPanel.gameObject.SetActive(true);
                roundText.text = $"Round {round.ToString()}";
                yield return new WaitForSeconds(delay);
                roundPanel.gameObject.SetActive(false);
                timerBar.gameObject.SetActive(true);
                callback?.Invoke();
            }
            else
            {
                callback?.Invoke();
            }
        }
    }
}
