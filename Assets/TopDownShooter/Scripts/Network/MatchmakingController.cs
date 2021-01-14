using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
namespace TopDownShooter.Network
{
    public enum PlayerNetworkState { Offline, Connecting, Connected, InRoom }
    public class MatchmakingController : Photon.PunBehaviour
    {
        [SerializeField] private float _delayToConnect = 3;
        public static MatchmakingController Instance;
        private const string _networkVersion = "v1.0";
        private void Awake()
        {
            Instance = this;
        }

        private IEnumerator Start()
        {
            MessageBroker.Default.Publish(new EventPlayerNetworkStateChange(PlayerNetworkState.Offline));
            yield return new WaitForSeconds(_delayToConnect);
            MessageBroker.Default.Publish(new EventPlayerNetworkStateChange(PlayerNetworkState.Connecting));
            PhotonNetwork.ConnectUsingSettings(_networkVersion);
        }


        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(null);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            MessageBroker.Default.Publish(new EventPlayerNetworkStateChange(PlayerNetworkState.InRoom));
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            MessageBroker.Default.Publish(new EventPlayerNetworkStateChange(PlayerNetworkState.Connected));
        }

        public override void OnDisconnectedFromPhoton()
        {
            base.OnDisconnectedFromPhoton();
            MessageBroker.Default.Publish(new EventPlayerNetworkStateChange(PlayerNetworkState.Offline));
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            MessageBroker.Default.Publish(new EventPlayerNetworkStateChange(PlayerNetworkState.Connected));
            Debug.Log("ON CONNECTED TO MASTER");
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            Debug.Log("ON JOINED LOBBY");
        }

    }
}