using System.Collections;
using System.Collections.Generic;
using TopDownShooter.Inventory;
using UniRx;
using UnityEngine;

namespace TopDownShooter.Stat
{
    public class PlayerStat : IDamageble
    {
        public int Id { get; private set; }
        public bool IsLocalPlayer { get; private set; }

        public int InstanceId { get; private set; } = -1;

        public ReactiveProperty<float> Health = new ReactiveProperty<float>(100);
        public ReactiveProperty<float> Armor = new ReactiveProperty<float>(100);
        public ReactiveCommand OnDeath = new ReactiveCommand();
        private bool _isDead = false;


        public PlayerStat(int id, bool isLocalPlayer)
        {
            Id = id;
            IsLocalPlayer = isLocalPlayer;
            ScriptableStatManager.Instance.RegisterStat(this);
        }

        public void Damage(IDamage dmg)
        {
            if (Armor.Value > 0)
            {
                Armor.Value -= (dmg.Damage * dmg.ArmorPenetration);
            }
            else
            {
                Health.Value -= dmg.Damage;
                //basic if armor is negative means damage is much more than armor
                Health.Value += Armor.Value;
                CheckHealth();
            }
            MessageBroker.Default.Publish(new EventPlayerGiveDamage(dmg.Damage, this, dmg.Stat));
        }

        public void Damage(float dmg, PlayerStat shooter)
        {
            Debug.Log("DAMAGE RECEOVED : " + dmg + " shooter : " + shooter.Id);
            if (Armor.Value > 0)
            {
                Armor.Value -= (dmg * dmg);
            }
            else
            {
                Health.Value -= dmg;
                //basic if armor is negative means damage is much more than armor
                Health.Value += Armor.Value;
                CheckHealth();
            }
            MessageBroker.Default.Publish(new EventPlayerGiveDamage(dmg, this, shooter));
        }

        private void CheckHealth()
        {
            if (_isDead)
            {
                return;
            }
            if (Health.Value <= 0)
            {
                _isDead = true;
                Debug.Log("DEATH : " + Id);
                OnDeath.Execute();
            }
        }
    }
}