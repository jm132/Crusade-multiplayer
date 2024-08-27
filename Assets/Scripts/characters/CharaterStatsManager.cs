using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace JM
{
    public class CharaterStatsManager : MonoBehaviour
    {
        CharaterManager charater;

        [Header("Stamina Regeneration")]
        [SerializeField] float staminaRegenerationAmount = 2;
        private float staminaRegenerationTimer = 0;
        private float staminaTickTimer = 0;
        [SerializeField] float staminaRegenerationDelay = 2;

        [Header("Blocking Absorptions")]
        public float blockingPhysicalAbsorption;
        public float blockingFireAbsorption;
        public float blockingMagicAbsorption;
        public float blockingLightningAbsorption;
        public float blockingHolyAbsorption;
        public float blockingStability;

        [Header("Poise")]
        public float totalPoiseDamage; // how much poise damage the character has taken
        public float offensivePoiseBonus; // the poise bonus gained from using weapons
        public float basePoiseDefense; // the poise bonus gained from armor/talismans 
        public float defaultPoiseResetTime = 8; // the time it takes for poise damage to reset (must not be hit in the time or will reset)
        public float poiseResetTimer = 0; // the current timer for poise reset

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();
        }

        protected virtual void start()
        {

        }

        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }

        public int CalculateHealthBasedOnVitalityLevel(int vitality)
        {
            float health = 0;

            health = vitality * 15;

            return Mathf.RoundToInt(health);
        }

        public int CalculateStaminaBasedOnEnduranceLevel(int endurance)
        {
            float stamina = 0;

            stamina = endurance * 10;

            return Mathf.RoundToInt(stamina);
        }

        public virtual void RegenerateStamina()
        {
            // only owners can edit their network veraibles
            if (!charater.IsOwner)
                return;

            // do not want to regenerate stamina if stamina is being used
            if (charater.characterNetworkManager.isSprinting.Value)
                return;

            if (charater.isPerfromingAction)
                return;

            staminaRegenerationTimer += Time.deltaTime;

            if (staminaRegenerationTimer >= staminaRegenerationDelay)
            {
                if (charater.characterNetworkManager.currentStamina.Value < charater.characterNetworkManager.maxStamina.Value)
                {
                    staminaTickTimer += Time.deltaTime;

                    if (staminaTickTimer >= 0.1)
                    {
                        staminaTickTimer = 0;
                        charater.characterNetworkManager.currentStamina.Value += staminaRegenerationAmount;
                    }
                }
            }
        }

        public virtual void ResetStaminaRegenTimer(float previousStaminaAmount, float currentStaminaAmount)
        {
            // only want to reset regenation if the action used stamina
            // dont want to reset regenation if already regenating stamina
            if (currentStaminaAmount < previousStaminaAmount)
            {
                staminaRegenerationTimer = 0;
            }
        }

        protected virtual void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                totalPoiseDamage = 0;
            }
        }
    }
}
