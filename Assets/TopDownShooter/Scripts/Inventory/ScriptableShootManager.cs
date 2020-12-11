using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.Inventory
{
    [CreateAssetMenu(menuName = "Topdown Shooter/Inventory/ScriptableShootManager")]
    public class ScriptableShootManager : AbstractScriptableManager<ScriptableShootManager>
    {
        public override void Initialize()
        {
            base.Initialize();
            Debug.Log("scriptable shoot manager instantiated");
        }

        public override void Destroy()
        {
            base.Destroy();
            Debug.Log("scriptable shoot manager destroyed");
        }

        public void Shoot()
        {
            
        }
    }
}