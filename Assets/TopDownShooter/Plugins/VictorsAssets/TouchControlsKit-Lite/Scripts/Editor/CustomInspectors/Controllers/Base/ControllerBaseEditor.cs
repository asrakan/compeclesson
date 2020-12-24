/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    public abstract class ControllerBaseEditor : Editor
    {
        protected SerializedProperty enableProp, activeProp, visibleProp;
        protected SerializedProperty updateModeProp, identifierProp;

        protected SerializedProperty baseImageColorProp;

        protected SerializedObject baseImageObj;

        Type targetType;
        List<MethodInfo> m_ApplyMethods = new List<MethodInfo>();
        static MethodInfo[] allApplyMethods;

        // enable & active & visible = true
        protected bool eavIsOk;

        // OnEnable
        protected virtual void OnEnable()
        {
            if( Application.isPlaying == false ) {
                ( target as ControllerBase ).OnAwake();
            }

            updateModeProp = serializedObject.FindProperty( "updateMode" );

            enableProp = serializedObject.FindProperty( "enable" );
            activeProp = serializedObject.FindProperty( "active" );
            visibleProp = serializedObject.FindProperty( "visible" );

            identifierProp = serializedObject.FindProperty( "identifier" );

            baseImageColorProp = serializedObject.FindProperty( "baseImageColor" );

            baseImageObj = new SerializedObject( serializedObject.FindProperty( "baseImage" ).objectReferenceValue );

            targetType = target.GetType();
            allApplyMethods = targetType
                .GetMethods( BindingFlags.Instance | BindingFlags.NonPublic )
                .Where( m => Attribute.IsDefined( m, typeof( ApplyMethodAttribute ) ) )
                .ToArray();
        }

        
        // OnInspectorGUI
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            eavIsOk = ( enableProp.boolValue && activeProp.boolValue && visibleProp.boolValue );

            using( TCKEditorLayout.Vertical( "box" ) )
            {                
                TCKEditorHelper.LargeFoldout( enableProp, "Parameters", ShowParameters );
                PostParametersDraw();                
            }

            serializedObject.ApplyModifiedProperties();
            PostAppliedModifiedProperties();
        }

        // ShowParameters
        protected virtual void ShowParameters()
        {
            DrawUpdateMode();
            DrawEnable();
        }

        
        // ShowSensitivity
        protected void DrawUpdateMode()
        {
            GUILayout.Space( 5f );
            using( TCKEditorLayout.Horizontal() )
            {
                GUILayout.Label( updateModeProp.displayName, GUILayout.Width( TCKEditorLayout.STANDARD_SIZE ) );
                TCKEditorHelper.DrawEnumAsToolbar( updateModeProp, false );
            }
        }


        // DrawEnable
        protected void DrawEnable( bool drawVisible = true )
        {
            GUILayout.Space( 5f );            
            DrawPropertyWithApply( enableProp );

            GUI.enabled = enableProp.boolValue;

            DrawPropertyWithApply( activeProp, TCKEditorLayout.STANDARD_INDENT );

            if( drawVisible ) {
                DrawPropertyWithApply( visibleProp, TCKEditorLayout.STANDARD_INDENT );
            }

            GUI.enabled = true;
        }

        // DrawProperty WithApply
        protected void DrawPropertyWithApply( SerializedProperty property, float space = 0f )
        {
            using( new TCKEditorChangeCheck( () => AddApplyMethod( property.name ) ) )
            {
                TCKEditorHelper.DrawPropertyField( property, space );
            }
        }


        // Draw IdentProp
        protected void DrawIdentProp()
        {
            GUILayout.Space( 5f );           

            if( target.name != identifierProp.stringValue )
            {
                identifierProp.stringValue = target.name;
            }

            using( var ecc = new TCKEditorChangeCheck() )
            {
                using( TCKEditorLayout.Horizontal() )
                {
                    GUILayout.Label( identifierProp.displayName, GUILayout.Width( TCKEditorLayout.STANDARD_SIZE ) );
                    EditorGUILayout.PropertyField( identifierProp, GUIContent.none );
                }

                ecc.OnChangeCheck = () => 
                {
                    string nextName = identifierProp.stringValue;

                    if( string.IsNullOrEmpty( nextName ) )
                    {
                        Debug.LogError( "Controller name for cant be empty" );
                        nextName = target.GetType().Name.Substring( 3 ) + UnityEngine.Random.value.ToString().Substring( 2 );
                    }

                    target.name = nextName;
                    identifierProp.stringValue = nextName;
                };
            }
        }


        // PostParametersDraw
        protected virtual void PostParametersDraw()
        {
            
        }


        // PostApplied ModifiedProperties
        private void PostAppliedModifiedProperties()
        {
            if( m_ApplyMethods.Count > 0 )
            {
                m_ApplyMethods.ForEach( mi => mi.Invoke( target, null ) );
                m_ApplyMethods.Clear();
            }
        }

        // Add DirtyMethod
        protected void AddApplyMethod( string propName )
        {
            m_ApplyMethods.Add( allApplyMethods.First( m => m.Name.ToLower().Contains( propName.ToLower() ) ) );
        }
    };
}