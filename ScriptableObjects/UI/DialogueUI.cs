using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField]
        TextMeshProUGUI AIText;
        [SerializeField]
        Button nextButton;
        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(Next);
            UpdateUI();
        }

        void Next()
        {
            playerConversant.Next();
            UpdateUI();

        }
        void UpdateUI()
        {
            AIText.text = playerConversant.GetText();

        }

    }
}