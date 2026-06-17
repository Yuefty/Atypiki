using Atypiki.Core.Core.Player.Component;
using UnityEngine;

namespace Atypiki.Core.Core.MovementState.Player.State
{
    [CreateAssetMenu(fileName = "walkState", menuName = "Atypiki/Player/Run", order = 0)]
    public class PlayerRunState : PlayerControlledMovementState
    {
        public override int GetStatePriority(PlayerMovement playerMovement)
        {
            return playerMovement.IsGrounded ? 1 : -1;
        }

        public override void OnEnter(PlayerMovement playerMovement)
        {
           
        }

        public override void OnExit(PlayerMovement playerMovement)
        {
            
        }
    }
}