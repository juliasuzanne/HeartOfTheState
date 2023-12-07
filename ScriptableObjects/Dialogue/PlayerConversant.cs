using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{


    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;
        // Start is called before the first frame update
        void Start()
        {
        }



        public string GetText()
        {

            return currentDialogue.GetRootNode().GetSpeech();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
