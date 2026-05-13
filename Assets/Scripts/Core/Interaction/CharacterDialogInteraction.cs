using Atypiki.Core.Core.DialogSystem;
using UnityEngine;

namespace Atypiki.Core.Core.Interaction
{
    public class CharacterDialogInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] protected DialogBehaviour _dialogBehaviour;
        [SerializeField] protected DialogData _dialogGraph;

        private void StartDialog()
        {
            _dialogBehaviour.StartDialog(_dialogGraph);
        }

        public void Interact()
        {
            Debug.Log("Interact");
            StartDialog();
        }
    }
}