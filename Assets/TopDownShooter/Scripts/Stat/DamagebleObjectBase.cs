using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.Stat
{
    public class DamagebleObjectBase : MonoBehaviour, IDamageble
    {
        [SerializeField] private Collider _collider;
        public int InstanceId { get; private set; }

        protected virtual void Awake()
        {
            InstanceId = _collider.GetInstanceID();
            this.InitializeDamageble();
        }

        public virtual void Damage(float dmg)
        {
            Debug.Log("you damaged me : " + dmg);
        }
    }
}