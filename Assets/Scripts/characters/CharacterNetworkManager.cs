using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        CharaterManager charater;

        [Header("Active")]
        public NetworkVariable<bool> isActive = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Position")]
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 networkPositionVelocity;
        public float networkPositionSmoothTime = 0.1f;
        public float networkRotationSmoothTime = 0.1f;

        [Header("Animator")]
        public NetworkVariable<bool> isMoving = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> moveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Target")]
        public NetworkVariable<ulong> currentTargetNetworkObjectID = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        
        [Header("Flags")]
        public NetworkVariable<bool> isBlocking = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isAttacking = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isInvulnerable = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isLockedOn = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isSprinting = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isJumping = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isChargingAttack = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Resources")]
        public NetworkVariable<int> currentHealth = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> maxHealth = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> currentStamina = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> maxStamina = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Stats")]
        public NetworkVariable<int> vitality = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> endurance = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> strenght = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Stats Modifiers")]
        public NetworkVariable<int> strenghtModifier = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();
        }

        public virtual void CheckHP(int oldValue, int newValue)
        {
            if (currentHealth.Value <= 0)
            {
                StartCoroutine(charater.ProessDeathEvent());
            }

            // prevents us from over healing
            if (charater.IsOwner)
            {
                if (currentHealth.Value > maxHealth.Value)
                {
                    currentHealth.Value = maxHealth.Value;
                }
            }
        }

        public void OnLockOnTargetIDChange(ulong oldID, ulong newID)
        {
            if (!IsOwner)
            {
                charater.characterCombatManager.currentTarget = NetworkManager.Singleton.SpawnManager.SpawnedObjects[newID].gameObject.GetComponent<CharaterManager>();
            }
        }

        public void OnIsLockedOnChanged(bool old, bool isLockedOn)
        {
            if (!isLockedOn)
            {
                charater.characterCombatManager.currentTarget = null;
            }
        }

        public void OnIsChargingAttackChaged(bool oldStatus, bool newStatus)
        {
            charater.animator.SetBool("isChargingAttack", isChargingAttack.Value);
        }

        public void OnIsMovingChanged(bool oldStatus, bool newStatus)
        {
            charater.animator.SetBool("isMoving", isMoving.Value);
        }

        public virtual void OnIsActiveChanged(bool oldStatus, bool newStatus)
        {
            gameObject.SetActive(isActive.Value);
        }

        public virtual void OnIsBlockingChanged(bool oldStatus, bool newStatus)
        {
            charater.animator.SetBool("isBlocking", isBlocking.Value);
        }

        // a Server rpc is a function called from a client, to the server/host
        [ServerRpc]
        public void NotifyTheServerOfActionAnimtionServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // if this Character is the host/server then activate the client rpc
            if (IsServer)
            {
                PlayActionAnimationForAllClientsClientRpc(clientID, animationID, applyRootMotion);
            }
        }

        // a client rpc is sent to all clients present, from the server
        [ClientRpc]
        public void PlayActionAnimationForAllClientsClientRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // we make sure to no run the function on the character who sent it(so we dont plat the animation twice)
            if(clientID != NetworkManager.Singleton.LocalClientId)
            {
                performActionAnimationFromServer(animationID, applyRootMotion);
            }
        }
        
        private void performActionAnimationFromServer(string animationID, bool applyRootMotion)
        {
            charater.characterAnimatorManager.applyRootMotion = applyRootMotion;
            charater.animator.CrossFade(animationID, 0.2f);
        }

        // attack animation

        [ServerRpc]
        public void NotifyTheServerOfAttackActionAnimtionServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // if this Character is the host/server then activate the client rpc
            if (IsServer)
            {
                PlayAttackActionAnimationForAllClientsClientRpc(clientID, animationID, applyRootMotion);
            }
        }

        [ClientRpc]
        public void PlayAttackActionAnimationForAllClientsClientRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // we make sure to no run the function on the character who sent it(so we dont plat the animation twice)
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                performAttackActionAnimationFromServer(animationID, applyRootMotion);
            }
        }

        private void performAttackActionAnimationFromServer(string animationID, bool applyRootMotion)
        {
            charater.characterAnimatorManager.applyRootMotion = applyRootMotion;
            charater.animator.CrossFade(animationID, 0.2f);
        }

        // damage
        [ServerRpc(RequireOwnership = false)]
        public void NotifyTheServerOfCharacterDamageServerRpc(
            ulong damagedCharacter,
            ulong characterCausingDamage,
            float physicalDamage,
            float magicDamage,
            float fireDamage,
            float holyDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            if (IsServer)
            {
                NotifyTheServerOfCharacterDamageClientRpc(damagedCharacter, characterCausingDamage, physicalDamage, magicDamage, fireDamage, holyDamage, poiseDamage, angleHitFrom, contactPointX, contactPointY, contactPointZ);
            }
        }

        [ClientRpc]
        public void NotifyTheServerOfCharacterDamageClientRpc(
            ulong damagedCharacterID,
            ulong characterCausingDamageID,
            float physicalDamage,
            float magicDamage,
            float fireDamage,
            float holyDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            ProcessCharacterDamageFromServer(damagedCharacterID, characterCausingDamageID, physicalDamage, magicDamage, fireDamage, holyDamage, poiseDamage, angleHitFrom, contactPointX, contactPointY, contactPointZ);
        }

        public void ProcessCharacterDamageFromServer(
            ulong damagedCharacterID,
            ulong characterCausingDamageID,
            float physicalDamage,
            float magicDamage,
            float fireDamage,
            float holyDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            CharaterManager damagedCharacter = NetworkManager.Singleton.SpawnManager.SpawnedObjects[damagedCharacterID].gameObject.GetComponent<CharaterManager>();
            CharaterManager characterCauingDamage = NetworkManager.Singleton.SpawnManager.SpawnedObjects[characterCausingDamageID].gameObject.GetComponent<CharaterManager>();
            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);

            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.angleHitFrom = angleHitFrom;
            damageEffect.contactPoint = new Vector3(contactPointX, contactPointY, contactPointZ);
            damageEffect.characterCausingDamage = characterCauingDamage;

            damagedCharacter.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }
    }
}