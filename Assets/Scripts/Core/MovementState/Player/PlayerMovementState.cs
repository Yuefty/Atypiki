using Atypiki.Core.Core.MovementState.Interface;
using Atypiki.Core.Core.Player.Component;
using UnityEngine;

namespace Atypiki.Core.Core.MovementState.Player
{
    public abstract class PlayerMovementState : ScriptableObject, IMovementState<PlayerMovement>
    {
        public int StatePriority { get; private set; }
        public abstract void Initialize(PlayerMovement orbitalController);
        public abstract void Dispose(PlayerMovement orbitalController);
        public abstract void OnEnter(PlayerMovement orbitalController);
        public abstract void OnExit(PlayerMovement orbitalController);
        public virtual void PreUpdate(PlayerMovement orbitalController) { }
        public abstract int GetStatePriority(PlayerMovement orbitalController);
        public abstract Vector3 GetVelocity(PlayerMovement playerMovement, float deltaTime);
    }
}