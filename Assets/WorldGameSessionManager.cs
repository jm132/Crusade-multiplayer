using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class WorldGameSessionManager : MonoBehaviour
    {
        public static WorldGameSessionManager instance;

        [Header("Active Player in Session")]
        public List<PlayerManager> players = new List<PlayerManager>();

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void AddPlayerToActivePlayersList(PlayerManager player)
        {
            // check the list, if it dose not already contain the player, add them
            if (!players.Contains(player))
            {
                players.Add(player);
            }

            // check the list for null slot, and remove the null slot
            for (int i = players.Count - 1; i > -1; i--)
            {
                if (players[i] == null)
                {
                    players.RemoveAt(i);
                }
            }
        }

        public void RemovePlayerFromActivePlayersList(PlayerManager player)
        {
            // check the list, if it dose already contain the player, remove them
            if (players.Contains(player))
            {
                players.Remove(player);
            }

            // check the list for null slot, and remove the null slot
            for (int i = players.Count - 1; i > -1; i--)
            {
                if (players[i] == player)
                {
                    players.RemoveAt(i);
                }
            }
        }
    }
}
