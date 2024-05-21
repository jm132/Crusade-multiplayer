using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AICharacterAnimatorManager : CharaterAnimatorManager
    {
        AICharacterManager aICharacter;

        protected override void Awake()
        {
            base.Awake();

            aICharacter = GetComponent<AICharacterManager>();
        }

        private void OnAnimatorMove()
        {
            if (aICharacter.IsOwner)
            {
                if (!aICharacter.isGrounded)
                    return;

                Vector3 velocity = aICharacter.animator.deltaPosition;

                aICharacter.characterController.Move(velocity);
                aICharacter.transform.rotation *= aICharacter.animator.deltaRotation;
            }
            else
            {
                if (!aICharacter.isGrounded)
                    return;

                Vector3 velocity = aICharacter.animator.deltaPosition;

                aICharacter.characterController.Move(velocity);
                aICharacter.transform.position = Vector3.SmoothDamp(transform.position,
                    aICharacter.characterNetworkManager.networkPosition.Value, 
                    ref aICharacter.characterNetworkManager.networkPositionVelocity,
                    aICharacter.characterNetworkManager.networkPositionSmoothTime);
                aICharacter.transform.rotation *= aICharacter.animator.deltaRotation;
            }
        }
    }
}
