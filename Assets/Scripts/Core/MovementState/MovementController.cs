using System;
using System.Collections.Generic;
using Atypiki.Core.Core.MovementState.Interface;
using DG.Tweening;
using UnityEngine;

namespace Atypiki.Core.Core.MovementState
{
    /*
     * This class compute velocity and handle movement state
     */
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class MovementController<T> : MonoBehaviour where T : MovementController<T>
    { 
     
        [SerializeField, Range(1,64)] private int maxPenetrationCount;
        [SerializeField, Range(0,10)] private float gravityScale;
        [SerializeField] private LayerMask groundMask;
        [SerializeField, Range(0,1)] private float groundDetectionRange;
        [SerializeField, Range(0,90)] private float groundMaxAngle;
        [SerializeField] private bool IsFlying;
        [SerializeField] private float rotationSpeed;

       
        public Vector3 CurrentVelocity { get; private set; }
        public bool IsGrounded { get; private set; }
        public Vector3 GroundNormal { get; private set; }
        public Vector3 GroundPosition { get; private set; }


        private List<IMovementState<T>> _movementStates;
        public IMovementState<T> currentMovementState { get; private set; }
        private Rigidbody rb => GetRigidbody();
        private CapsuleCollider cc;
        private static readonly Collider[] colliders = new Collider[16];
        private static readonly RaycastHit[] raycastHits = new RaycastHit[16];
        
        

        protected virtual void Awake()
        {
            this.cc = GetComponent<CapsuleCollider>();
            _movementStates = new List<IMovementState<T>>();
        }
        
        protected virtual void FixedUpdate()
        {
            CheckGround();
            SelectNextState();
            ComputeVelocity();
            if (!IsFlying)
            {
                ApplyGravity();
            }
            Move();
        }

        private T GetController() => this as T;
        protected abstract Rigidbody GetRigidbody();
        protected abstract void ApplyVelocity(Vector3 velocity, Quaternion rotation);
        protected abstract void ChangeGrounded(bool IsGrounded);

        /*
         * Add state to the list and initialize it.
         */
        public void AddState(IMovementState<T> orbitalMovementState)
        {
            if (_movementStates.Contains(orbitalMovementState))
            {
                return;
            }
            _movementStates.Add(orbitalMovementState);
            orbitalMovementState.Initialize(GetController());
        }

        /*
         * Remove state and uninitialize it
         */
        public void RemoveState(IMovementState<T> orbitalMovementState)
        {
            if (_movementStates.Remove(orbitalMovementState))
            {
                orbitalMovementState.Dispose(GetController());
            }
        }

        /*
         * Get the state with the higher priority
         */
        private void SelectNextState()
        {
            IMovementState<T> nextMovementState = null;
            T controller = GetController();
            int maxPriority = 0;
            
            foreach (var state in _movementStates)
            {
                state.PreUpdate(controller);
                int priority = state.GetStatePriority(controller);
                if (priority > maxPriority)
                {
                    maxPriority = priority;
                    nextMovementState = state;
                }
            }

            if (currentMovementState != nextMovementState)
            {
                currentMovementState?.OnExit(controller);
                nextMovementState?.OnEnter(controller);
                currentMovementState = nextMovementState;
            }
            //Debug.Log("the current state is : "+ currentMovementState.GetType().Name);
        }

        private void ComputeVelocity()
        {
            if (currentMovementState==null)
            {
                CurrentVelocity = Vector3.zero;
                return;
            }
            CurrentVelocity = currentMovementState.GetVelocity(GetController(), Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (!IsGrounded)
            {
                CurrentVelocity += Vector3.down * (gravityScale * Time.deltaTime * 9.81f);
            }
            else if(CurrentVelocity.y <= 0)
            {
                CurrentVelocity = new Vector3(CurrentVelocity.x, 0, CurrentVelocity.z);
            }
            //Debug.Log("gravity : "+CurrentVelocity);
        }
        private void Move()
        {
            float deltaTime = Time.deltaTime;
        
            
            Vector3 newPosition = rb.position + new Vector3(CurrentVelocity.x, CurrentVelocity.y, CurrentVelocity.z);
            Vector3 lastPosition = rb.position;
            
            Vector3 finalVelocity = (newPosition - lastPosition);

            /*
            Debug.Log("current velocity on y : "+ CurrentVelocity.y+" final velocity on y : "+ finalVelocity.y);
            Debug.Log("current velocity on x : "+ CurrentVelocity.x+" final velocity on x : "+ finalVelocity.x);
            Debug.Log("current velocity on z : "+ CurrentVelocity.z+" final velocity on z : "+ finalVelocity.z);
            */
            
            Vector3 p1 = lastPosition + cc.center + transform.up * (-cc.height * 0.25f);
            Vector3 p2 = p1 + transform.up * cc.height;


            Vector3 collisionOffset = Vector3.zero;

            for (int i = 0; i < maxPenetrationCount; i++)
            {
                Vector3 nextPosition = lastPosition + collisionOffset + finalVelocity * deltaTime;
                
                Vector3 nextP1 = p1 + collisionOffset ;
                Vector3 nextP2 = p2 + collisionOffset ;
                int count = Physics.OverlapCapsuleNonAlloc(nextP1, nextP2, cc.radius - 0.01f, colliders);

                int numberOfObstacle = 0;
                
                if (count > 0)
                {
                    for (int j = 0; j < count; j++)
                    {
                        Collider c = colliders[j];

                        if (c.attachedRigidbody == rb)
                            continue;

                        if (Physics.GetIgnoreLayerCollision(c.gameObject.layer, cc.gameObject.layer))
                            continue;

                        if (Physics.GetIgnoreCollision(c, cc))
                            continue;
                        if (c.isTrigger)
                            continue;

                        numberOfObstacle++;
                        Vector3 otherPosition = c.transform.position;
                        Quaternion otherRotation = c.transform.rotation;

                        if (Physics.ComputePenetration(cc, nextPosition, rb.rotation, c, otherPosition,
                                otherRotation, out Vector3 direction, out float distance))
                        {
                            Vector3 offset = direction * distance;
                            Debug.DrawLine(rb.position + collisionOffset, rb.position + collisionOffset + offset);
                            collisionOffset += offset;
                        }
                    }
                }
                
            }
            //Debug.Log(" final vel : "+angularVelocity);
            
            Vector3 NewPosition =  lastPosition + collisionOffset + finalVelocity * deltaTime;
            ApplyVelocity(NewPosition, RotateTowardsCam( deltaTime));
            //rb.MovePosition(NewPosition);
        }
        
        private void CheckGround()
        {
            var up = transform.up;
            Vector3 p1 = rb.position + cc.center + up * (-cc.height * 0.25f);
            Vector3 p2 = p1 + up * cc.height;

            float shrink = 0.02f;
            int count = Physics.CapsuleCastNonAlloc(p1, p2, cc.radius - shrink, Vector3.down, raycastHits,
                groundDetectionRange + shrink, groundMask);
            IsGrounded = false;
            GroundNormal = Vector3.up;
            GroundPosition = rb.position;

            for (int i = 0; i < count; i++)
            {
                RaycastHit hit = raycastHits[i];
                float angle = Vector3.Angle(Vector3.up, hit.normal);
                if (angle < groundMaxAngle)
                {
                    IsGrounded = true;
                    GroundNormal = hit.normal;
                    GroundPosition = hit.point;
                    ChangeGrounded(IsGrounded);
                    return;
                }
            }
            ChangeGrounded(IsGrounded);
        }
        
        
        private Quaternion RotateTowardsCam(float deltaTime)
        {
            Transform cam = Camera.main.transform;
            Vector3 camForward = cam.forward;
            camForward.y = 0f;
            
            Quaternion targetRotation = Quaternion.LookRotation(camForward, Vector3.up);

            // Smooth rotation
            rotationSpeed = 10f; 
            Quaternion newRotation = Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * deltaTime
            );

            return newRotation;
        }
    }
}
