using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Atypiki.Core.Core.DialogSystem
{
    public class DialogBehavior : MonoBehaviour
    {
        [SerializeField] private float _dialogCharDelay;
        [SerializeField] private List<KeyCode> _nextSentenceKeyCodes;
        [SerializeField] private bool _isCanSkippingText = true;
        
        private DialogData currentDialog;
        private Node currentNode;
        
        
        [SerializeField] public UnityEvent OnDialogStarted;
        [SerializeField] public UnityEvent OnDialogFinished;
        
        private int _maxAmountOfAnswerButtons;

        private bool _isDialogStarted;
        private bool _isCurrentSentenceSkipped;
        private bool _isCurrentSentenceTyping;
        private bool _isCurrentSentenceNext;
    }
}