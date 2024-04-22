using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.TextCore.Text;

namespace JM
{
    public class CharaterCombatManager : NetworkBehaviour
    {
        CharaterManager charater;

        [Header("Attack Tagert")]
        public CharaterManager curremtTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Lock On TransForm")]
        public Transform lockOnTransform;

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
                    curremtTarget = newTarget;
                    charater.characterNetworkManager.currentTargetNetworkObjectID.Value = newTarget.GetComponent<NetworkObject>().NetworkObjectId; 
                }
                else
                {
                    curremtTarget = null;
                }
            }
        }
    }
}
