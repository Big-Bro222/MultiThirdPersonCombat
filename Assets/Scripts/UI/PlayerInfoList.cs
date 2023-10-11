using System;
using System.Collections.Generic;
using BigBro.Utility;
using UnityEngine;

namespace BigBro.UI
{
    public class PlayerInfoList : MonoBehaviour
    {
        [SerializeField] private PlayerInfoListItem _playerInfoListItemPrefab;
        private List<Player> _players=new List<Player>();
        private Dictionary<Player, PlayerInfoListItem> _playerInfoListItemsDic = new Dictionary<Player, PlayerInfoListItem>();
        public void Init(List<Player> players)
        {
            _players = players;
            foreach (var player in players)
            {
                 //creat playerinfo item
                 AddPlayerItem(player, item =>
                 {
                     _playerInfoListItemsDic.Add(player,item);
                     RectTransform rt = (RectTransform) item.transform;
                     rt.SetParent(transform);
                     item.Setup(player.PlayerData,player.IsLocalPlayer);
                 });
            }
        }

        public void Clear()
        {
            _players = new List<Player>();
            _playerInfoListItemsDic = new Dictionary<Player, PlayerInfoListItem>();
        }

        public void UpdateInfo(Player player,bool isPlayerEnter)
        {
            if (isPlayerEnter)
            {
                AddPlayerItem(player, item =>
                {
                    _playerInfoListItemsDic.Add(player,item);
                    RectTransform rt = (RectTransform) item.transform;
                    rt.SetParent(transform);
                    item.Setup(player.PlayerData,player.IsLocalPlayer);
                });
            }
            else
            {
                RemovePlayerItem(player, item =>
                {
                    _playerInfoListItemsDic.Remove(player);
                    ObjectPool.Recycle(item);
                });
            }
        }

        private void RemovePlayerItem(Player player,Action<PlayerInfoListItem> recycle)
        {
             _playerInfoListItemsDic.TryGetValue(player, out PlayerInfoListItem item);
             recycle(item);
        }
        
        private void AddPlayerItem(Player player,Action<PlayerInfoListItem> setup)
        {
            PlayerInfoListItem item = PooledObject.Instantiate(_playerInfoListItemPrefab);
            setup(item);
        }

    }
}
