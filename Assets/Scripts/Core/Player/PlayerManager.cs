using System;
using Atypiki.Core.Core.DialogSystem;
using Atypiki.Core.Player;
using UnityEngine;

namespace Atypiki.Core 
{
    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerMovement Movement { get; private set; }
        [field: SerializeField]
        public PlayerBody Body { get; private set; }
        [field: SerializeField]
        public PlayerAnimator PlayerAnimator { get; private set; }
        
        [field: SerializeField]
        public DialogBehaviour DialogBehaviour { get; private set; }

        private void Start()
        {
            Body.SetCanMove(true);
        }

        private void OnEnable()
        {
            DialogBehaviour.OnDialogStarted += LockPlayerMovement;
            DialogBehaviour.OnDialogFinished += UnLockPlayerMovement;
        }

        private void OnDisable()
        {
            DialogBehaviour.OnDialogStarted -= LockPlayerMovement;
            DialogBehaviour.OnDialogFinished -= UnLockPlayerMovement;
        }

        public void UnLockPlayerMovement()
        {
            Body.SetCanMove(true);
        }
        
        public void LockPlayerMovement()
        {
            Body.SetCanMove(false);
        }
    }
}