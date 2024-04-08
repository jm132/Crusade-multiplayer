using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class CharaterAnimatorManager : MonoBehaviour
    {
        CharaterManager charater;

        int vertical;
        int horizontal;

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();

            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue, bool isSprinting)
        {
            float horizontalAmount = horizontalValue;
            float verticalAmount = verticalValue;

            if (isSprinting)
            {
                verticalAmount = 2;
            }

            charater.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            charater.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMation = true,
            bool canRotate = false,
            bool canMove = false)
        {
            charater.applyRootMotion = applyRootMation;
            charater.animator.CrossFade(targetAnimation, 0.2f);
            // can be used to stop character from attempting new actions
            charater.isPerfromingAction = isPerformingAction;
            charater.canRotate = canRotate;
            charater.canMove = canMove;

            // tell the host it played the animation, and to play that animation for everybody else present
            charater.characterNetworkManager.NotifyTheServerOfActionAnimtionServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMation);
        }

        public virtual void PlayTargetAttackActionAnimation(
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMation = true,
            bool canRotate = false,
            bool canMove = false)
        {
            // keep track of last attack performed (for combos) 
            // leep track of current attack tyoe (light, heavy, ect)
            // update animation set to current weapons animations
            // decide if attack can be parried
            // tell the network our "isAttacking" flag is active (for cunter damage ect)
            charater.applyRootMotion = applyRootMation;
            charater.animator.CrossFade(targetAnimation, 0.2f);
            charater.isPerfromingAction = isPerformingAction;
            charater.canRotate = canRotate;
            charater.canMove = canMove;
            
            // tell the host it played the animation, and to play that animation for everybody else present
            charater.characterNetworkManager.NotifyTheServerOfAttackActionAnimtionServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMation);
        }
    }
}