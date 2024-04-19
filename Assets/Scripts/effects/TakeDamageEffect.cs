using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
    public class TakeDamageEffect : InstantCharacterEffect
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

            // if the character is dead, no additional damafe effects should be processed
            if (charater.isDead.Value)
                return;

            // check for "invulnerability"

            CalaulateDamage(charater);
            PlayDirectionalBasedDamageAnimation(charater); 
            // play a damage animation 
            // check for build ups (poison, bleed ect)
            PlayDamageSFX(charater);
            PlayDamageVFX(charater);

            // if character is A.I, check for new target if character causing damage is persent
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
            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            Debug.Log("final Damage Given: " + finalDamageDealt);
            charater.characterNetworkManager.currentHealth.Value -= finalDamageDealt;

            // calculate poise damage to determine if the character will be stunned
        }

        private void PlayDamageVFX(CharaterManager charater)
        {
            // if fire damage, play fire particales
            // if lightning damage, play lightning particales ect

            charater.characterEffectsManager.PlayBloodSplatterVFX(contactPoint);
        }

        private void PlayDamageSFX(CharaterManager charater)
        {
            AudioClip physicalDamageSFX = WorldSoundFXManager.Instance.ChooseRandomSFXFromArray(WorldSoundFXManager.Instance.physicalDamageSFX);

            charater.characterSoundFXManager.PlayScoundFX(physicalDamageSFX);
            // if fire damage is greater then 0, play burn sfx
            // if lightning damage is greater then 0, play zap sfx
        }

        private void PlayDirectionalBasedDamageAnimation(CharaterManager charater)
        {
            if (!charater.IsOwner)
                return;
            // todo calculate if poise is broken 
            poiseIsBroken = true;

            if (angleHitFrom >= 145 && angleHitFrom <= 180)
            {
                damageAnimation = charater.characterAnimatorManager.GetRandomAnimationFromList(charater.characterAnimatorManager.forward_Medium_Damage);
            }
            else if (angleHitFrom <= -145 && angleHitFrom >= -180)
            {
                damageAnimation = charater.characterAnimatorManager.GetRandomAnimationFromList(charater.characterAnimatorManager.forward_Medium_Damage);
            }
            else if (angleHitFrom >= -45 && angleHitFrom <= 45)
            {
                damageAnimation = charater.characterAnimatorManager.GetRandomAnimationFromList(charater.characterAnimatorManager.backward_Medium_Damage);
            }
            else if (angleHitFrom >= -144 && angleHitFrom <= -45)
            {
                damageAnimation = charater.characterAnimatorManager.GetRandomAnimationFromList(charater.characterAnimatorManager.left_Medium_Damage);
            }
            else if (angleHitFrom >= 45 && angleHitFrom <= 144)
            {
                damageAnimation = charater.characterAnimatorManager.GetRandomAnimationFromList(charater.characterAnimatorManager.right_Medium_Damage);
            }

            // if poise is broken, play a staggering damage animation
            if (poiseIsBroken)
            {
                charater.characterAnimatorManager.lastDamageAnimationPlayed = damageAnimation;
                charater.characterAnimatorManager.PlayTargetActionAnimation(damageAnimation, true);
            }
        }
    }
}
