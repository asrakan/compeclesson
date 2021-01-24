using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using TopDownShooter.Inventory;

namespace TopDownShooter.Network
{
    public enum InGameNetworkState { NotReady, Ready }
    public class InGameNetworkController : Photon.PunBehaviour
    {
        [SerializeField] private NetworkPlayer _localPlayerPrefab;
        [SerializeField] private NetworkPlayer _remotePlayerPrefab;
        private InGameNetworkState _inGameNetworkState;
        private void Awake()
        {
            MessageBroker.Default.Receive<EventSceneLoaded>().Subscribe(OnSceneLoaded).AddTo(gameObject);
            MessageBroker.Default.Receive<EventPlayerShoot>().Subscribe(OnPlayerShoot).AddTo(gameObject);
        }


        private void OnSceneLoaded(EventSceneLoaded obj)
        {
            switch (obj.SceneName)
            {
                case "GameScene":
                    _inGameNetworkState = InGameNetworkState.Ready;
                    PhotonNetwork.isMessageQueueRunning = true;
                    InstantiateLocalPlayer();
                    break;
                default:
                    _inGameNetworkState = InGameNetworkState.NotReady;
                    break;
            }
        }



        private void OnPlayerShoot(EventPlayerShoot obj)
        {
            if (obj.ShooterId == PhotonNetwork.player.ID)
            {
                Shoot(obj.Origin);
            }
        }

        public void InstantiateLocalPlayer()
        {
            var instantiated = Instantiate(_localPlayerPrefab);
            int[] allocatedViewIdArray = new int[instantiated.PhotonViews.Length];
            for (int i = 0; i < allocatedViewIdArray.Length; i++)
            {
                allocatedViewIdArray[i] = PhotonNetwork.AllocateViewID();
            }

            instantiated.SetOwnership(PhotonNetwork.player, allocatedViewIdArray);
            photonView.RPC("RPC_InstantiateLocalPlayer", PhotonTargets.OthersBuffered, allocatedViewIdArray);
            PhotonNetwork.isMessageQueueRunning = true;
        }


        [PunRPC]
        public void RPC_InstantiateLocalPlayer(int[] viewIdArray, PhotonMessageInfo photonMessageInfo)
        {
            var instantiated = Instantiate(_remotePlayerPrefab);
            instantiated.SetOwnership(photonMessageInfo.sender, viewIdArray);
        }


        public void Shoot(Vector3 origin)
        {
            photonView.RPC("RPC_Shoot", PhotonTargets.Others, origin);
        }

        [PunRPC]
        public void RPC_Shoot(Vector3 origin, PhotonMessageInfo photonMessageInfo)
        {
            MessageBroker.Default.Publish(new EventPlayerShoot(origin,photonMessageInfo.sender.ID));
        }
    }
}