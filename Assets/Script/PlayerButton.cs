using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPSLS
{
    public class PlayerButton : MonoBehaviour
    {
        [SerializeField] private State playerState;

        private Button thisButton;

        private void Awake()
        {
            thisButton = GetComponent<Button>();
            if (thisButton == null) Debug.Log("Button is null on GameObject: " + gameObject.name);
        }

        // Start is called before the first frame update
        void Start()
        {
            thisButton.onClick.AddListener(()=> { GameManager.Instance.SetPlayerMove(playerState); });
        }
    }
}
