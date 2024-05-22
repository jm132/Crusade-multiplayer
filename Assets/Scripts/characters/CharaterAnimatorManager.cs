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

        [Header("Flags")]
        public bool applyRootMotion = false;

        [Header("Damage Animations")]
        public string lastDamageAnimationPlayed;

        [SerializeField] string hit_Forward_Medium_01 = "hit_Forward_Medium_01";
        [SerializeField] string hit_Forward_Medium_02 = "hit_Forward_Medium_02";

        [SerializeField] string hit_Backward_Medium_01 = "hit_Backward_Medium_01";
        [SerializeField] string hit_Backward_Medium_02 = "hit_Backward_Medium_02";

        [SerializeField] string hit_Left_Medium_01 = "hit_Left_Medium_01";
        [SerializeField] string hit_Left_Medium_02 = "hit_Left_Medium_02";

        [SerializeField] string hit_Right_Medium_01 = "hit_Right_Medium_01";
        [SerializeField] string hit_Right_Medium_02 = "hit_Right_Medium_02";

        public List<string> forward_Medium_Damage = new List<string>();
        public List<string> backward_Medium_Damage = new List<string>();
        public List<string> left_Medium_Damage = new List<string>();
        public List<string> right_Medium_Damage = new List<string>();

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();

            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        protected virtual void Start()
        {
            forward_Medium_Damage.Add(hit_Forward_Medium_01);
            forward_Medium_Damage.Add(hit_Forward_Medium_02);

            backward_Medium_Damage.Add(hit_Backward_Medium_01);
            backward_Medium_Damage.Add(hit_Backward_Medium_02);

            left_Medium_Damage.Add(hit_Left_Medium_01);
            left_Medium_Damage.Add(hit_Left_Medium_02);

            right_Medium_Damage.Add(hit_Right_Medium_01);
            right_Medium_Damage.Add(hit_Right_Medium_02);
        }

        public string GetRandomAnimationFromList(List<string> animationList)
        {
            List<string> finalList = new List<string>();
           
            foreach (var item in animationList)
            {
                finalList.Add(item);
            }

            // chech if already played this damage animation so it doesnt repeat
            finalList.Remove(lastDamageAnimationPlayed);

            // chrck the list for null entries, and remove them
            for (int i = finalList.Count - 1; i > -1; i--)
            {
                if (finalList[i] == null)
                {
                    finalList.RemoveAt(i);
                }
            }

            int randomValue = Random.Range(0, finalList.Count);

            return finalList[randomValue];
        }

        public void UpdateAnimatorMovementParameters(float horizontalMovment, float verticalMovement, bool isSprinting)
        {
            float snappedHorizontal;
            float snappedVertical;

            // this if chain will round the horizontal movement to -1, -0.5, 0, 0.5 or 1

            if (horizontalMovment > 0 && horizontalMovment <= 0.5f)
            {
                snappedHorizontal = 0.5f;
            }
            else if (horizontalMovment > 0.5f && horizontalMovment <= 1)
            {
                snappedHorizontal = 1;
            }
            else if (horizontalMovment < 0 && horizontalMovment >= -0.5f)
            {
                snappedHorizontal = -0.5f;
            }
            else if (horizontalMovment < -0.5f &&  horizontalMovment >= -1)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }
            // this if chain will round the horizontal movement to -1, -0.5, 0, 0.5 or 1

            if (verticalMovement > 0 && verticalMovement <= 0.5f)
            {
                snappedVertical = 0.5f;
            }
            else if (verticalMovement > 0.5f && verticalMovement <= 1)
            {
                snappedVertical = 1;
            }
            else if (verticalMovement < 0 && verticalMovement >= -0.5f)
            {
                snappedVertical = -0.5f;
            }
            else if (verticalMovement < -0.5f && verticalMovement >= -1)
            {
                snappedVertical = -1;
            }
            else
            {
                snappedVertical = 0;
            }

            if (isSprinting)
            {
                snappedVertical = 2;
            }

            charater.animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            charater.animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate = false,
            bool canMove = false)
        {
            this.applyRootMotion = applyRootMotion;
            charater.animator.CrossFade(targetAnimation, 0.2f);
            // can be used to stop character from attempting new actions
            charater.isPerfromingAction = isPerformingAction;
            charater.charaterLocomotionManager.canRotate = canRotate;
            charater.charaterLocomotionManager.canMove = canMove;

            // tell the host it played the animation, and to play that animation for everybody else present
            charater.characterNetworkManager.NotifyTheServerOfActionAnimtionServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }

        public virtual void PlayTargetAttackActionAnimation(AttackType attackType,
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

            charater.characterCombatManager.currentAttackType = attackType;
            charater.characterCombatManager.lastAttackAnimationPerformed = targetAnimation;
            charater.characterAnimatorManager.applyRootMotion = applyRootMation;
            charater.animator.CrossFade(targetAnimation, 0.2f);
            charater.isPerfromingAction = isPerformingAction;
            charater.charaterLocomotionManager.canRotate = canRotate;
            charater.charaterLocomotionManager.canMove = canMove;
            
            // tell the host it played the animation, and to play that animation for everybody else present
            charater.characterNetworkManager.NotifyTheServerOfAttackActionAnimtionServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMation);
        }

        public virtual void EnableCanDoCombo()
        {

        }

        public virtual void DisableCanDoCombo()
        {

        }
    }
}