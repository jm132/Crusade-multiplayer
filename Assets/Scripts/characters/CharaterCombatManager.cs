using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.TextCore.Text;

namespace JM
{
    public class CharaterCombatManager : NetworkBehaviour
    {
        protected CharaterManager charater;

        [Header("Last Attack Animation Performed")]
        public string lastAttackAnimationPerformed;

        [Header("Attack Tagert")]
        public CharaterManager currentTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Lock On TransForm")]
        public Transform lockOnTransform;

        [Header("Attack Flags")]
        public bool canPerformRollingAttack = false;
        public bool canPerformBackstepAttack = false;
        public bool canBlock = true;

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();
        }

        public virtual void SetTarget(CharaterManager newTarget)
        {
            if (charater.IsOwner)
            {
                if (newTarget != null)
                {
                    currentTarget = newTarget;
                    charater.characterNetworkManager.currentTargetNetworkObjectID.Value = newTarget.GetComponent<NetworkObject>().NetworkObjectId; 
                }
                else
                {
                    currentTarget = null;
                }
            }
        }

        public void EnableIsInvulnerable()
        {
            if (charater.IsOwner)
                charater.characterNetworkManager.isInvulnerable.Value = true;
        }

        public void DisableIsInvulnerable()
        {
            if (charater.IsOwner)
                charater.characterNetworkManager.isInvulnerable.Value = false;
        }

        public void EnableCanDoRollingAttack()
        {
            canPerformRollingAttack = true;
        }

        public void DisableCanDoRollingAttack()
        {
            canPerformRollingAttack = false;
        }

        public void EnableCanDoBackstepAttack()
        {
            canPerformBackstepAttack = true;
        }

        public void DisableCanDoBackStepAttack()
        {
            canPerformBackstepAttack = false;
        }

        public virtual void EnableCanDoCombo()
        {

        }

        public virtual void DisableCanDoCombo()
        {

        }
    }
}
