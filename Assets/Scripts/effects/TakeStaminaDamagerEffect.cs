using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take stamina Damage")]
    public class TakeStaminaDamagerEffect : InstantCharacterEffect
    {
        public float staminaDamage;

        public override void ProcessEffect(CharaterManager charater)
        {
            CalculateStaminaDamage(charater);
        }

        private void CalculateStaminaDamage(CharaterManager charater)
        {
            if (charater.IsOwner)
            {
                Debug.Log("CHARACTER IS TAKEING" + staminaDamage + "STAMINA DAMAGE");
                charater.characterNetworkManager.currentStamina.Value -= staminaDamage;
            }
        }
    }
}
