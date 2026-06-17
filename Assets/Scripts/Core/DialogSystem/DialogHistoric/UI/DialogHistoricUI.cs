using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Atypiki.Core.Core.DialogSystem.DialogHistoric.UI
{
    public class DialogHistoricUI : MonoBehaviour
    {
        [field: SerializeField] public DialogHistoricManager DialogHistoricManager { get; private set; }
        
        [SerializeField] private Button _dialogSelectionPrefab;
        [SerializeField] private Transform _dialogSelectionparentTransform;
        
        private readonly List<Button> _dialogHistoricUIs = new();
        
        [SerializeField] private DialogHistoricSentenceUI _dialogSentencePrefab;
        [SerializeField] private Transform _dialogSentenceparentTransform;
        
        private readonly List<DialogHistoricSentenceUI> _sentenceUis = new();

        private int currentIndex = -1;


        private void OnEnable()
        {
            DialogHistoricManager.OnAddDialog += AddDialogToHistoric;
        }

        private void OnDisable()
        {
            DialogHistoricManager.OnAddDialog -= AddDialogToHistoric;
        }

        public void OnDialogHistoricSelected(int index)
        {
            if(currentIndex == index)
                return;
            
            currentIndex = index;
            LoadDialogHistoric(index);
        }

        public void AddDialogToHistoric(DialogHistoric dialog)
        {
            Button dialogHistoricUI = Instantiate(_dialogSelectionPrefab, _dialogSelectionparentTransform);
            dialogHistoricUI.GetComponentInChildren<TextMeshProUGUI>().text = dialog.DialogName;
            _dialogHistoricUIs.Add(dialogHistoricUI);
            
            int index =  _dialogHistoricUIs.IndexOf(dialogHistoricUI);
            dialogHistoricUI.onClick.AddListener(() => OnDialogHistoricSelected(index));
        }
        
        
        public void LoadDialogHistoric(int index)
        {
            UnloadDialogHistoric();
            
            DialogHistoric dialog = DialogHistoricManager.GetHistoricDialog(index);

            foreach (var sentence in dialog.HistoryList)
            {
                DialogHistoricSentenceUI dialogHistoricSentenceUI = Instantiate(_dialogSentencePrefab, _dialogSentenceparentTransform);
                dialogHistoricSentenceUI.SetText(sentence.charName, sentence.dialogText);
                _sentenceUis.Add(dialogHistoricSentenceUI);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(
                _dialogSentenceparentTransform.GetComponent<RectTransform>());
        }
        
        private void UnloadDialogHistoric()
        {
            if (_sentenceUis.Count > 0)
            {
                foreach (var sentenceUi in _sentenceUis) 
                    Destroy(sentenceUi.gameObject);
                            
                _sentenceUis.Clear();
            }
        }
    }
}