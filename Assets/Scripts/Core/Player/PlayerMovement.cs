using Atypiki.Core.Core.MovementState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Atypiki.Core
{
    public class PlayerMovement : MovementController<PlayerMovement>
    {
        [field: SerializeField]
        public PlayerManager Manager { get; private set; }
        
        public PlayerInput PlayerInput { get; private set; }
        [field: SerializeField] private PlayerMovementState[] defaultStates;
        
        private void OnValidate()
        {
            if (Manager == null)
                Manager = FindObjectOfType<PlayerManager>();
        }
        
        protected override void Awake()
        {
            base.Awake();
            PlayerInput = GetComponent<PlayerInput>();
        }
        
        private void Start()
        {
            for (int i = 0; i < defaultStates.Length; i++)
            {
                AddState(defaultStates[i]);
            }
        }

        protected override Rigidbody GetRigidbody()
        {
            return Manager.Body.rb;
        }

        protected override void ApplyVelocity(Vector3 velocity, Quaternion rotation)
        {
            Manager.Body.ApplyVelocity(velocity, rotation);
        }

        protected override void ChangeGounded(bool isGrounded)
        {
            Manager.Body.CheckGround(isGrounded);
        }
        
        
    }
}