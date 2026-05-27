using System.Collections.Generic;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.DialogHistoric.UI
{
    public class DialogHistoricUI : MonoBehaviour
    {
        [field: SerializeField] public DialogHistoricManager DialogHistoricManager { get; private set; }
        [SerializeField] private DialogHistoricSentenceUI _prefab;
        [SerializeField] private Transform _parentTransform;
        
        private readonly List<DialogHistoricSentenceUI> _sentenceUis = new();

        private int currentIndex = -1;


        public void OnDialogHistoricSelected(int index)
        {
            if(currentIndex == index)
                return;
            
            currentIndex = index;
            LoadDialogHistoric(index);
        }
        
        
        public void LoadDialogHistoric(int index)
        {
            UnloadDialogHistoric();
            
            DialogHistoric dialog = DialogHistoricManager.GetHistoricDialog(index);

            foreach (var sentence in dialog.HistoryList)
            {
                DialogHistoricSentenceUI dialogHistoricSentenceUI = Instantiate(_prefab, _parentTransform);
                dialogHistoricSentenceUI.SetText(sentence.charName, sentence.dialogText);
                _sentenceUis.Add(dialogHistoricSentenceUI);
            }
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