using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownShooter.Network;
using TopDownShooter.Inventory;
using NetworkPlayer = TopDownShooter.Network.NetworkPlayer;
using UniRx;
using System;
using TopDownShooter.Stat;

namespace TopDownShooter
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private DamagebleObjectBase[] _damagebleObjectBases;
        [SerializeField] private NetworkPlayer _networkPlayer;
        [SerializeField] protected PlayerInventoryController _inventoryController;
        protected void Start()
        {
            _networkPlayer.RegisterStatHolder(_inventoryController);
            _networkPlayer.PlayerStat.OnDeath.Subscribe(OnDeath).AddTo(gameObject);
            for (int i = 0; i < _damagebleObjectBases.Length; i++)
            {
                _networkPlayer.RegisterStatHolder(_damagebleObjectBases[i]);
            }
        }

        private void OnDeath(Unit obj)
        {
            if (_networkPlayer.IsLocalPlayer)
            {
                MatchmakingController.Instance.LeaveRoom();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}