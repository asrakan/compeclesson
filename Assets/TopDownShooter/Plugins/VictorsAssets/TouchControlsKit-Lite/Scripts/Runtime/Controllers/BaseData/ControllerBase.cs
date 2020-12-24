/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using UnityEngine;
using UnityEngine.UI;

namespace TouchControlsKit
{
    // ControllerBase
    [DisallowMultipleComponent]
    [RequireComponent( typeof( Image ) )]
    public abstract class ControllerBase : MonoBehaviour
    {
        public EUpdateMode updateMode = EUpdateMode.Normal;

        internal ETouchPhase touchPhase = ETouchPhase.NoTouch;

        public RectTransform baseRect = null;
        public Image baseImage = null;

        public string identifier = "NONAME_Controller";

        [SerializeField]
        protected Color32 baseImageColor = new Color32( 255, 255, 255, 165 );

        protected int touchId = -1;
        protected bool touchDown;

        [SerializeField]
        protected bool 
            enable = true
            , active = true
            , visible = true;

        private float touchPosMag, prevTouchPosMag;


        // GetActiveColor
        protected Color GetActiveColor( Color color )
        {
            return ( active ? color : color * .5f );
        }


        // isEnable
        public bool isEnable
        {
            get { return enable; }
            set
            {
                if( enable == value )
                    return;

                enable = value;
                OnApplyEnable();
            }
        }
        // Apply Enable
        [ApplyMethod]
        protected virtual void OnApplyEnable()
        {
            enabled = ( enable && active );
            baseImage.enabled = enable;
        }

        // Active
        public bool isActive
        {
            get { return active; }
            set
            {
                if( active == value )
                    return;

                active = value;
                OnApplyActive();
            }
        }
        // Apply Active
        [ApplyMethod]
        protected virtual void OnApplyActive()
        {
            enabled = ( enable && active );
            
            if( visible ) {
                OnApplyActiveColors();
            }
        }

        // OnApply ActiveColors
        protected virtual void OnApplyActiveColors()
        {
            baseImage.color = GetActiveColor( baseImageColor );
        }


        // Visible
        public bool isVisible
        {
            get { return visible; }
            set
            {
                if( visible == value )
                    return;

                visible = value;
                OnApplyVisible();
            }
        }
        // Apply Visible
        [ApplyMethod]
        protected virtual void OnApplyVisible()
        {
            baseImage.color = visible ? GetActiveColor( baseImageColor ) : Color.clear;
        }


        // OnAwake
        public virtual void OnAwake()
        {
            baseImage = GetComponent<Image>();
            baseRect = baseImage.rectTransform;

            OnApplyActive();           
        }


        // Update
        protected virtual void Update()
        {           
            if( updateMode == EUpdateMode.Normal ) {
                UpdateTouchPhase();
            }                
        }
        // Late Update
        protected virtual void LateUpdate()
        {
            if( updateMode == EUpdateMode.Late ) {
                UpdateTouchPhase();
            }                
        }
        // Fixed Update
        protected virtual void FixedUpdate()
        {
            if( updateMode == EUpdateMode.Fixed ) {
                UpdateTouchPhase();
            }                
        }


        // OnDisable
        void OnDisable()
        {
            if( Application.isPlaying && touchDown ) {
                ControlReset();
            }
        }


        
        // Update TouchPhase
        private void UpdateTouchPhase()
        {
            if( touchDown )
            {
                if( touchPosMag == prevTouchPosMag )
                    touchPhase = ETouchPhase.Stationary;
                else
                    touchPhase = ETouchPhase.Moved;                
            }
            else
            {
                touchPhase = ETouchPhase.NoTouch;
            }

            prevTouchPosMag = touchPosMag;
        }


        // Update Position
        protected virtual void UpdatePosition( Vector2 touchPos )
        {
            touchPosMag = touchPos.magnitude;
        }

        // Control Reset
        protected virtual void ControlReset()
        {
            touchPhase = ETouchPhase.Ended;
            touchId = -1;
            touchDown = false;
        }
    };
}