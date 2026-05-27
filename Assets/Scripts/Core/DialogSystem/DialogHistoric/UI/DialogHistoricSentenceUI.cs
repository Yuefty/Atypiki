using TMPro;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.DialogHistoric.UI
{
    public class DialogHistoricSentenceUI : MonoBehaviour
    {
        [Header("Text Slot")]
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogText;
        
        public void SetText(string charName, string text)
        {
            characterName.text = charName;
            dialogText.text = text;
        }
    }
}