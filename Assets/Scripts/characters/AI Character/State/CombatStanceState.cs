using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JM
{
    [CreateAssetMenu(menuName = "A.I/States/Combat Stance")]
    public class CombatStanceState : AIState
    {
        [Header("Attacks")]
        public List<AICharacterAttackAtion> aiCharacterAttacks;   // a list of all possible attacks this character can do 
        protected List<AICharacterAttackAtion> potentialAttacks;  // all attacjs possible in this situation (based on angle, distance ect)
        private AICharacterAttackAtion choosenAttack;
        private AICharacterAttackAtion previouesAttack;
        protected bool hasAttack = false;

        [Header("Combo")]
        [SerializeField] protected bool canPerformCombo = false;  // if the character can perform a combo attack, after the initial attack
        [SerializeField] protected int chanceToPerformCombo = 25; // the chance (in percent) of the character to perform a combo on the next attack
        protected bool hasRolledForComboChance = false;           // if alreadt rolled for the chance during this state

        [Header("Engagement Distance")]
        [SerializeField] public float maximumEngagementDistance = 5; // the distance havbe to be away from the target before enter the pursue target state

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerfromingAction)
                return this;

            if (!aiCharacter.navMeshAgent.enabled)
                aiCharacter.navMeshAgent.enabled = true;

            // if the ai character to face and turn toward its target when its outside it's fov include this
            if (!aiCharacter.aiCharacterNetworkManager.isMoving.Value)
            {
                if (aiCharacter.aiCharacterCombarManager.viewableAngle < -30 || aiCharacter.aiCharacterCombarManager.viewableAngle > 30)
                    aiCharacter.aiCharacterCombarManager.PivotTowardsTarget(aiCharacter);
            }

            aiCharacter.aiCharacterCombarManager.RoatateTowardsAgent(aiCharacter);

            // if target is no longer present, switch back to idle
            if (aiCharacter.aiCharacterCombarManager.currentTarget == null)
                return SwitchState(aiCharacter, aiCharacter.idle);

            // if do not have and attack, get one
            if (!hasAttack)
            {
                GetNewAttack(aiCharacter);
            }
            else
            {
                aiCharacter.attacks.currentAttack = choosenAttack;
                // roll for combo chance
                return SwitchState(aiCharacter, aiCharacter.attacks);
            }

            // if are outside of the combot engagement distance, switch to the pursue target state
            if (aiCharacter.aiCharacterCombarManager.distanceFromTarget > maximumEngagementDistance)
                return SwitchState(aiCharacter, aiCharacter.pursueTarget);

            NavMeshPath path = new NavMeshPath();
            aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombarManager.currentTarget.transform.position, path);
            aiCharacter.navMeshAgent.SetPath(path);

            return this;
        }

        protected virtual void GetNewAttack(AICharacterManager aiCharacter)
        {
            potentialAttacks = new List<AICharacterAttackAtion>();

            foreach (var potentialAttack in aiCharacterAttacks)
            {
                Debug.Log("0");
                // if too close for this attack, check the next
                if (potentialAttack.minimumAttackDistance > aiCharacter.aiCharacterCombarManager.distanceFromTarget)
                    continue;
                Debug.Log("1");
                //  if too far for this attack, check the next
                if (potentialAttack.maximumAttackDistance < aiCharacter.aiCharacterCombarManager.distanceFromTarget)
                    continue;
                Debug.Log("2");
                // if the target is outside minimum field of view for this attack, check the next
                if (potentialAttack.minimumAttackAngle > aiCharacter.aiCharacterCombarManager.distanceFromTarget)
                    continue;
                Debug.Log("3");
                // if the target is outside maximum field of view for this attack, check the next 
                if (potentialAttack.maximumAttackAngle < aiCharacter.aiCharacterCombarManager.distanceFromTarget)
                    continue;
                Debug.Log("4");

                potentialAttacks.Add(potentialAttack);
            }

            if (potentialAttacks.Count <= 0)
                return;

           var totalWeight = 0;

            foreach (var attack in potentialAttacks)
            {
                totalWeight += attack.attackWeight;
            }

            var randomWeightValue = Random.Range(1, totalWeight + 1);
            var processedWeight = 0;

            foreach (var attack in potentialAttacks)
            {
                processedWeight += attack.attackWeight;

                if (randomWeightValue <= processedWeight)
                {
                    choosenAttack = attack;
                    previouesAttack = choosenAttack;
                    hasAttack = true;
                    return;
                }
            }

            // 4. pick one of the remaining attacks rendomly, based on weight
            // 5. select this attask and pass it to the attack state 
        }

        protected virtual bool RoolForOutcomeChance(int outcomeChance)
        {
            bool outcomeWillBePerformed = false;

            int randomPercentage = Random.Range(0, 100);

            if(randomPercentage < outcomeChance)
                outcomeWillBePerformed = true;

            return outcomeWillBePerformed;
        }

        protected override void ResetStateFlags(AICharacterManager aiCharacter)
        {
            base.ResetStateFlags(aiCharacter);

            hasAttack = false;
            hasRolledForComboChance = false;
        }
    }
}
