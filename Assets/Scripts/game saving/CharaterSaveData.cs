using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [System.Serializable]
    // reference this date for every save file
    public class CharaterSaveData
    {
        [Header("SCENE INDEX")]
        public int sceneIndex = 1;

        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Time Played")]
        public string secondsPlayed;

        [Header("World Coordinates")]
        public float xPosition;
        public float yPosition;
        public float zPosition;

        [Header("Resources")]
        public int currentHealth;
        public float currentStamina;

        [Header("Stats")]
        public int vitality;
        public int endurance;

        [Header("Sites Of Grace")]
        public SerializableDictionary<int, bool> sitesOfGrace;  // the int is the site of grace I.D, the bool is the "activated" status

        [Header("Bosses")]
        public SerializableDictionary<int, bool> bossesAwakened; // the int is the boss I.D, the bool is the awwkened status
        public SerializableDictionary<int, bool> bossesDefeated; // the int is the boss I.D, the bool is the defeated status

        public CharaterSaveData()
        {
            sitesOfGrace = new SerializableDictionary<int, bool>();
            bossesAwakened = new SerializableDictionary<int, bool>();
            bossesDefeated = new SerializableDictionary<int, bool>();
        }
    }
}
