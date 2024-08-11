using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AIBossSoundFXManager : CharaterSoundFXManager
    {
        [Header("boss weapon whooshes")]
        public AudioClip[] weaponWhooshes;

        [Header("boss weapon Inpacts")]
        public AudioClip[] weaponInpacts;

        [Header("boss Stomp Inpacts")]
        public AudioClip[] StompInpacts;

        public virtual void PlayWeaponIMpactSoundFX()
        {
            if (weaponInpacts.Length > 0)
                PlaySoundFX(WorldSoundFXManager.Instance.ChooseRandomSFXFromArray(weaponInpacts));
        }
        public virtual void PlayStompInpactsSoundFX()
        {
            if (StompInpacts.Length > 0)
                PlaySoundFX(WorldSoundFXManager.Instance.ChooseRandomSFXFromArray(StompInpacts));
        }
    }
}
