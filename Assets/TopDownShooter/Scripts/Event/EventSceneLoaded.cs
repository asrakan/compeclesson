using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter
{
    public struct EventSceneLoaded
    {
        public string SceneName;
        public EventSceneLoaded(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}