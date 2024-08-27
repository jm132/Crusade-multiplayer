using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Blocked Damage")]
    public class TakeBlockDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Damage")]
        public CharaterManager characterCausingDamage; // if the damage is caused by another characacters attack it will be stored here

        [Header("Damage")]
        public float physicalDamage = 0;  // (will be maing this into "standerd", "Strike", "Slash" and "Pierca"
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Final Damage")]
        private int finalDamageDealt = 0; // the damage the chatacter takes after all calculations have been made

        [Header("Poise")]
        public float poiseDamage = 0;
        public bool poiseIsBroken = false; // if a character's poise is broken, they will be "stunned" and play a damage animation

        [Header("Stanina")]
        public float StaminaDamage = 0;
        public float finalStaminaDamage = 0;

        //(to do) build ups
        // build up effects amounts

        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;
        public AudioClip elementalDamageSoundFX; // used on top of regular sfx if there is elemental damage present (magic/fire/lightning/holy)

        [Header("Direction Damafe Taken From")]
        public float angleHitFrom; // used to determine what damage animation to play (move backwards, to the left, to the right ect)
        public Vector3 contactPoint;// used to determine where the blood fx instantiate

        public override void ProcessEffect(CharaterManager charater)
        {
            base.ProcessEffect(charater);

            Debug.Log("hit was blocked!");

            if (charater.characterNetworkManager.isInvulnerable.Value)
                return;

            // if the character is dead, no additional damafe effects should be processed
            if (charater.isDead.Value)
                return;

            // check for "invulnerability"

            CalaulateDamage(charater);
            CalculateStaminaDamage(charater);
            PlayDirectionalBasedBlockingDamageAnimation(charater);
            // play a damage animation 
            // check for build ups (poison, bleed ect)
            PlayDamageSFX(charater);
            PlayDamageVFX(charater);

            // if character is A.I, check for new target if character causing damage is persent

            CheckForGuardBreak(charater);
        }

        private void CalaulateDamage(CharaterManager charater)
        {
            if (!charater.IsOwner)
                return;

            if (characterCausingDamage != null)
            {
                // check for damage modifiers and modify base damage (physical damage buff, elemental damage buff ect)
            }

            // check character for flat defenses and subtract them from damage

            // check character for armor absorptions, and subtract the percantage from damage

            // add all damage types together, and apply final damage

            physicalDamage -= (physicalDamage * (charater.characterStatsManager.blockingPhysicalAbsorption / 100));
            magicDamage -= (magicDamage * (charater.characterStatsManager.blockingMagicAbsorption / 100));
            fireDamage -= (fireDamage * (charater.characterStatsManager.blockingFireAbsorption / 100));
            lightningDamage -= (lightningDamage * (charater.characterStatsManager.blockingLightningAbsorption / 100));
            holyDamage -= (holyDamage * (charater.characterStatsManager.blockingHolyAbsorption / 100));

            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            charater.characterNetworkManager.currentHealth.Value -= finalDamageDealt;
        }

        private void CalculateStaminaDamage(CharaterManager charater)
        {
            if (!charater.IsOwner)
                return;

            finalStaminaDamage = StaminaDamage;

            float staminaDamageAbsorption = finalStaminaDamage * (charater.characterStatsManager.blockingStability / 100);
            float staminaDamageAfterAbsorption = finalStaminaDamage - staminaDamageAbsorption;

            charater.characterNetworkManager.currentStamina.Value -= staminaDamageAfterAbsorption;
        }

        private void CheckForGuardBreak(CharaterManager charater)
        {
            if (!charater.IsOwner)
                return;

            if (charater.characterNetworkManager.currentStamina.Value <= 0)
            {
                charater.characterAnimatorManager.PlayTargetActionAnimation("Guard_Break_01", true);
                charater.characterNetworkManager.isBlocking.Value = false;
            }
        }

        private void PlayDamageVFX(CharaterManager charater)
        {
            // if fire damage, play fire particales
            // if lightning damage, play lightning particales ect

            charater.characterSoundFXManager.PlayBlockSoundFX();
        }

        private void PlayDamageSFX(CharaterManager charater)
        {
            // if fire damage is greater then 0, play burn sfx
            // if lightning damage is greater then 0, play zap sfx
        }

        private void PlayDirectionalBasedBlockingDamageAnimation(CharaterManager charater)
        {
            if (!charater.IsOwner)
                return;

            if (charater.isDead.Value)
                return;

            DamageIntensity damageIntensity = WorldUtilityManager.instance.GetDamageIntensityBasedOnPoiseDamage(poiseDamage);

            switch (damageIntensity)
            {
                case DamageIntensity.Ping:
                    damageAnimation = "Block_Ping_01";
                    break;
                case DamageIntensity.Light:
                    damageAnimation = "Block_light_01";
                    break;
                case DamageIntensity.Medium:
                    damageAnimation = "Block_Medium_01";
                    break;
                case DamageIntensity.Heavy:
                    damageAnimation = "Block_Heavy_01";
                    break;
                case DamageIntensity.Colossal:
                    damageAnimation = "Block_Colossal_01";
                    break;
                default:
                    break;
            }

            // if poise is broken, play a staggering damage animation
            charater.characterAnimatorManager.lastDamageAnimationPlayed = damageAnimation;
            charater.characterAnimatorManager.PlayTargetActionAnimation(damageAnimation, true);
        }
    }
}
