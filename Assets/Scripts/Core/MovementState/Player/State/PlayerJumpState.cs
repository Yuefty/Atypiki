using UnityEngine;
using UnityEngine.InputSystem;

namespace Atypiki.Core.Core.MovementState.State
{
    [CreateAssetMenu(fileName = "JumpState", menuName = "Atypiki/Player/Jump", order = 0)]
    public class PlayerJumpState : PlayerAirborneState
    {
        [ field : SerializeField, Range(0, 100) ]
        private float jumpForce;

        [ field : SerializeField,Range(0, 10) ]
        private int coyoteTime;
        [ field : SerializeField, Range(1, 5) ] 
        private int jumpCount;
        
        [ field : SerializeField ]
        private AnimationCurve jumpCurve;
        [ field : SerializeField ] 
        private AnimationCurve jumpForcePerCountModifier;
        [ field : SerializeField ] 
        private float jumpDuration;
        
        private InputAction jumpInput;
        private int currentJumpBuffer;
        private int currentJumpCount;
        private float currentJumpTime;
        
        private bool IsJumping => currentJumpTime >= 0 && currentJumpTime < jumpDuration;
        private bool wantsToJump;
        
        

        public override void Initialize(PlayerMovement playerMovement)
        {
            base.Initialize(playerMovement);
            //retrieve the input action linked to this movement
            jumpInput = playerMovement.PlayerInput.actions.FindActionMap("Player").FindAction("Jump");
            jumpInput.performed += OnJumpInputPerformed;
        }

        public override void Dispose(PlayerMovement playerMovement)
        {
            base.Dispose(playerMovement);
            jumpInput = null;
        }
        
        public override void OnEnter(PlayerMovement playerMovement)
        {
            base.OnEnter(playerMovement);
            currentJumpTime = 0;
            currentJumpCount ++;
            playerMovement.Manager.PlayerAnimator.OnJump();
        }

        public override void OnExit(PlayerMovement playerMovement)
        {
            base.OnExit(playerMovement);
            //Debug.Log("Ending jump");
            currentJumpTime = -1;
            currentJumpBuffer = 0;
        }

        /*
         * Handle multiple jump
         */
        public override void PreUpdate(PlayerMovement playerMovement)
        {
            base.PreUpdate(playerMovement);
            
            if (currentJumpBuffer >= 0)
                currentJumpBuffer--;

            if (playerMovement.IsGrounded)
                currentJumpCount = 0;
        }

        public override int GetStatePriority(PlayerMovement playerMovement)
        {
            if (IsJumping)
                return 10;

            if (currentJumpBuffer <= 0)
                return -1;
            
            bool canJump = playerMovement.IsGrounded || currentJumpCount < jumpCount;
            
            return canJump ? 10 : -1;
        }

        public override Vector3 GetVelocity(PlayerMovement playerMovement, float deltaTime)
        {
            Vector3 velocity = base.GetVelocity(playerMovement, deltaTime);
            float normalizedTime = currentJumpTime / jumpDuration;
            float modifier = jumpForcePerCountModifier.Evaluate(currentJumpCount / (float)jumpCount);
            float currentJumpForce = jumpForce * modifier * jumpCurve.Evaluate(normalizedTime);

            currentJumpTime += Time.deltaTime;

            return new Vector3()
            {
                x = velocity.x,
                y = currentJumpForce,
                z = velocity.z
            };
        }

        private void OnJumpInputPerformed(InputAction.CallbackContext obj)
        {
            currentJumpBuffer = coyoteTime;
            if (IsJumping && jumpDuration > 0 && currentJumpCount < jumpCount)
            {
                currentJumpCount++;
                currentJumpTime = 0;
            }
        }
        
    }
}