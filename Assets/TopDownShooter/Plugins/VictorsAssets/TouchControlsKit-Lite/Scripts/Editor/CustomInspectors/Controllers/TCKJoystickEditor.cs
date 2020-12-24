/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using UnityEngine;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    [CustomEditor( typeof( TCKJoystick ) )]
    public class TCKJoystickEditor : AxesBasedControllerEditor
    {
        SerializedProperty isStaticProp, borderSizeProp, 
            smoothReturnProp, smoothFactorProp,
            joystickImageColorProp, backgroundImageColorProp;

        SerializedObject joystickImageObj, backgroundImageObj;


        static readonly string[] modNames = { "Dynamic", "Static" };


        // OnEnable
        protected override void OnEnable()
        {
            base.OnEnable();

            isStaticProp = serializedObject.FindProperty( "isStatic" );
            borderSizeProp = serializedObject.FindProperty( "borderSize" );
            smoothReturnProp = serializedObject.FindProperty( "smoothReturn" );
            smoothFactorProp = serializedObject.FindProperty( "smoothFactor" );

            joystickImageColorProp = serializedObject.FindProperty( "joystickImageColor" );
            backgroundImageColorProp = serializedObject.FindProperty( "backgroundImageColor" );

            joystickImageObj = new SerializedObject( serializedObject.FindProperty( "joystickImage" ).objectReferenceValue );
            backgroundImageObj = new SerializedObject( serializedObject.FindProperty( "backgroundImage" ).objectReferenceValue );
        }

        
        // ShowParameters
        protected override void ShowParameters()
        {
            base.ShowParameters();
            
            DrawIdentProp();

            GUILayout.Space( 5f );
            using( TCKEditorLayout.Horizontal() )
            {
                GUILayout.Label( isStaticProp.GetLabel(), GUILayout.Width( TCKEditorLayout.STANDARD_SIZE ) );
                isStaticProp.boolValue = Convert.ToBoolean( GUILayout.Toolbar( Convert.ToInt32( isStaticProp.boolValue ), modNames, EditorStyles.miniButton, GUILayout.Height( 16f ) ) );
            }
            
            DrawSensitivityProp();

            GUILayout.Space( 5f );
            TCKEditorHelper.DrawPropertyField( borderSizeProp );

            GUILayout.Space( 5f );
            using( TCKEditorLayout.Horizontal() )
            {
                EditorGUILayout.PropertyField( smoothReturnProp, GUIContent.none, GUILayout.Width( 15f ) );
                GUILayout.Label( smoothReturnProp.GetLogicLabel(), GUILayout.Width( TCKEditorLayout.STANDARD_SIZE - 20f ) );
                GUI.enabled = smoothReturnProp.boolValue;
                EditorGUILayout.PropertyField( smoothFactorProp, GUIContent.none );
                GUI.enabled = true;
            }

            DrawTouchZone();

            GUILayout.Space( 5f );
            using( var ecc = new TCKEditorChangeCheck() )
            {
                GUI.enabled = eavIsOk;
                TCKEditorHelper.DrawSpriteAndColor( joystickImageObj, "Joystick" );
                TCKEditorHelper.DrawSpriteAndColor( backgroundImageObj, "Background" );
                GUI.enabled = true;

                ecc.OnChangeCheck = () => 
                {
                    joystickImageColorProp.colorValue = joystickImageObj.FindProperty( "m_Color" ).colorValue;
                    backgroundImageColorProp.colorValue = backgroundImageObj.FindProperty( "m_Color" ).colorValue;
                };
            }
        }
    };
}