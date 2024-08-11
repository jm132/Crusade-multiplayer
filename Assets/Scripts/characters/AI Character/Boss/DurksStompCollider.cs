using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class DurksStompCollider : DamageCollider
    {
        [SerializeField] AIDerkCharacterManager durkCharacterManager;

        protected override void Awake()
        {
            base.Awake();

            durkCharacterManager = GetComponentInParent<AIDerkCharacterManager>();
        }

        public void StompAttack()
        {
            GameObject stompVFX = Instantiate(durkCharacterManager.durkCombatManager.durkInpactVFX, transform);

            Collider[] colliders = Physics.OverlapSphere(transform.position, durkCharacterManager.durkCombatManager.stompAttackAOERadius, WorldUtilityManager.instance.GetCharacterLayer());
            List<CharaterManager> charactersDamaged = new List<CharaterManager>();

            foreach (var collider in colliders)
            {
                CharaterManager charater = collider.GetComponentInParent<CharaterManager>();

                if (charater != null)
                {
                    if (charactersDamaged.Contains(charater))
                        continue;

                    // stop the boss from hurting it self when stomping
                    if (charater == durkCharacterManager)
                        continue;

                    charactersDamaged.Add(charater);

                    // only process damage if the character "isowner" so that the player get damaged if the collider connects on the client
                    // meaning if the player is hit on the hosts screen but not on player screen, player will not be hit
                    if (charater.IsOwner)
                    {
                        // chech for block
                        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
                        damageEffect.physicalDamage = durkCharacterManager.durkCombatManager.stompDamage;
                        damageEffect.poiseDamage = durkCharacterManager.durkCombatManager.stompDamage;

                        charater.characterEffectsManager.ProcessInstantEffect(damageEffect);
                    }
                }
            }
        }
    }
}
