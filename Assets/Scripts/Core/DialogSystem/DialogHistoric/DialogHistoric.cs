using System.Collections.Generic;

namespace Atypiki.Core.Core.DialogSystem.DialogHistoric
{
    public class DialogHistoric
    {
        public string DialogName { get; private set; }
        public List<HistoricSentence> HistoryList = new();
        
        public DialogHistoric(string dialogName)
        {
            DialogName = dialogName;
        }

        public void AddSentence(string charName, string text)
        {
            HistoryList.Add(new HistoricSentence(charName, text));
        }
            
    }
}