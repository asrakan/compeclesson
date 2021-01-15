using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.Network
{
    public class NetworkPlayer : Photon.PunBehaviour
    {
        [SerializeField] private PhotonView[] _photonViewsForOwnership;
        public void SetOwnership(PhotonPlayer photonPlayer)
        {
            Debug.Log("Set ownership for : " + photonPlayer.name);
            for (int i = 0; i < _photonViewsForOwnership.Length; i++)
            {
                _photonViewsForOwnership[i].TransferOwnership(photonPlayer);
            }
        }
    }
}