using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerAnimatorManager : CharaterAnimatorManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        private void OnAnimatorMove()
        {
            if(player.characterAnimatorManager.applyRootMotion)
            {
                Vector3 velocity = player.animator.deltaPosition;
                player.characterController.Move(velocity);
                player.transform.rotation *= player.animator.deltaRotation;
            }
        }

        // animation event call
        public override void EnableCanDoCombo()
        {
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                player.playerCombatManager.canComboWithMainHandWeapon = true;
            }
            else
            {
                // enable off hand combo
            }
        }

        public override void DisableCanDoCombo()
        {
            player.playerCombatManager.canComboWithMainHandWeapon = false;
            //player.playerCombatManager.canComboWithOffHandWeapon = false;
        }
    }
}
