using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

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
            Debug.Log("Instantiate player " + photonMessageInfo.sender.name);
            var instantaited = Instantiate(_remotePlayerPrefab);
            instantaited.SetOwnership(photonMessageInfo.sender, viewIdArray);
        }
    }
}