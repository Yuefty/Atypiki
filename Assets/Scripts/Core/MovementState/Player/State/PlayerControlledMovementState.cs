using UnityEngine;
using UnityEngine.InputSystem;

namespace Atypiki.Core.Core.MovementState.State
{
    public abstract class PlayerControlledMovementState : PlayerMovementState
    {
        [field : SerializeField] private float maxSpeed;
        [field : SerializeField] protected float acceleration;
        [field : SerializeField] protected float deceleration;
        [field : SerializeField] protected float stopForce;

        private InputAction moveAction;
        
        /*
         * Function that initialize variables
         * Called when this class is added to a movement controller
         */
        public override void Initialize(PlayerMovement playerMovement)
        {
            //retrieve the input action linked to this movement
            moveAction = playerMovement.PlayerInput.actions.FindActionMap("Player").FindAction("Move");
        }

        /*
         * Function that unitialize variable
         * called when this class it removed from a movement controller
         */
        public override void Dispose(PlayerMovement playerMovement)
        {
            moveAction = null;
        }

        /*
         * Function that get the new velocity
         * based on the current velocity of the rigidbody and the input
         */
        public override Vector3 GetVelocity(PlayerMovement playerMovement, float deltaTime)
        {
            // Retrieve the direction
            Vector3 direction = GetTargetDirection(playerMovement);
            // Apply speed to get velocity
            Vector3 targetVelocity = direction * maxSpeed;
            // Retrieve current velocity
            Vector3 currentVelocity = playerMovement.CurrentVelocity;
            
            float accelerationForce = 0;

            // Get angle between the current velocity and targeted velocity
            float alignment = Vector3.Dot(
                currentVelocity.normalized,
                targetVelocity.normalized
            );
            
            // If the new velocity is opposed to current velocity, add stopforce
            if (alignment < 0f)
                accelerationForce = GetStopForce(playerMovement);
            else
            {
                float currentSpeed = currentVelocity.sqrMagnitude;
                float maxSpeedSqr = maxSpeed * maxSpeed;
                
                // If underspeed accelerate, if turning accelerate, if overspeed decelerate
                if (currentSpeed < maxSpeedSqr)
                    accelerationForce = acceleration;
                else
                    accelerationForce = (alignment > 0.9f) ? deceleration : acceleration;
            }

            // Final velocity depend on the current velocity, the target velocity and the accelerationForce
            Vector3 newVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, accelerationForce * deltaTime);
            
            return newVelocity;
        }


        /*
         * Function that get the direction
         * based on the camera
         */
        protected virtual Vector3 GetTargetDirection(PlayerMovement controller)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;

            // Ignore vertical tilt for the camera (we only want x/z plan)
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            camForward.y = 0f;
            camRight.y = 0f;

            // Retrieve only the direction
            camForward.Normalize();
            camRight.Normalize();

            // Build direction so it's relative to camera
            Vector3 direction = camRight * input.x + camForward * input.y;

            // Prevent faster diagonal movement
            if (direction.sqrMagnitude > 1f)
                direction.Normalize();

            return direction;
        }
        
        protected virtual float GetStopForce(PlayerMovement orbitalController) => stopForce;
    }
}