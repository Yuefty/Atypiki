using Atypiki.Core.Core.MovementState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Atypiki.Core
{
    /*
     * This class control movement for the player
     * It uses the new input system and inherit MovementController
     * It need a player Body class to apply velocity to the player
     */
    public class PlayerMovement : MovementController<PlayerMovement>
    {
        [field: SerializeField]
        public PlayerManager Manager { get; private set; }
        
        public PlayerInput PlayerInput { get; private set; }
        [field: SerializeField] private PlayerMovementState[] defaultStates;
        
        /*
         * Called on Unity Editor only
         */
        private void OnValidate()
        {
            if (Manager == null)
                Manager = FindFirstObjectByType<PlayerManager>();
        }
        
        /*
         * Called when object is loaded
         * initialize variables
         */
        protected override void Awake()
        {
            base.Awake();
            PlayerInput = GetComponent<PlayerInput>();
            if (Manager == null)
                Manager = FindFirstObjectByType<PlayerManager>();
        }
        
        /*
         * Add default state to the list. those are all movement the player can do.
         */
        private void Start()
        {
            for (int i = 0; i < defaultStates.Length; i++)
            {
                AddState(defaultStates[i]);
            }
        }
        
        /*
         * Retrieve the rigid body from the Body 
         */
        protected override Rigidbody GetRigidbody()
        {
            return Manager.Body.rb;
        }

        /*
         * Let the body apply the velocity
         */
        protected override void ApplyVelocity(Vector3 velocity, Quaternion rotation)
        {
            Manager.Body.ApplyVelocity(velocity, rotation);
        }

        /*
         * Change grounded state on the body
         */
        protected override void ChangeGounded(bool isGrounded)
        {
            Manager.Body.CheckGround(isGrounded);
        }
        
        
    }
}