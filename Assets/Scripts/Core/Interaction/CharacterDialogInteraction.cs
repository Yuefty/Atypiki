using System;
using Atypiki.Core.Core.DialogSystem;
using Atypiki.Core.Core.DialogSystem.Data;
using Unity.Cinemachine;
using UnityEngine;

namespace Atypiki.Core.Core.Interaction
{
    public class CharacterDialogInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] protected DialogBehaviour _dialogBehaviour;
        [SerializeField] protected DialogData _dialogGraph;
        [SerializeField] protected CinemachineCamera _camera;
        
        [SerializeField] protected CharacterData _characterData;

        private void OnEnable()
        {
            _dialogBehaviour.OnDialogFinished += OnDialogEnd;
        }

        private void OnDisable()
        {
            _dialogBehaviour.OnDialogFinished -= OnDialogEnd;
        }

        private void StartDialog()
        {
            _dialogBehaviour.StartDialog(_dialogGraph, _characterData);
        }

        public void Interact()
        {
            ChangeCameraPriority(20);
            StartDialog();
        }

        public void ChangeCameraPriority(int priority)
        {
            _camera.Priority = priority;
        }

        public void OnDialogEnd()
        {
            ChangeCameraPriority(0);
        }
    }
}