using UnityEngine;

namespace Atypiki.Core.Core.Player.Component
{
    public class PlayerAnimator : PlayerComponent
    {
        [SerializeField] 
        private Animator animator;

        
        private void OnEnable()
        {
            Manager.Body.OnInteract += OnInteraction;
            Manager.Body.OnChangedGrounded += OnGroundedChanged;
        }

        private void OnDisable()
        {
            Manager.Body.OnInteract -= OnInteraction;
            Manager.Body.OnChangedGrounded -= OnGroundedChanged;
        }
    
        private void Update()
        {
            animator.SetFloat("Velocity", Manager.Body.rb.linearVelocity.magnitude);
        }

        private void OnGroundedChanged(bool grounded)
        {
            animator.SetBool("Grounded",grounded);
        }
        

        private void OnInteraction()
        {
            animator.SetTrigger("Interaction");
        }

        public void OnJump()
        {
            Debug.Log("OnJump");
            animator.SetTrigger("Jump");
        }
    }
}