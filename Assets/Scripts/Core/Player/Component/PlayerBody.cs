using System;
using Atypiki.Core.Core.Interaction;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Atypiki.Core
{
    /*
     * This class is responsible for monitoring the body's states and let other classes know about it.
     */
    public class PlayerBody : PlayerComponent
    {
        [SerializeField] 
        public Rigidbody rb;

        [field: SerializeField]
        public Vector3 CurrentVelocity { get; private set; }
        public bool IsGrounded { get; private set; }
 
        public bool CanMove { get; private set; }

        public event Action<bool> OnChangedGrounded;
        public event Action OnInteract;
        public event Action<bool> OnInteractable;
 
        private IInteractable interactable;
        
        /*
         * Function that allow to apply movement on the character only when they're unlocked
         * input : newPosition : the position the character will move to
         * newRotation : the rotation the character will rotate too
         */
        public void ApplyVelocity(Vector3 newPosition, Quaternion newRotation)
        {
            //Check if character is locked
            if (CanMove)
            {
                rb.MovePosition(newPosition);
                rb.MoveRotation(newRotation);
            }
        }

        /*
         * Function that lock and unlock the character
         */
        public void SetCanMove(bool canMove)
        {
            CanMove = canMove;
        }
        
        /*
         * Function that handle ground state and
         * call event for other element to track the character ground state
         */
        public void CheckGround(bool isGrounded)
        {
            if (IsGrounded != isGrounded)
                OnChangedGrounded?.Invoke(IsGrounded);
            IsGrounded =  isGrounded;
        }
        
        /*
         * Check if we are in range for an interaction
         */
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter");
            IInteractable item = other.transform.GetComponent<IInteractable>();
            if (item is not null && interactable is null)
            {
                interactable = item;
                OnInteractable?.Invoke(true);
            }
        }

        /*
         * Check if we out of range of previous in range interaction
         */
        private void OnTriggerExit(Collider other)
        {
            IInteractable item = other.transform.GetComponent<IInteractable>();
            if (item == interactable)
            {
                interactable = null;
                OnInteractable?.Invoke(false);
            }
        }
        
        /*
         * Check if we are in range for an interaction
         */
        private void OnTriggerStay(Collider other)
        {
            IInteractable item = other.transform.GetComponent<IInteractable>();
            if (item == interactable)
            {
                interactable = item;
                OnInteractable?.Invoke(true);
            }
        }
        
        /*
         * If we are in range of interaction and player interact, then interaction is performed
         */
        public void Interact(InputAction.CallbackContext context)
        {
            if (interactable is not null && context.performed)
            {
                Debug.Log("we interact");
                interactable.Interact();
                OnInteract?.Invoke();
                interactable = null;
                OnInteractable?.Invoke(false);
            }
        }
        
    }
}