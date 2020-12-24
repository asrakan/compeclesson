/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchControlsKit
{
    public class TCKTouchpad : AxesBasedController,
        IPointerUpHandler, IPointerDownHandler, IDragHandler, IPointerEnterHandler
    {
        GameObject prevPointerPressGO;


        // Set Visible
        protected override void OnApplyVisible()
        { }


        // Update Position
        protected override void UpdatePosition( Vector2 touchPos )
        {
            if( !axisX.enabled && !axisY.enabled )
                return;

            base.UpdatePosition( touchPos );

            if( touchDown )
            {
                if( axisX.enabled ) currentPosition.x = touchPos.x;
                if( axisY.enabled ) currentPosition.y = touchPos.y;

                currentDirection = currentPosition - defaultPosition;
                
                float touchForce = Vector2.Distance( defaultPosition, currentPosition ) * 2f;
                defaultPosition = currentPosition;

                SetAxes( currentDirection.normalized * touchForce / 100f * sensitivity );
            }
            else
            {
                touchDown = true;
                touchPhase = ETouchPhase.Began;

                currentPosition = defaultPosition = touchPos;
                UpdatePosition( touchPos );
                ResetAxes();
            }
        }
               
        
        // OnPointer Enter
        public void OnPointerEnter( PointerEventData pointerData )
        {
            if( pointerData.pointerPress == null )
                return;

            if( pointerData.pointerPress == gameObject )
            {
                OnPointerDown( pointerData );
                return;
            }

            var btn = pointerData.pointerPress.GetComponent<TCKButton>();
            if( btn != null && btn.swipeOut )
            {
                prevPointerPressGO = pointerData.pointerPress;
                pointerData.pointerDrag = gameObject;
                pointerData.pointerPress = gameObject;
                OnPointerDown( pointerData );
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
            if( Input.touchCount >= touchId && touchDown )
            {
                UpdatePosition( pointerData.position );
                StopCoroutine( "UpdateEndPosition" );
                StartCoroutine( "UpdateEndPosition", pointerData.position );
            }            
        }


        // Update EndPosition
        private IEnumerator UpdateEndPosition( Vector2 position )
        {
            for( float el = 0f; el < .0025f; el += Time.deltaTime )
                yield return null;
            
            if( touchDown )
                UpdatePosition( position );
            else
                ControlReset();
        }

        // OnPointer Up
        public void OnPointerUp( PointerEventData pointerData )
        {
            if( prevPointerPressGO != null )
            {
                ExecuteEvents.Execute( prevPointerPressGO, pointerData, ExecuteEvents.pointerUpHandler );
                prevPointerPressGO = null;
            }

            ControlReset();
        }
    };
}