/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TouchControlsKit
{
    /// <summary>
    /// isStatic = true;  - Switches a joystick in a static mode, in which it is only at the specified position.
    /// isStatic = false; - Switches a joystick in the dynamic mode, in this mode, it operates within the touch zone.
    /// </summary>

    public class TCKJoystick : AxesBasedController,
        IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        public Image joystickImage, backgroundImage;
        public RectTransform joystickRT, backgroundRT;
        
        [SerializeField, Label( "Mode" )]
        private bool isStatic = true;
                
        [Range( 1f, 9f )]
        public float borderSize = 5.85f;

        [LogicLabel( "isStatic", "Smooth Return", "Fadeout" )]
        public bool smoothReturn = false;

        [Range( 1f, 20f )]
        public float smoothFactor = 7f;

        [SerializeField]
        private Color32 joystickImageColor = new Color32( 255, 255, 255, 165 )
            , backgroundImageColor = new Color32( 255, 255, 255, 165 );


        // Joystick Mode
        public bool IsStatic
        {
            get { return isStatic; }
            set
            {
                if( isStatic == value ) return;
                isStatic = value;
            }
        }

        
        // Control Awake
        public override void OnAwake()
        {
            backgroundRT = transform.GetChild( 0 ) as RectTransform;
            joystickRT = backgroundRT.GetChild( 0 ) as RectTransform;

            backgroundImage = backgroundRT.GetComponent<Image>();
            joystickImage = joystickRT.GetComponent<Image>();

            if( Application.isPlaying ) {
                joystickImage.enabled = backgroundImage.enabled = isStatic;
            }

            base.OnAwake();
        }

        // Refresh Enable
        protected override void OnApplyEnable()
        {
            base.OnApplyEnable();
            backgroundImage.enabled = joystickImage.enabled = enable;
        }


        // OnRefresh ActiveColors
        protected override void OnApplyActiveColors()
        {
            base.OnApplyActiveColors();
            joystickImage.color = GetActiveColor( joystickImageColor );
            backgroundImage.color = GetActiveColor( backgroundImageColor );
        }


        // Refresh Visible
        protected override void OnApplyVisible()
        {
            base.OnApplyVisible();
            joystickImage.color = visible ? GetActiveColor( joystickImageColor ) : Color.clear;
            backgroundImage.color = visible ? GetActiveColor( backgroundImageColor ) : Color.clear;
        }

        // Update Position
        protected override void UpdatePosition( Vector2 touchPos )
        {
            if( !axisX.enabled && !axisY.enabled )
                return;

            base.UpdatePosition( touchPos );

            if( touchDown )
            {
                UpdateCurrentPosition( touchPos );

                currentDirection = currentPosition - defaultPosition;

                float currentDistance = Vector2.Distance( defaultPosition, currentPosition );
                float touchForce = 100f;

                float calculatedBorderSize = ( backgroundRT.sizeDelta.magnitude / 2f ) * borderSize / 16f;
                
                if( currentDistance > calculatedBorderSize ) { // borderPosition 
                    currentPosition = defaultPosition + currentDirection.normalized * calculatedBorderSize;
                }                                    
                else {
                    touchForce = ( currentDistance / calculatedBorderSize ) * 100f;
                }                    

                UpdateJoystickPosition();
                SetAxes( currentDirection.normalized * touchForce / 100f * sensitivity );
            }
            else
            {
                touchDown = true;
                touchPhase = ETouchPhase.Began;

                if( isStatic == false ) {
                    UpdateTransparencyAndPosition( touchPos );
                }                    

                UpdateCurrentPosition( touchPos );
                UpdatePosition( touchPos );
                ResetAxes();
            }
        }

        // Get CurrentPosition
        private void UpdateCurrentPosition( Vector2 touchPos )
        {
            defaultPosition = currentPosition = backgroundRT.position;

            Vector2 worldPoint = GuiCamera.ScreenToWorldPoint( touchPos );

            if( axisX.enabled ) currentPosition.x = worldPoint.x;
            if( axisY.enabled ) currentPosition.y = worldPoint.y;
        }
        
        // Update JoystickPosition
        private void UpdateJoystickPosition()
        {
            joystickRT.position = currentPosition;
        }

        // Update Transparency and Position for DynamicJoystick
        private void UpdateTransparencyAndPosition( Vector2 touchPos )
        {
            OnApplyVisible();
            joystickImage.enabled = backgroundImage.enabled = true;            
            joystickRT.position = backgroundRT.position = GuiCamera.ScreenToWorldPoint( touchPos );
        }

        // SmoothReturn Run
        private IEnumerator SmoothReturnRun()
        {
            bool smoothReturnIsRun = true;
            int defPosMagnitude = Mathf.RoundToInt( defaultPosition.sqrMagnitude );

            while( smoothReturnIsRun && touchDown == false )
            {                
                float smoothTime = smoothFactor * Time.smoothDeltaTime;

                currentPosition = Vector2.Lerp( currentPosition, defaultPosition, smoothTime );

                if( isStatic == false )
                {
                    joystickImage.color = Color.Lerp( joystickImage.color, Color.clear, smoothTime );
                    backgroundImage.color = Color.Lerp( backgroundImage.color, Color.clear, smoothTime );
                }

                if( Mathf.RoundToInt( currentPosition.sqrMagnitude ) == defPosMagnitude )
                {
                    currentPosition = defaultPosition;
                    smoothReturnIsRun = false;

                    if( isStatic == false ) {
                        joystickImage.enabled = backgroundImage.enabled = false;
                    }                                           
                }

                UpdateJoystickPosition();
                yield return null;
            }
        }


        // Control Reset
        protected override void ControlReset()
        {
            base.ControlReset();

            if( smoothReturn )
            {
                StopCoroutine( "SmoothReturnRun" );
                StartCoroutine( "SmoothReturnRun" );
            }
            else
            {
                joystickImage.enabled = backgroundImage.enabled = isStatic;
                currentPosition = defaultPosition;
                UpdateJoystickPosition();
            }
        }


        // OnPointer Down
        public void OnPointerDown( PointerEventData pointerData )
        {
            if( touchDown == false )
            {
                touchId = pointerData.pointerId;
                UpdatePosition( pointerData.position );
            }
        }

        // OnDrag
        public void OnDrag( PointerEventData pointerData )
        {
            if( Input.touchCount >= touchId && touchDown ) {
                UpdatePosition( pointerData.position );
            }                
        }

        // OnPointer Up
        public void OnPointerUp( PointerEventData pointerData )
        {
            UpdatePosition( pointerData.position );
            ControlReset();
        }
    };
}