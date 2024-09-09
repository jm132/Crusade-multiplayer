using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Character Action/Weapon Actions/Heavy Attack Action")]
    public class HeavyAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] string heavy_Attack_01 = "Main_Heavy_Attack_01"; // main = main hand
        [SerializeField] string heavy_Attack_02 = "Main_Heavy_Attack_02";
        [SerializeField] string th_heavy_Attack_01 = "TH_Heavy_Attack_01"; // main = main hand
        [SerializeField] string th_heavy_Attack_02 = "TH_Heavy_Attack_02";
        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {

            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.IsOwner)
                return;

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0)
                return;

            if (!playerPerformingAction.charaterLocomotionManager.isGrounded)
                return;

            if (playerPerformingAction.IsOwner)
                playerPerformingAction.playerNetworkManager.isAttacking.Value = true;

            PerformHeavyAttacked(playerPerformingAction, weaponPerformingAction);
        }

        private void PerformHeavyAttacked(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                PerformTwoHamdHeavyAttack(playerPerformingAction, weaponPerformingAction);
            }
            else
            {
                PerformMainHamdHeavyAttack(playerPerformingAction, weaponPerformingAction);
            }
        }

        private void PerformMainHamdHeavyAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                // perform an attack based on the pervious attack just played
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == heavy_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack02, heavy_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, heavy_Attack_01, true);
                }
            }
            // otherwise, if not already attacking, perform a regular attack
            else if (!playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, heavy_Attack_01, true);
            }
        }
        private void PerformTwoHamdHeavyAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                // perform an attack based on the pervious attack just played
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == heavy_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack02, th_heavy_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, th_heavy_Attack_01, true);
                }
            }
            // otherwise, if not already attacking, perform a regular attack
            else if (!playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, th_heavy_Attack_01, true);
            }
        }
    }
}
