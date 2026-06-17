using System;
using System.Collections.Generic;
using Atypiki.Core.Core.DialogSystem.Data;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.DialogHistoric
{
    public class DialogHistoricManager : MonoBehaviour
    {
        [field: SerializeField] public DialogBehaviour DialogBehaviour { get; set; }
        [field: SerializeField] public CharacterData PlayerData { get; set; }
        
        public List<DialogHistoric> dialogHistorics { get; private set;}
        private DialogHistoric currentDialog;
        
        public event Action<DialogHistoric> OnAddDialog;

        private void Start()
        {
            dialogHistorics  = new List<DialogHistoric>();
        }

        private void OnEnable()
        {
            DialogBehaviour.OnDialogStarted += AddDialogHistoric;
            DialogBehaviour.OnSentenceNode += OnSentenceNode;
            DialogBehaviour.OnChoiceMade += OnChoiceMade;
        }

        private void OnDisable()
        {
            DialogBehaviour.OnDialogStarted -= AddDialogHistoric;
            DialogBehaviour.OnSentenceNode -= OnSentenceNode;
            DialogBehaviour.OnChoiceMade -= OnChoiceMade;
        }

        public void AddDialogHistoric()
        {
            currentDialog = new DialogHistoric(DialogBehaviour.CurrentDialog.DialogName);
            dialogHistorics.Add(currentDialog);
            OnAddDialog?.Invoke(currentDialog);
        }
        

        private void OnChoiceMade(string text)
        {
            AddSentence(PlayerData.displayName, text);
        }

        private void OnSentenceNode(CharacterData characterData, string sentence)
        {
            AddSentence(characterData.displayName, sentence);
        }
        

        private void AddSentence(string charName,string sentence)
        {
            currentDialog.AddSentence(charName,sentence);
        }

        public DialogHistoric GetHistoricDialog(int index)
        {
            if(index < 0 || index > dialogHistorics.Count)
                return null;
            return dialogHistorics[index];
        }
    }
}