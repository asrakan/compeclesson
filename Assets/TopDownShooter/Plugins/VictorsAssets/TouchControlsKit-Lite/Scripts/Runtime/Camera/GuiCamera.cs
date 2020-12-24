/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine;

namespace TouchControlsKit
{
    [RequireComponent( typeof( Camera ) )]
    public sealed class GuiCamera : MonoBehaviour
    {
        public static Camera getCamera { get; private set; }
        public static Transform getTransform { get; private set; }


        // Awake
        void Awake()
        {
            getTransform = transform;
            getCamera = GetComponent<Camera>();
        }                 

        // ScreenToWorldPoint
        public static Vector2 ScreenToWorldPoint( Vector2 position )
        {
            return getCamera.ScreenToWorldPoint( position );
        }
    };
}