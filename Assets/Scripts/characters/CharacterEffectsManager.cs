using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        [Header("Static Effects")]
        public List<StaticCharacterEffect> staticEffects = new List<StaticCharacterEffect>();

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

        public void AddStaticEffect(StaticCharacterEffect effect)
        {
            // 1. add a static effect to the characters
            staticEffects.Add(effect);

            // 2. process its effect
            effect.ProcessStaticEffect(charater);

            // 3. check for null entries in list and then remove them
            for (int i = staticEffects.Count - 1; i > -1; i--)
            {
                if (staticEffects[i] == null)
                    staticEffects.RemoveAt(i);
            }
        }

        public void RemoveStaticEffect(int effectID)
        {
            // 1. remove static effect from character
            StaticCharacterEffect effect;
            
            for (int i = 0; i < staticEffects.Count; i++)
            {
                if (staticEffects[i] != null)
                {
                    if (staticEffects[i].staticEffectID == effectID)
                    {
                        effect = staticEffects[i];
                        // 1. remove static effect from character
                        effect.RemoveStaticEffect(charater);
                        // 2. remove static effect from list
                        staticEffects.Remove(effect);
                    }
                }
            }

            // 3. check for null entries in list and then remove them
            for (int i = staticEffects.Count - 1; i > -1; i--)
            {
                if (staticEffects[i] == null)
                    staticEffects.RemoveAt(i);
            }
        }
    }
}
