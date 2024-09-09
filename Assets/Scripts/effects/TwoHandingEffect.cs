using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Character Effects/Static Effects/Two Handing Effect")]
    public class TwoHandingEffect : StaticCharacterEffect
    {
        [SerializeField] int strengthGainedFromTwoHandingWeapon;

        public override void ProcessStaticEffect(CharaterManager charater)
        {
            base.ProcessStaticEffect(charater);

            if (charater.IsOwner)
            {
                strengthGainedFromTwoHandingWeapon = Mathf.RoundToInt(charater.characterNetworkManager.strenght.Value / 2);
                Debug.Log("Strenght Gained: " + strengthGainedFromTwoHandingWeapon);
                charater.characterNetworkManager.strenghtModifier.Value += strengthGainedFromTwoHandingWeapon;
            }
        }

        public override void RemoveStaticEffect(CharaterManager charater)
        {
            base.RemoveStaticEffect(charater);

            if (charater.IsOwner)
            {
                charater.characterNetworkManager.strenghtModifier.Value -= strengthGainedFromTwoHandingWeapon;
            }
        }
    }
}
