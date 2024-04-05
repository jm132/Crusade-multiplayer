using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharaterLocomotionManager : MonoBehaviour
    {
        CharaterManager charater;

        [Header("Ground Check & Jumping")]
        [SerializeField] protected float gravityForce = -5.55f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSpherRadius = 1;
        [SerializeField] protected Vector3 yVelocity; // the force at which the character is pulled up or down (jumping or falling)
        [SerializeField] protected float groundedYVelocity = -20; // the force at which the character is sticking to the ground whilst on the grounded
        [SerializeField] protected float fallStartYVelocity = -5; // the force at which the character begins to fall when become to ungrounded (pises as they fall longer)
        protected bool fallingVelocityHAsBeenSet = false;
        protected float inAirTimer = 0;

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();
        }

        protected virtual void Update()
        {
            HandleGroundCheck();

            if (charater.isGrounded)
            {
                // not attempting to jump or move upward
                if(yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocityHAsBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                //if not jumping, and falling velocity has not been set
                if (!charater.characterNetworkManager.isJumping.Value && !fallingVelocityHAsBeenSet)
                {
                    fallingVelocityHAsBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                charater.animator.SetFloat("InAirTimer", inAirTimer);

                yVelocity.y += gravityForce * Time.deltaTime;
            }

            // there should always be some force applied to the y velocity
            charater.characterController.Move(yVelocity * Time.deltaTime);
        }

        protected void HandleGroundCheck()
        {
            charater.isGrounded = Physics.CheckSphere(charater.transform.position, groundCheckSpherRadius, groundLayer);
        }

        // draws ground check sphere in scene view
        protected void OnDrawGizmosSelected()
        {
            //Gizmos.DrawSphere(charater.transform.position, groundCheckSpherRadius);
        }
    }
}