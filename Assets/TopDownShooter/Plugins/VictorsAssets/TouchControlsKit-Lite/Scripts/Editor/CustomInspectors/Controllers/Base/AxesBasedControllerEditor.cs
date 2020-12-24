/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/

 
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    public class AxesBasedControllerEditor : ControllerBaseEditor
    {
        sealed class AxisSPData
        {
            public SerializedProperty selfProp;
            public SerializedProperty enabledProp, inverseProp;
            public string label { get; private set; }

            public void Init()
            {
                label = selfProp.GetLabel();
                enabledProp = selfProp.FindPropertyRelative( "enabled" );
                inverseProp = selfProp.FindPropertyRelative( "inverse" );
            }
        };
        
        protected int endAxisIndexToDraw;

        List<AxisSPData> axisSPData = new List<AxisSPData>();
        SerializedProperty axesLagProp, axesLagSpeedProp;

        SerializedProperty sensitivityProp, showTouchZoneProp;
                
        
        // OnEnable
        protected override void OnEnable()
        {
            base.OnEnable();

            axisSPData.Add( new AxisSPData { selfProp = serializedObject.FindProperty( "axisX" ) } );
            axisSPData.Add( new AxisSPData { selfProp = serializedObject.FindProperty( "axisY" ) } );

            axisSPData.ForEach( ad => ad.Init() );

            endAxisIndexToDraw = axisSPData.Count;

            axesLagProp = serializedObject.FindProperty( "axesLag" );
            axesLagSpeedProp = serializedObject.FindProperty( "axesLagSpeed" );

            sensitivityProp = serializedObject.FindProperty( "sensitivity" );
            showTouchZoneProp = serializedObject.FindProperty( "showTouchZone" );
        }


        // ShowParameters
        protected override void ShowParameters()
        {
            base.ShowParameters();            
        }

        // Post ParametersDraw
        protected override void PostParametersDraw()
        {
            DrawAxes();
            base.PostParametersDraw();
        }

        // DoLayout Axes
        private void DrawAxes()
        {
            TCKEditorHelper.LargeFoldout( axesLagProp, "Axes", () =>
            {
                for( int i = 0; i < endAxisIndexToDraw; i++ ) {
                    DrawAxisData( axisSPData[ i ] );
                }

                EditorGUILayout.Space();

                using( TCKEditorLayout.Horizontal() )
                {
                    GUILayout.Space( 10f );
                    GUI.enabled = AnyAxisEnabled();

                    bool axesLag = axesLagProp.boolValue;
                    axesLag = EditorGUILayout.Toggle( axesLag, GUILayout.Width( 15f ) );
                    axesLagProp.boolValue = axesLag;

                    GUILayout.Label( axesLagProp.GetLabel(), GUILayout.Width( 25f ) );
                    GUI.enabled &= axesLag;

                    EditorGUILayout.PropertyField( axesLagSpeedProp, GUIContent.none );
                    GUI.enabled = true;
                }
            } );
        }

        // AnyAxis Enabled
        bool AnyAxisEnabled()
        {
            for( int i = 0; i < axisSPData.Count; i++ ) {
                if( axisSPData[ i ].enabledProp.boolValue ) {
                    return true;
                }
            }

            return false;
        }

        // Draw AxisData
        static void DrawAxisData( AxisSPData axisData )
        {
            using( TCKEditorLayout.Horizontal() )
            {
                bool enabled = axisData.enabledProp.boolValue;
                enabled = EditorGUILayout.Toggle( enabled, GUILayout.Width( 15f ) );
                axisData.enabledProp.boolValue = enabled;
                GUI.enabled = enabled;

                GUILayout.Label( axisData.label );

                bool inverse = axisData.inverseProp.boolValue;
                inverse = EditorGUILayout.Toggle( inverse, GUILayout.Width( 15f ) );
                axisData.inverseProp.boolValue = inverse;
                GUILayout.Label( axisData.inverseProp.displayName, GUILayout.Width( 55f ) );
                GUI.enabled = true;
            }
        }
        

        // Draw Sensitivity
        protected void DrawSensitivityProp()
        {
            GUILayout.Space( 5f );
            TCKEditorHelper.DrawPropertyField( sensitivityProp );
        }

        // Draw TouchZone
        protected void DrawTouchZone()
        {
            GUILayout.Space( 5f );

            using( TCKEditorLayout.Horizontal() )
            {
                GUI.enabled = eavIsOk;

                using( new TCKEditorChangeCheck( () => AddApplyMethod( showTouchZoneProp.name ) ) )
                {
                    EditorGUILayout.PropertyField( showTouchZoneProp, GUIContent.none, GUILayout.Width( 15f ) );
                }
                GUI.enabled &= showTouchZoneProp.boolValue;

                using( var ecc = new TCKEditorChangeCheck() )
                {
                    baseImageObj.Update();
                    GUILayout.Label( "Touch Zone", GUILayout.Width( 95f ) );
                    SerializedProperty colorProp = baseImageObj.FindProperty( "m_Color" );
                    EditorGUILayout.PropertyField( colorProp, GUIContent.none, GUILayout.Width( TCKEditorLayout.STANDARD_SIZE / 2f ) );
                    EditorGUILayout.PropertyField( baseImageObj.FindProperty( "m_Sprite" ), GUIContent.none );
                    baseImageObj.ApplyModifiedProperties();

                    ecc.OnChangeCheck = () => 
                    {
                        baseImageColorProp.colorValue = colorProp.colorValue;
                    };
                }                
                                
                GUI.enabled = true;
            }
        }
    };
}
