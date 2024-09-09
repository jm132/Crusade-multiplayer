using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Character Action/Weapon Actions/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        // main hand
        [Header("Light Attack")]
        [SerializeField] string light_Attack_01 = "Main_Light_Attack_01"; // main = main hand
        [SerializeField] string light_Attack_02 = "Main_Light_Attack_02"; // main = main hand

        [Header("Running Attacks")]
        [SerializeField] string run_Attack_01 = "Main_Run_Attack_01";

        [Header("Rolling Attack")]
        [SerializeField] string roll_Attack_01 = "Main_Roll_Attack_01";

        [Header("Backstep Attack")]
        [SerializeField] string backstep_Attack_01 = "Main_Backstep_Attack_01";

        // two hand
        [Header("Light Attack")]
        [SerializeField] string th_light_Attack_01 = "TH_Light_Attack_01";
        [SerializeField] string th_light_Attack_02 = "TH_Light_Attack_02";

        [Header("Running Attacks")]
        [SerializeField] string th_run_Attack_01 = "TH_Run_Attack_01";

        [Header("Rolling Attack")]
        [SerializeField] string th_roll_Attack_01 = "TH_Roll_Attack_01";

        [Header("Backstep Attack")]
        [SerializeField] string th_backstep_Attack_01 = "TH_Backstep_Attack_01";

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);
            
            if (!playerPerformingAction.IsOwner)
                return;

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0)
                return;

            if (!playerPerformingAction.charaterLocomotionManager.isGrounded)
                return;

            if(playerPerformingAction.IsOwner)
                playerPerformingAction.playerNetworkManager.isAttacking.Value = true;

            // if sprinting, perform a running attack
            if (playerPerformingAction.characterNetworkManager.isSprinting.Value)
            {
                PerformRunningAttacked(playerPerformingAction, weaponPerformingAction);
                return;
            }

            // if Rolling, perform a rolling attack
            if (playerPerformingAction.characterCombatManager.canPerformRollingAttack)
            {
                PerformRollingAttacked(playerPerformingAction, weaponPerformingAction);
                return;
            }

            // if backstepping, perform a backstep attack
            if (playerPerformingAction.characterCombatManager.canPerformBackstepAttack)
            {
                PerformBackstepAttacked(playerPerformingAction, weaponPerformingAction);
                return;
            }

            PerformLightAttacked(playerPerformingAction,weaponPerformingAction);
        }

        private void PerformLightAttacked(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                PerfromTwoHandLightAttack(playerPerformingAction, weaponPerformingAction);
            }
            else
            {
                PerfromMainHandLightAttack(playerPerformingAction, weaponPerformingAction);
            }
        }

        private void PerfromMainHandLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // if is attacking currntaly, and can combo, perform the combo attack
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                // perform an attack based on the pervious attack just played
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == light_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack02, light_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, light_Attack_01, true);
                }
            }
            // otherwise, if not already attacking, perform a regular attack
            else if (!playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, light_Attack_01, true);
            }
        }

        private void PerfromTwoHandLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // if is attacking currntaly, and can combo, perform the combo attack
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                // perform an attack based on the pervious attack just played
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == th_light_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack02, th_light_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, th_light_Attack_01, true);
                }
            }
            // otherwise, if not already attacking, perform a regular attack
            else if (!playerPerformingAction.isPerfromingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, th_light_Attack_01, true);
            }
        }

        private void PerformRunningAttacked(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RunningAttack01, th_run_Attack_01, true);
            }
            else
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RunningAttack01, run_Attack_01, true);
            }
        }

        private void PerformRollingAttacked(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            playerPerformingAction.playerCombatManager.canPerformRollingAttack = false;

            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RollingAttack01, th_roll_Attack_01, true);
            }
            else
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RollingAttack01, roll_Attack_01, true);
            }
        }

        private void PerformBackstepAttacked(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            playerPerformingAction.playerCombatManager.canPerformBackstepAttack = false;

            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.BackstepAttack01, th_backstep_Attack_01, true);
            }
            else
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.BackstepAttack01, backstep_Attack_01, true);
            }
        }
    }
}
