/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchControlsKit
{
    public sealed class TCKInput : MonoBehaviour
    {
        static TCKInput instance
        {
            get
            {
                if( m_Instance == null ) {
                    m_Instance = FindObjectOfType<TCKInput>();
                }

                return m_Instance;
            }
        }

        static TCKInput m_Instance;


        static bool m_Active;

        /// <summary>
        /// The local active state of all controllers. (Read Only)
        /// </summary>
        public static bool isActive { get { return m_Active; } }


        static ControllerBase[] allControllers
        {
            get
            {
                if( m_Inited == false ) {
                    InitControllers();
                }

                return m_AllControllers;
            }
        }
        static AxesBasedController[] abControllers
        {
            get
            {
                if( m_Inited == false ) {
                    InitControllers();
                }

                return m_AbControllers;
            }
        }
        static TCKButton[] buttons
        {
            get
            {
                if( m_Inited == false ) {
                    InitControllers();
                }

                return m_Buttons;
            }
        }

        static ControllerBase[] m_AllControllers;
        static AxesBasedController[] m_AbControllers;
        static TCKButton[] m_Buttons;

        static bool m_Inited;


        // Check UIEventSystem
        public static void CheckUIEventSystem()
        {
            if( FindObjectOfType<EventSystem>() != null )
            {
                return;
            }

            Type evType = typeof( EventSystem );
            new GameObject( evType.Name, evType, typeof( StandaloneInputModule ) );
        }


        // Awake
        void Awake()
        {
            CheckUIEventSystem();

            m_Instance = this;
            SetActive( true );
            InitControllers();
            Array.ForEach( m_AllControllers, c => c.OnAwake() );
        }

        // OnDisable
        void OnDisable()
        {
            OnReset();
        }

        // OnDestroy
        void OnDestroy()
        {
            OnReset();
        }

        // OnReset
        static void OnReset()
        {
            m_Inited = false;
            m_Instance = null;
        }


        // InitControllers
        static void InitControllers()
        {
            m_Inited = true;
            m_AllControllers = instance.GetComponentsInChildren<ControllerBase>( true );
            m_AbControllers = GetControllers<AxesBasedController>();
            m_Buttons = GetControllers<TCKButton>();
        }



        // Get Controllers
        static T[] GetControllers<T>() where T : ControllerBase
        {
            return allControllers.Where( obj => obj is T ).Cast<T>().ToArray();
        }


        /// <summary>
        /// Activates/Deactivates all controllers in scene.
        /// </summary>
        /// <param name="value"></param>
        public static void SetActive( bool value )
        {
            m_Active = value;
            instance.enabled = value;
            instance.gameObject.SetActive( value );
        }
        
        
        
        /// <summary>
        /// Checked controller in scene, identified by controllerName
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static bool CheckController( string controllerName )
        {
            if( !m_Active )
                return false;            

            foreach( ControllerBase controller in allControllers )
                if( controller.identifier == controllerName )
                    return true;                    

            return false;
        }


        /// <summary>
        /// Returns the controller Enable value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static bool GetControllerEnable( string controllerName )
        {
            if( !m_Active )
                return false;

            foreach( ControllerBase controller in allControllers )            
                if( controller.identifier == controllerName )                
                    return controller.isEnable;

            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return false;
        }

        /// <summary>
        /// Sets the controller Enable value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="value"></param>
        public static void SetControllerEnable( string controllerName, bool value )
        {
            if( !m_Active )
                return;

            foreach( ControllerBase controller in allControllers )
            {
                if( controller.identifier == controllerName )
                {
                    controller.isEnable = value;
                    return;
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Returns the controller Active state value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static bool GetControllerActive( string controllerName )
        {
            if( !m_Active )
                return false;

            foreach( ControllerBase controller in allControllers )
                if( controller.identifier == controllerName )
                    return controller.isActive;

            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return false;
        }

        /// <summary>
        /// Sets the controller Active state value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="value"></param>
        public static void SetControllerActive( string controllerName, bool value )
        {
            if( !m_Active )
                return;

            foreach( ControllerBase controller in allControllers )
            {
                if( controller.identifier == controllerName )
                {
                    controller.isActive = value;
                    return;
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Returns the controller Visibility value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static bool GetControllerVisible( string controllerName )
        {
            if( !m_Active )
                return false;

            foreach( ControllerBase controller in allControllers )
                if( controller.identifier == controllerName )
                    return controller.isVisible;

            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return false;
        }

        /// <summary>
        /// Sets the controller Visibility value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="value"></param>
        public static void SetControllerVisible( string controllerName, bool value )
        {
            if( !m_Active )
                return;

            foreach( ControllerBase controller in allControllers )
            {
                if( controller.identifier == controllerName )
                {
                    controller.isVisible = value;
                    return;
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Returns the Axis value identified by controllerName & axisType.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisType"></param>
        /// <returns></returns>
        public static float GetAxis( string controllerName, EAxisType axisType )
        {
            if( !m_Active )
                return 0f;

            for( int i = 0; i < abControllers.Length; i++ )
            {
                if( abControllers[ i ].identifier == controllerName )
                {
                    return ( axisType == EAxisType.Horizontal )
                        ? abControllers[ i ].axisX.value : abControllers[ i ].axisY.value;
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return 0f;
        }

        /// <summary>
        /// Returns the Axes values as Vector2 identified by controllerName & axisType.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisType"></param>
        /// <returns></returns>
        public static Vector2 GetAxis( string controllerName )
        {
            for( int i = 0; i < abControllers.Length; i++ )
            {
                if( abControllers[ i ].identifier == controllerName )
                {
                    return new Vector2( abControllers[ i ].axisX.value, abControllers[ i ].axisY.value );
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return Vector2.zero;
        }


        /// <summary>
        /// Returns the axis Enable value identified by controllerName & axisType.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisType"></param>
        /// <returns></returns>
        public static bool GetAxisEnable( string controllerName, EAxisType axisType )
        {
            if( !m_Active )
                return false;

            foreach( AxesBasedController controller in abControllers )
            {
                if( controller.identifier == controllerName )
                {
                    return ( axisType == EAxisType.Horizontal )
                        ? controller.axisX.enabled : controller.axisY.enabled;
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return false;
        }        

        /// <summary>
        /// Sets the axis Enable value identified by controllerName & EAxisType.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="EAxisType"></param>
        /// <param name="value"></param>
        public static void SetAxisEnable( string controllerName, EAxisType axisType, bool value )
        {
            if( !m_Active )
                return;

            foreach( AxesBasedController controller in abControllers )
            {
                if( controller.identifier == controllerName )
                {
                    if( axisType == EAxisType.Horizontal )
                    {
                        controller.axisX.enabled = value;
                        return;
                    }
                    else if( axisType == EAxisType.Vertical )
                    {
                        controller.axisY.enabled = value;
                        return;
                    }
                    Debug.LogError( "Axis: " + axisType + " not found!" );
                    return;
                }
            }
            Debug.LogError( "Controller: " + controllerName + " not found!" );
        }

        /// <summary>
        /// Returns the axis Inverse value identified by controllerName & axisType.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisType"></param>
        /// <returns></returns>
        public static bool GetAxisInverse( string controllerName, EAxisType axisType )
        {
            if( !m_Active )
                return false;

            foreach( AxesBasedController controller in abControllers )
            {
                if( controller.identifier == controllerName )
                {
                    return ( axisType == EAxisType.Horizontal )
                        ? controller.axisX.inverse : controller.axisY.inverse;
                }
            }
            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return false;
        }

        /// <summary>
        /// Sets the axis Inverse value identified by controllerName & axisType.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisType"></param>
        /// <param name="value"></param>
        public static void SetAxisInverse( string controllerName, EAxisType axisType, bool value )
        {
            if( !m_Active )
                return;

            foreach( AxesBasedController controller in abControllers )
            {
                if( controller.identifier == controllerName )
                {
                    if( axisType == EAxisType.Horizontal )
                    {
                        controller.axisX.inverse = value;
                        return;
                    }
                    else if( axisType == EAxisType.Vertical )
                    {
                        controller.axisY.inverse = value;
                        return;
                    }
                    Debug.LogError( "Axis: " + axisType + " not found!" );
                    return;
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Returns the value of the controller Sensitivity identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static float GetSensitivity( string controllerName )
        {
            if( !m_Active )
                return 0f;

            foreach( AxesBasedController controller in abControllers )            
                if( controller.identifier == controllerName )                
                    return controller.sensitivity;                
            
            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return 0f;
        }

        /// <summary>
        /// Sets the Sensitivity value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="value"></param>
        public static void SetSensitivity( string controllerName, float value )
        {
            if( !m_Active )
                return;

            foreach( AxesBasedController controller in abControllers )
            {
                if( controller.identifier == controllerName )
                {
                    controller.sensitivity = value;
                    return;
                }
            }

            Debug.LogError( "Controller: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Showing/Hiding touch zone for all controllers in scene.
        /// </summary>
        /// <param name="value"></param>
        public static void ShowingTouchZone( bool value )
        {
            if( !m_Active )
                return;

            Array.ForEach( abControllers, c => c.ShowTouchZone = value );
        }


        /// <summary>
        /// Returns true during the frame the user action event the touch button identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <param name="m_Event"></param>
        /// <returns></returns>
        public static bool GetAction( string buttonName, EActionEvent m_Event )
        {
            if( !m_Active )
                return false;

            for( int i = 0; i < buttons.Length; i++ )
            {
                if( buttons[ i ].identifier == buttonName )
                {
                    switch( m_Event )
                    {
                        case EActionEvent.Down:
                            return buttons[ i ].isDOWN;
                        case EActionEvent.Press:
                            return buttons[ i ].isPRESSED;
                        case EActionEvent.Up:
                            return buttons[ i ].isUP;
                        case EActionEvent.Click:
                            return buttons[ i ].isCLICK;

                        default: break;
                    }
                }
            }

            Debug.LogError( "Button: " + buttonName + " not found!" );
            return false;
        }

        /// <summary>
        /// Returns true during the frame the user pressed down the touch button identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        [Obsolete( "Please use 'GetAction' instead." )]
        public static bool GetButtonDown( string buttonName )
        {
            if( !m_Active )
                return false;

            foreach( TCKButton button in buttons )            
                if( button.identifier == buttonName )                
                    return button.isDOWN;                
            
            Debug.LogError( "Button: " + buttonName + " not found!" );
            return false;
        }
        /// <summary>
        /// Returns whether the given touch button is held down identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        [Obsolete( "Please use 'GetAction' instead." )]
        public static bool GetButton( string buttonName )
        {
            if( !m_Active )
                return false;

            foreach( TCKButton button in buttons )            
                if( button.identifier == buttonName )                
                    return button.isPRESSED;                
            
            Debug.LogError( "Button: " + buttonName + " not found!" );
            return false;
        }
        /// <summary>
        /// Returns true during the frame the user releases the given touch button identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        [Obsolete( "Please use 'GetAction' instead." )]
        public static bool GetButtonUp( string buttonName )
        {
            if( !m_Active )
                return false;

            foreach( TCKButton button in buttons )            
                if( button.identifier == buttonName )                
                    return button.isUP;                
            
            Debug.LogError( "Button: " + buttonName + " not found!" );
            return false;
        }
        /// <summary>
        /// Returns true during the frame the user clicked the given touch button identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        [Obsolete( "Please use 'GetAction' instead." )]
        public static bool GetButtonClick( string buttonName )
        {
            if( !m_Active )
                return false;

            foreach( TCKButton button in buttons )            
                if( button.identifier == buttonName )                
                    return button.isCLICK;               
            
            Debug.LogError( "Button: " + buttonName + " not found!" );
            return false;
        }


        /// <summary>
        /// Returns the EUpdateType enum value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static EUpdateMode GetEUpdateType( string controllerName )
        {
            if( !m_Active )
                return EUpdateMode.OFF;

            foreach( ControllerBase controller in allControllers )
                if( controller.identifier == controllerName )
                    return controller.updateMode;

            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return EUpdateMode.OFF;
        }

        /// <summary>
        /// Sets the EUpdateType enum value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="EUpdateType"></param>
        public static void SetEUpdateType( string controllerName, EUpdateMode EUpdateType )
        {
            if( !m_Active )
                return;

            foreach( ControllerBase controller in allControllers )
                if( controller.identifier == controllerName )
                    controller.updateMode = EUpdateType;

            Debug.LogError( "Controller: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Returns describes phase of a finger touch identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static ETouchPhase GetTouchPhase( string controllerName )
        {
            if( !m_Active )
                return ETouchPhase.NoTouch;

            foreach( ControllerBase controller in allControllers )            
                if( controller.identifier == controllerName )                
                    return controller.touchPhase;                
            
            Debug.LogError( "Controller: " + controllerName + " not found!" );
            return ETouchPhase.NoTouch;
        }
    };
}