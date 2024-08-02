using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharaterManager characterCausingDamage; // (when calculating damage this is used to check for attackers damage modifiers, effects ect)

        [Header("Weapon Attack Modifiers")]
        public float light_Attack_01_Modifier;
        public float light_Attack_02_Modifier;
        public float heavy_Attack_01_Modifier;
        public float heavy_Attack_02_Modifier;
        public float charge_Attack_01_Modifier;
        public float charge_Attack_02_Modifier;

        protected override void Awake()
        {
            base.Awake();

            if (damageCollider == null)
            {
                damageCollider = GetComponent<Collider>();
            }
            damageCollider.enabled = false; // melle weapon colliders should be disabled at start, only enable when animations allow
        }

        protected override void OnTriggerEnter(Collider other)
        {
            CharaterManager damageTarget = other.GetComponentInParent<CharaterManager>();

            if (damageTarget != null)
            {
                // dont want to damage the player
                if (damageTarget == characterCausingDamage)
                    return;

                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // check if we can damage this traget based on friendly fire

                // check if target is blocking

                DamageTarget(damageTarget);
            }
        }

        protected override void DamageTarget(CharaterManager damageTarget)
        {
            // don't want to damage the same target more than once in a single attack
            // add them to a list then checks before applying damage
            if (charactersDamaged.Contains(damageTarget))
                return;

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.contactPoint = contactPoint;
            damageEffect.angleHitFrom = Vector3.SignedAngle(characterCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);

            switch (characterCausingDamage.characterCombatManager.currentAttackType)
            {
                case AttackType.LightAttack01:
                    ApplyAttackDameageModifiers(light_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.LightAttack02:
                    ApplyAttackDameageModifiers(light_Attack_02_Modifier, damageEffect);
                    break;
                case AttackType.HeavyAttack01:
                    ApplyAttackDameageModifiers(heavy_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.HeavyAttack02:
                    ApplyAttackDameageModifiers(heavy_Attack_02_Modifier, damageEffect);
                    break;
                case AttackType.ChargedAttack01:
                    ApplyAttackDameageModifiers(charge_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.ChargedAttack02:
                    ApplyAttackDameageModifiers(charge_Attack_02_Modifier, damageEffect);
                    break;
                default:
                    break;
            }

            //damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

            if (characterCausingDamage.IsOwner)
            {
                damageTarget.characterNetworkManager.NotifyTheServerOfCharacterDamageServerRpc(
                    damageTarget.NetworkObjectId, 
                    characterCausingDamage.NetworkObjectId,
                    damageEffect.physicalDamage,
                    damageEffect.magicDamage,
                    damageEffect.fireDamage,
                    damageEffect.holyDamage,
                    damageEffect.poiseDamage,
                    damageEffect.angleHitFrom,
                    damageEffect.contactPoint.x,
                    damageEffect.contactPoint.y,
                    damageEffect.contactPoint.z);
            }
        }

        private void ApplyAttackDameageModifiers(float modifier, TakeDamageEffect damage)
        {
            damage.physicalDamage *= modifier;
            damage.magicDamage *= modifier;
            damage.fireDamage *= modifier;
            damage.holyDamage *= modifier;
            damage.poiseDamage *= modifier;

            // if attack is a fully charged heavy, multiply by full charge modifier after normal modifier have been calculated 
        }
    }
}