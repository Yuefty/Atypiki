using Atypiki.Core.Core.Player.Component;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Atypiki.Core.Core.Player
{
    public class CameraLook : PlayerComponent
    {
        [Header("Settings")]
        public float sensitivity = 200f;
        public float maxPitch = 80f;

        private float pitch;
        private Vector2 lookInput;

        private void Start()
        {
            // Lock cursor for TPS control
            // Cursor.lockState = CursorLockMode.Locked;
            // Hide the cursor
            Cursor.visible = false; 
        }

        private void FixedUpdate()
        {
            //Look();
        }

        private void Look()
        {
            Vector2 targetMouseDelta = Mouse.current.delta.ReadValue().normalized*Time.smoothDeltaTime;
            
            float mouseX = targetMouseDelta.x * sensitivity ;
            float mouseY = (targetMouseDelta.y) * sensitivity ;

            // Vertical rotation (camera up/down)
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

            Vector3 rotation = Vector3.up * mouseX;
            transform.localRotation = Quaternion.Euler(pitch,mouseX, 0f );
        }
        
    }
}
