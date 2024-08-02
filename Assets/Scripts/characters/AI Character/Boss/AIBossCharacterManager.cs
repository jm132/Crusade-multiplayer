using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class AIBossCharacterManager : AICharacterManager
    {
        public int bossID = 0;
        [SerializeField] bool hasBeenDefeated = false;
        [SerializeField] bool hasBeenAwakened = false;
        [SerializeField] List<FogWallInteractable> fogWalls;

        [Header("debug")]
        [SerializeField] bool wakeBossUp = false;

        protected override void Update()
        {
            base.Update();

            if (wakeBossUp)
            {
                wakeBossUp = false;
                WakeBoss();
            }
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            if (IsServer)
            {
                // if save date dose not contain information on this boss, add it now
                if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, false);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, false);
                }
                // otherwise, load the data that already exists on this boss
                else
                {
                    hasBeenDefeated = WorldSaveGameManager.instance.currentCharacterData.bossesDefeated[bossID];
                    hasBeenAwakened = WorldSaveGameManager.instance.currentCharacterData.bossesAwakened[bossID];
                }

                // loacte fog wall
                StartCoroutine(GetFogWallSFromWorldObjectManager());

                // if the boss has been awakened, enable the fog walls
                if (hasBeenAwakened)
                {
                    for (int i = 0; i < fogWalls.Count; i++)
                    {
                        fogWalls[i].isActive.Value = true;
                    }
                }

                // if the boss has been defeated, disable the fog walls
                if (hasBeenDefeated)
                {
                    for (int i = 0; i < fogWalls.Count; i++)
                    {
                        fogWalls[i].isActive.Value = false ;
                    }

                    aiCharacterNetworkManager.isActive.Value = false;
                }
            }
        }

        private IEnumerator GetFogWallSFromWorldObjectManager()
        {
            {
                while (WorldObjectManager.instance == null || WorldObjectManager.instance.fogWalls == null || WorldObjectManager.instance.fogWalls.Count == 0)
                {
                    yield return new WaitForEndOfFrame();
                }

                fogWalls = new List<FogWallInteractable>();

                foreach (var fogWall in WorldObjectManager.instance.fogWalls)
                {
                    if (fogWall.fogWallID == bossID)
                    {
                        fogWalls.Add(fogWall);
                    }
                }
            }
        }

        public override IEnumerator ProessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (IsOwner)
            {
                characterNetworkManager.currentHealth.Value = 0;
                isDead.Value = true;

                // reset any falgs here that need to be reset
                // nothing yet

                // if not grounded, play an aerial death animation
                if (!manuallySelectDeathAnimation)
                {
                    characterAnimatorManager.PlayTargetActionAnimation("Dead_01", true);
                }

                hasBeenDefeated = true;

                // if save date dose not contain information on this boss, add it now
                if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);
                }
                // otherwise, load the data that already exists on this boss
                else
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Remove(bossID);
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);
                }

                WorldSaveGameManager.instance.SaveGame();
            }

            // play some death sfx

            yield return new WaitForSeconds(5);

            // award players with runes

            // disable character
        }

        public void WakeBoss()
        {
            hasBeenAwakened = true;
            if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
            {
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
            }
            else
            {
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
            }

            for (int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].isActive.Value = true;
            }
        }
    }
}
