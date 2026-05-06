using UnityEngine;

namespace Atypiki.Core.Core.MovementState.State
{
    [CreateAssetMenu(fileName = "AirborneState", menuName = "Atypiki/Player/Airborne", order = 0)]
    public class PlayerAirborneState : PlayerControlledMovementState
    {
        public override void OnEnter(PlayerMovement playerMovement)
        {
            
        }

        public override void OnExit(PlayerMovement playerMovement)
        {
            
        }

        public override Vector3 GetVelocity(PlayerMovement playerMovement, float deltaTime)
        {
            var velocity = base.GetVelocity(playerMovement, deltaTime);
            
            return new Vector3()
            {
                x = velocity.x,
                y = playerMovement.CurrentVelocity.y,
                z = velocity.z
            };
        }

        public override int GetStatePriority(PlayerMovement playerMovement)
        {
            return playerMovement.IsGrounded ? -1 : 1;
        }
    }
}