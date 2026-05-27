using System;

namespace Atypiki.Core.Core.DialogSystem.DialogHistoric
{
    public struct HistoricSentence
    {
        public string charName;
        public string dialogText;

        public HistoricSentence(string charName, string text)
        {
            this.charName = charName;
            this.dialogText = text;
        }
    }
}