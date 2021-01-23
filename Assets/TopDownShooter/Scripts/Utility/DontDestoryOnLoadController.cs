using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.Utility
{
    public class DontDestoryOnLoadController : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}