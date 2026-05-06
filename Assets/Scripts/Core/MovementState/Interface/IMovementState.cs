using UnityEngine;

namespace Atypiki.Core.Core.MovementState.Interface
{
    public interface IMovementState<in T> where T : MovementController<T>
    {
        public int GetStatePriority(T orbitalController);
        public Vector3 GetVelocity(T playerMovement, float deltaTime);
        public void OnEnter(T orbitalController);
        public void OnExit(T orbitalController);
        public void Initialize(T orbitalController);
        public void Dispose(T orbitalController);

        void PreUpdate(T orbitalController);
    }
}