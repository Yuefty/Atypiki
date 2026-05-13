using System.Collections.Generic;
using Atypiki.Core.Core.DialogSystem.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Atypiki.Core.Core.DialogSystem.UI
{
    public class SentenceUI : MonoBehaviour
    {
        [Header("Text Slot")]
        [SerializeField] private GameObject textSlot;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogText;
        
        private string _currentFullText;
        

        /*
         * Clean the text in the UI
         */
        public void ResetDialogText()
        {
            dialogText.maxVisibleCharacters = 0;
            _currentFullText = string.Empty;
        }

        /*
         * It shows all the text, by setting the max visible characters to dialog text length
         */
        public void ShowFullDialogText()
        {
            dialogText.maxVisibleCharacters = _currentFullText.Length;
        }

        /*
         * Slowly increase max visible characters for typewriting effect
         */
        public void ChangeMaxVisibility(int maxVisibility)
        {
            dialogText.maxVisibleCharacters = maxVisibility;
        }

        /*
         * Whenever a new sentence is process, show the UI and change the text
         */
        public void OnSentenceNode(CharacterData characterData, string text)
        {
            textSlot.SetActive(true);
            ChangeMaxVisibility(0);
            ChangeText(characterData, text);
        }

        /*
         * Hide the dialog box when choosing an answer
         */
        public void OnChoiceNode(int i)
        {
            textSlot.SetActive(false);
        }

        /*
         * Set up the UI with the new text and the name of the character talking
         */
        private void ChangeText(CharacterData characterData, string text)
        {
            characterName.text = characterData.displayName;
            dialogText.text = text;
            _currentFullText = text;
        }
    }
}