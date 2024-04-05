using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace JM
{
    public class PlayerStatsManager : CharaterStatsManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void start()
        {
            base.start();

            // why calaculate these here?
            // when making a character creation menu, and set the stats depending on the class, this well be calculated there
            // until then however, stats are never calculated, so we do it here on start, if a save file exists then will be over written when loading into a scene
            CalculateHealthBasedOnVitalityLevel(player.playerNetworkManager.vitality.Value);
            CalculateStaminaBasedOnEnduranceLevel(player.playerNetworkManager.endurance.Value);
        }
    }
}