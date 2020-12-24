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
    public static class TCKEditorHelper
    {
        // DrawPropertyField
        public static void DrawPropertyField( SerializedProperty property, float space = 0f )
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space( space );
            GUILayout.Label( property.displayName, GUILayout.Width( TCKEditorLayout.STANDARD_SIZE - space ) );
            EditorGUILayout.PropertyField( property, GUIContent.none );
            GUILayout.EndHorizontal();
        }

        // Draw SpriteAndColor
        public static void DrawSpriteAndColor( SerializedProperty spriteProp, SerializedProperty colorProp, string label )
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label( label, GUILayout.Width( TCKEditorLayout.STANDARD_SIZE ) );
            EditorGUILayout.PropertyField( colorProp, GUIContent.none, GUILayout.Width( TCKEditorLayout.STANDARD_SIZE / 2f ) );
            EditorGUILayout.PropertyField( spriteProp, GUIContent.none );
            GUILayout.EndHorizontal();
        }

        // Draw SpriteAndColor
        public static void DrawSpriteAndColor( SerializedObject imageObj, string label )
        {
            imageObj.Update();
            DrawSpriteAndColor( imageObj.FindProperty( "m_Sprite" ), imageObj.FindProperty( "m_Color" ), label );
            imageObj.ApplyModifiedProperties();
        }



        // Show FixedPropertyField
        public static void ShowFixedPropertyField( SerializedProperty property, string label, float space, float width )
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space( space );
            GUILayout.Label( label, GUILayout.Width( width ) );
            EditorGUILayout.PropertyField( property, GUIContent.none );
            GUILayout.EndHorizontal();
        }            
        
                
        // Draw StringPopup
        public static void DrawStringPopup( SerializedProperty property, string[] names, string label, params GUILayoutOption[] options )
        {
            int id = GetStringId( property, names );
            id = EditorGUILayout.Popup( label, id, names, options );
            property.stringValue = ( id > -1 ) ? names[ id ] : string.Empty;
        }

        // Draw StringPopup
        public static void DrawStringPopup( Rect rect, SerializedProperty property, string[] names )
        {
            int id = GetStringId( property, names );
            id = EditorGUI.Popup( rect, id, names );
            property.stringValue = ( id > -1 ) ? names[ id ] : string.Empty;
        }
        // GetStringId
        static int GetStringId( SerializedProperty property, string[] names )
        {
            string propValue = property.stringValue;
            return ArrayUtility.FindIndex( names, n => n == propValue );
        }

        
        // DrawLink
        public static void DrawLink( string label, string url, string tooltip = null )
        {
            label = label.Insert( 0, "• " );

            var style = TCKEditorStyle.Get;

            Rect btnRect = GUILayoutUtility.GetRect( GUIContent.none, GUIStyle.none );

            btnRect.width = style.link.CalcSize( new GUIContent( label ) ).x;

            if( btnRect.Contains( Event.current.mousePosition ) )
            {
                label = "<color=#0CB4CCFF>" + label + "</color>";
            }

            if( GUI.Button( btnRect, new GUIContent( label, string.IsNullOrEmpty( tooltip ) ? url : tooltip ), style.link ) )
            {
                Application.OpenURL( url );
            }

            EditorGUIUtility.AddCursorRect( btnRect, MouseCursor.Link );
        }


        // Separator
        public static void Separator()
        {
            GUILayout.Box( GUIContent.none, TCKEditorStyle.Get.line, GUILayout.ExpandWidth( true ), GUILayout.Height( 1f ) );
        }


        // LargeFoldout
        public static void LargeFoldout( SerializedProperty foldoutProp, string label, Action showMethod )
        {
            GUILayout.BeginVertical( "box", GUILayout.ExpandWidth( true ) );

            const float SPACE = 15f;
            Rect foldoutRect = EditorGUILayout.GetControlRect();
            EditorGUI.LabelField( foldoutRect, label, TCKEditorStyle.Get.largeBoldLabel );
            foldoutRect.x += SPACE;
            foldoutRect.width -= SPACE;

            bool foldout = foldoutProp.isExpanded;
            foldout = EditorGUI.Foldout( foldoutRect, foldout, string.Empty, true, EditorStyles.foldout );
            foldoutProp.isExpanded = foldout;

            if( foldout )
            {
                GUILayout.Space( 5f );
                showMethod.Invoke();
                GUILayout.Space( 5f );
            }
            else
            {
                GUILayout.Space( 2f );
            }

            GUILayout.EndVertical();
        }

        // ToggleFoldout
        public static bool ToggleFoldout( SerializedProperty property, string label, bool bold = true )
        {
            GUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 2f;
            EditorGUILayout.PropertyField( property, GUIContent.none, GUILayout.ExpandWidth( false ) );
            EditorGUIUtility.labelWidth = 0f;

            GUILayout.Space( 10f );
            property.isExpanded = EditorGUI.Foldout( EditorGUILayout.GetControlRect(), property.isExpanded, label, true, bold ? TCKEditorStyle.Get.richBoldFoldout : EditorStyles.foldout );
            GUILayout.EndHorizontal();

            return property.isExpanded;
        }


        
        // Draw StaticButton
        public static bool DrawStaticButton( GUIContent content, GUIStyle style, out Rect rect, params GUILayoutOption[] options )
        {
            rect = GUILayoutUtility.GetRect( content, style, options );
            Event ev = Event.current;

            if( ev.type == EventType.Repaint )
            {
                style.Draw( rect, content, false, false, false, false );
            }

            if( ev.type == EventType.MouseDown && rect.Contains( ev.mousePosition ) )
            {
                ev.Use();
                return true;
            }

            return false;
        }


        // DrawEnum AsToolbar
        public static void DrawEnumAsToolbar( SerializedProperty property, bool withLabel = true )
        {
            DrawEnumAsToolbar( EditorGUILayout.GetControlRect(), property, withLabel );
        }
        // DrawEnum AsToolbar
        public static void DrawEnumAsToolbar( Rect position, SerializedProperty property, bool withLabel = true )
        {
            if( withLabel ) {
                position = EditorGUI.PrefixLabel( position, new GUIContent( property.displayName, property.tooltip ) );
            }

            property.enumValueIndex = GUI.Toolbar( position, property.enumValueIndex, property.enumDisplayNames, EditorStyles.miniButton );
        }


        // DrawBool AsButton
        public static bool DrawBoolAsButton( SerializedProperty property )
        {
            Rect btnRect = EditorGUILayout.GetControlRect();

            bool boolValue = property.boolValue;
            boolValue = EditorGUI.Toggle( btnRect, boolValue, EditorStyles.toolbarButton );

            GUIStyle labelStyle = new GUIStyle( GUI.skin.label );
            labelStyle.fontStyle = boolValue ? FontStyle.Bold : FontStyle.Normal;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            EditorGUI.LabelField( btnRect, property.displayName + ( boolValue ? " ON" : " OFF" ), labelStyle );

            property.boolValue = boolValue;
            return boolValue;
        }
    };
}