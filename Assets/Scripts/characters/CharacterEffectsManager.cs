using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        // process instant effects (take Damage, heal)

        // process timed effects (poison, build ups)

        // process static effects (adding/removing buffs from talismans ect)

        CharaterManager charater;

        [Header("VFX")]
        [SerializeField] GameObject bloodSplatterVFX;

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProcessEffect(charater);
        }

        public void PlayBloodSplatterVFX(Vector3 contactPoint)
        {
            // if manually placed a blood splatter vfx on this model, play its version
            if (bloodSplatterVFX != null)
            {
                GameObject bloodSplatter = Instantiate(bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
            // else , use the generic (default version) that is stroed elsewhere
            else
            {
                GameObject bloodSplatter = Instantiate(WorldCharacterEffectManager.instance.bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
        }
    }
}
