using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atypiki.Core.Core.DialogSystem.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Atypiki.Core.Core.DialogSystem
{
    public class DialogBehaviour : MonoBehaviour
    {
        [field : SerializeField] public float typingSpeed { get; private set; }
        
        private bool _isDialogStarted;
        private DialogData currentDialog;
        private NodeData currentNode;
        
        private bool _isCurrentSentenceTyping; // is the text currently being writen 
        private Coroutine TextWritingCoroutine;
        
        public event Action OnDialogStarted;
        public event Action OnDialogFinished;
        
        
        public event Action<CharacterData, string> OnSentenceNode;
        public event Action<int> OnChangeMaxVisibility;
        public event Action OnSentenceSkipped;

        public event Action<int> OnChoiceNode;
        public event Action<int, string,  UnityAction> OnAddChoice; 
        public event Action<int, UnityAction> AddChoiceAction; 


        public void StartDialog(DialogData dialogData)
        {
            _isDialogStarted = true;

            if (dialogData.NodesList == null)
            {
                Debug.LogWarning("Dialog Graph's node list is empty");
                return;
            }

            OnDialogStarted?.Invoke();
            currentDialog = dialogData;
            currentNode = dialogData.GetFirstNode();
            
            
            if (currentNode is not null)
            {
                currentNode.ProcessNode(this);
            }
            else
            {
                EndDialog();
            }
        }
        

        public void ProcessNextNode()
        {
            NodeData nextNode = currentNode.GetNextNode();

            if (nextNode is null)
            {
                EndDialog();
            }
            else
            {
                currentNode = nextNode;
                currentNode.ProcessNode(this);
            }
        }

        public void EndDialog()
        {
            OnDialogFinished?.Invoke();
            _isDialogStarted = false;
            currentDialog = null;
        }
        
        public void OnSentenceSkip(InputAction.CallbackContext context)
        {
            if (context.performed && _isDialogStarted)
            {
                if (_isCurrentSentenceTyping)
                {
                    OnSentenceSkipped?.Invoke();
                    if (TextWritingCoroutine != null) {
                        StopCoroutine(TextWritingCoroutine);
                        TextWritingCoroutine = null;
                    }
                    _isCurrentSentenceTyping = false;
                }
                else
                {
                    if (currentNode.GetType() == typeof(SentenceNodeData))
                    {
                        ProcessNextNode();
                    }
                }
            }
        }

        public void ShowText(SentenceStruct sentence)
        {
            OnSentenceNode?.Invoke(sentence.CharacterData, sentence.Text);
            _isCurrentSentenceTyping = true;
            TextWritingCoroutine = StartCoroutine(WriteText(sentence));
        }
        
        IEnumerator WriteText(SentenceStruct sentence)
        {
            int totalChars = sentence.Text.Length;

            for (int i = 0; i <= totalChars; i++)
            {
                OnChangeMaxVisibility?.Invoke(i);
                yield return new WaitForSeconds(typingSpeed);
            }
            TextWritingCoroutine =  null;
            _isCurrentSentenceTyping = false;
        }
        
        public void InitChoice(int maxChoices)
        {
            OnChoiceNode?.Invoke(maxChoices);
        }

        public void AddChoice(int index, string choiceText, UnityAction action)
        {
            OnAddChoice?.Invoke(index, choiceText, action);
            AddChoiceAction?.Invoke(index, ProcessNextNode);
        }

        
    }
}