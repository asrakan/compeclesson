/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using UnityEngine;

namespace TouchControlsKit.Inspector
{
    public struct TCKEditorLayout : IDisposable
    {
        public const float STANDARD_SIZE = 115f, STANDARD_INDENT = 15f;


        enum ELayoutMode : byte
        {
            Horizontal,
            Vertical,
            ScrollView
        };

        readonly ELayoutMode m_LayoutMode;


        // Constructor
        private TCKEditorLayout( ELayoutMode mode, GUIStyle style, params GUILayoutOption[] options )
        {
            m_LayoutMode = mode;

            switch( mode )
            {
                case ELayoutMode.Horizontal:
                    GUILayout.BeginHorizontal( style, options );
                    break;
                case ELayoutMode.Vertical:
                    GUILayout.BeginVertical( style, options );
                    break;

                default: break;
            }
        }
        // Constructor
        private TCKEditorLayout( ref Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options )
        {
            m_LayoutMode = ELayoutMode.ScrollView;
            scrollPosition = GUILayout.BeginScrollView( scrollPosition, style, options );
        }


        // Horizontal
        public static TCKEditorLayout Horizontal( params GUILayoutOption[] options )
        {
            return Horizontal( GUIStyle.none, options );
        }
        // Horizontal
        public static TCKEditorLayout Horizontal( GUIStyle style, params GUILayoutOption[] options )
        {
            return new TCKEditorLayout( ELayoutMode.Horizontal, style, options );
        }

        // Vertical
        public static TCKEditorLayout Vertical( params GUILayoutOption[] options )
        {
            return Vertical( GUIStyle.none, options );
        }
        // Vertical
        public static TCKEditorLayout Vertical( GUIStyle style, params GUILayoutOption[] options )
        {
            return new TCKEditorLayout( ELayoutMode.Vertical, style, options );
        }

        // ScrollView
        public static TCKEditorLayout ScrollView( ref Vector2 scrollPosition, params GUILayoutOption[] options )
        {
            return ScrollView( ref scrollPosition, GUIStyle.none, options );
        }
        // ScrollView
        public static TCKEditorLayout ScrollView( ref Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options )
        {
            return new TCKEditorLayout( ref scrollPosition, style, options );
        }


        // Dispose
        void IDisposable.Dispose()
        {
            switch( m_LayoutMode )
            {
                case ELayoutMode.Horizontal:
                    GUILayout.EndHorizontal();
                    break;
                case ELayoutMode.Vertical:
                    GUILayout.EndVertical();
                    break;
                case ELayoutMode.ScrollView:
                    GUILayout.EndScrollView();
                    break;

                default: break;
            }
        }
    };
}
