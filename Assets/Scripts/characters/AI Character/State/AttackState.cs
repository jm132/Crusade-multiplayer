using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "A.I/States/Attack")]
    public class AttackState : AIState
    {
        [HideInInspector] public AICharacterAttackAtion currentAttack;
        [HideInInspector] public bool willPerformCombo = false;

        [Header("State Flags")]
        protected bool hasPerformedAttack = false;
        protected bool hasPerformedCombo = false;

        [Header("Pivot After Attack")]
        [SerializeField] protected bool pivotAfterAttack = false;

        public override AIState Tick(AICharacterManager aICharacter)
        {
            if (aICharacter.aiCharacterCombarManager.currentTarget == null)
                return SwitchState(aICharacter, aICharacter.idle);

            if (aICharacter.aiCharacterCombarManager.currentTarget.isDead.Value)
                return SwitchState(aICharacter, aICharacter.idle);

            aICharacter.aiCharacterCombarManager.RoatateTowardsTargetWhilstAttacking(aICharacter);

            aICharacter.characterAnimatorManager.UpdateAnimatorMovementParameters(0, 0, false);

            // perform a combo
            if (willPerformCombo && !hasPerformedCombo)
            {
                if (currentAttack.comboAction != null)
                {
                    // if can combo
                    //hasPerformedCombo = true;
                    //currentAttack.comboAction.AttemptToPerformAction(aICharacter);
                }
            }

            if (aICharacter.isPerfromingAction)
                return this;

            if (!hasPerformedAttack)
            {
                // if still recovering from an action, wait before performing another
                if (aICharacter.aiCharacterCombarManager.actionRecoveryTimer > 0)
                    return this;

                PerformAttack(aICharacter);

                // return to the top, so if a combo be process that
                return this;
            }

            if (pivotAfterAttack)
                aICharacter.aiCharacterCombarManager.PivotTowardsTarget(aICharacter);

            return SwitchState(aICharacter, aICharacter.combatStance);
        }

        protected void PerformAttack(AICharacterManager aICharacter)
        {
            hasPerformedAttack = true;
            currentAttack.AttemptToPerformAction(aICharacter);
            aICharacter.aiCharacterCombarManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
        }

        protected override void ResetStateFlags(AICharacterManager aiCharacter)
        {
            base.ResetStateFlags(aiCharacter);

            hasPerformedAttack = false;
            hasPerformedCombo = false;
        }
    }
}
