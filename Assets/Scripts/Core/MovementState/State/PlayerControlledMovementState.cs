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
        
        public override void Initialize(PlayerMovement playerMovement)
        {
            moveAction = playerMovement.PlayerInput.actions.FindActionMap("Player").FindAction("Move");
        }

        public override void Dispose(PlayerMovement playerMovement)
        {
            moveAction = null;
        }

        public override Vector3 GetVelocity(PlayerMovement playerMovement, float deltaTime)
        {
            Vector3 direction = GetTargetDirection(playerMovement);
            Vector3 targetVelocity = direction * maxSpeed;
            Vector3 currentVelocity = playerMovement.CurrentVelocity;
            
            float accelerationForce = 0;

            if (Vector3.Dot(targetVelocity, currentVelocity) < 0)
                accelerationForce = GetStopForce(playerMovement);
            else
            {
                float currentSpeed = currentVelocity.sqrMagnitude;
                float maxSpeedSqr = maxSpeed * maxSpeed;
                
                if (currentSpeed < maxSpeedSqr)
                    accelerationForce = acceleration;
                else
                    accelerationForce = deceleration;
            }

            Vector3 newVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, accelerationForce * deltaTime);
            
            return newVelocity;
        }


        protected virtual Vector3 GetTargetDirection(PlayerMovement controller)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
            return direction;
        }
        
        protected virtual float GetStopForce(PlayerMovement orbitalController) => stopForce;
    }
}