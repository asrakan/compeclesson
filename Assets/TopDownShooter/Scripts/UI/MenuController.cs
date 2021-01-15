using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TopDownShooter.Network;
using System;
using TMPro;
using UnityEngine.UI;

namespace TopDownShooter.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentState;
        [SerializeField] private Button[] _networkButtons;
        [SerializeField] private TMP_InputField _inputField;
        private void Awake()
        {
            MessageBroker.Default.Receive<EventPlayerNetworkStateChange>().Subscribe(OnPlayerNetworkState).AddTo(gameObject);
            _inputField.onEndEdit.AddListener(OnEditEnd);
        }

        private void OnEditEnd(string arg0)
        {
            PhotonNetwork.playerName = arg0;
        }

        private void OnPlayerNetworkState(EventPlayerNetworkStateChange obj)
        {
            _currentState.text = "Connetion state: " + obj.PlayerNetworkState.ToString();
            for (int i = 0; i < _networkButtons.Length; i++)
            {
                _networkButtons[i].interactable = obj.PlayerNetworkState == PlayerNetworkState.Connected;
            }
            //_currentState.color = Color.green;
        }

        public void _CreateRoomClick()
        {
            MatchmakingController.Instance.CreateRoom();
        }

        public void _JoinRandomRoomClick()
        {
            MatchmakingController.Instance.JoinRandomRoom();
        }

        public void _SettingsClick()
        {
            Debug.LogError("not ready yet");
        }
    }
}