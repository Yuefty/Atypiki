using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Atypiki.Core.Core.DialogSystem.UI
{
    public class ChoiceUI : MonoBehaviour
    {
        [SerializeField] private Button _answerButtonPrefab;
        [SerializeField] private Transform _parentTransform;

        private readonly List<Button> _buttons = new();
        private readonly List<TextMeshProUGUI> _buttonTexts = new();
        private GameObject answerPanel;
        
        
        public void InitButtons(int maxAmountOfAnswerButtons)
        {
            DeleteAllExistingButtons();
            
            for (int i = 0; i < maxAmountOfAnswerButtons; i++)
            {
                Button answerButton = Instantiate(_answerButtonPrefab, _parentTransform);

                _buttons.Add(answerButton);
                _buttonTexts.Add(answerButton.GetComponentInChildren<TextMeshProUGUI>());
            }
        }

        public void SetButton(int index, string text,  UnityAction action)
        {
            GetButtonTextByIndex(index).SetText(text);
            AddButtonOnClickListener(index, action);
        }
    
        public Button GetButtonByIndex(int index) => _buttons[index];
    
        public TextMeshProUGUI GetButtonTextByIndex(int index) => _buttonTexts[index];

       /*
        * Add listener to a specific button
        */
        public void AddButtonOnClickListener(int index, UnityAction action) => _buttons[index].onClick.AddListener(action);

        
        public void DisableAllButtons()
        {
            foreach (Button button in _buttons)
                button.gameObject.SetActive(false);
        }

    
        private void DeleteAllExistingButtons()
        {
            if (_buttons.Count > 0)
            {
                foreach (var button in _buttons) 
                    Destroy(button.gameObject);
                            
                _buttons.Clear();
                _buttonTexts.Clear();
            }
        }
    }
}