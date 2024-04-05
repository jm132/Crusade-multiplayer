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

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();
        }

        protected virtual void start()
        {

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
    }
}
