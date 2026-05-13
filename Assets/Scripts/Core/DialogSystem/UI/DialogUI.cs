using System;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.UI
{
    public class DialogUI : MonoBehaviour
    {
        private Camera DialogCamera;
        
        [Header("Main component")]
        [field : SerializeField] private DialogBehaviour _dialogBehaviour;
        
        [Header("Panels")]
        [field : SerializeField] private GameObject dialogUI;
        [field : SerializeField] private SentenceUI sentenceUI;
        [field : SerializeField] private ChoiceUI choiceUI;


        private void OnEnable()
        {
            _dialogBehaviour.OnDialogFinished += HideDialog;
            _dialogBehaviour.OnDialogStarted += ShowDialog;

            _dialogBehaviour.OnSentenceNode += sentenceUI.OnSentenceNode;
            _dialogBehaviour.OnChangeMaxVisibility += sentenceUI.ChangeMaxVisibility;
            _dialogBehaviour.OnSentenceSkipped += sentenceUI.ShowFullDialogText;
            _dialogBehaviour.OnChoiceNode += sentenceUI.OnChoiceNode;
            
            _dialogBehaviour.OnChoiceNode += choiceUI.InitButtons;
            _dialogBehaviour.OnAddChoice += choiceUI.SetButton;
            _dialogBehaviour.AddChoiceAction += choiceUI.AddButtonOnClickListener;
        }

        private void OnDisable()
        {
            _dialogBehaviour.OnDialogFinished -= HideDialog;
            _dialogBehaviour.OnDialogStarted -= ShowDialog;

            _dialogBehaviour.OnSentenceNode -= sentenceUI.OnSentenceNode;
            _dialogBehaviour.OnChangeMaxVisibility -= sentenceUI.ChangeMaxVisibility;
            _dialogBehaviour.OnSentenceSkipped -= sentenceUI.ShowFullDialogText;
            _dialogBehaviour.OnChoiceNode -= sentenceUI.OnChoiceNode;
            
            _dialogBehaviour.OnChoiceNode -= choiceUI.InitButtons;
            _dialogBehaviour.OnAddChoice -= choiceUI.SetButton;
            _dialogBehaviour.AddChoiceAction -= choiceUI.AddButtonOnClickListener;
        }

        private void HideDialog()
        {
            dialogUI.SetActive(false);
        }
        
        private void ShowDialog()
        {
            dialogUI.SetActive(true);
        }
    }
}