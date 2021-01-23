using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace TopDownShooter.Inventory
{
    [CreateAssetMenu(menuName = "Topdown Shooter/Inventory/Scriptable Effect Manager")]
    public class ScriptableEffectManager : AbstractScriptableManager<ScriptableEffectManager>
    {
        [SerializeField] private GameObject _playerShootEffect;
        public override void Initialize()
        {
            base.Initialize();
            MessageBroker.Default.Receive<EventPlayerShoot>().Subscribe(OnPlayerShoot).AddTo(_compositeDisposable);
        }

        private void OnPlayerShoot(EventPlayerShoot obj)
        {
            Instantiate(_playerShootEffect, obj.Origin, Quaternion.identity);
        }
    }
}