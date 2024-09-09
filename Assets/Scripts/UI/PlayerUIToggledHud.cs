using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerUIToggledHud : MonoBehaviour
    {
        private void OnEnable()
        {
            // hise the hud
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(false);
        }

        private void OnDisable()
        {
            // bring the hud back
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(true);
        }
    }
}
