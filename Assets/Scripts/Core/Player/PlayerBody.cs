using System;
using UnityEngine;

namespace Atypiki.Core
{
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
 
        //private IInteractable interactable;
        
        public void ApplyVelocity(Vector3 newPosition, Quaternion currentRotation)
        {
            if (CanMove)
            {
                rb.MovePosition(newPosition);
                rb.MoveRotation(currentRotation);
            }
        }

        public void SetCanMove(bool canMove)
        {
            CanMove = canMove;
        }
        
        public void CheckGround(bool isGrounded)
        {
            if (IsGrounded != isGrounded)
                OnChangedGrounded?.Invoke(IsGrounded);
            IsGrounded =  isGrounded;
        }
        
    }
}