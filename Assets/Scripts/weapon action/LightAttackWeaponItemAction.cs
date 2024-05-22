using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Character Action/Weapon Actions/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] string light_Attack_01 = "Main_Light_Attack_01"; // main = main hand
        [SerializeField] string light_Attack_02 = "Main_Light_Attack_02"; // main = main hand
        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);
            
            if (!playerPerformingAction.IsOwner)
                return;

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0)
                return;

            if (!playerPerformingAction.charaterLocomotionManager.isGrounded)
                return;

            PerformLightAttacked(playerPerformingAction,weaponPerformingAction);
        }

        private void PerformLightAttacked(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // if is attacking currntaly, and can combo, perform the combo attack
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                // perform an attack based on the pervious attack just played
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == light_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack02, light_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01, light_Attack_01, true);
                }
            }
            // otherwise, if not already attacking, perform a regular attack
            else if (!playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01, light_Attack_01, true);
            }
        }
    }
}
