/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    public static class TCKAboutTab
    {
        const string PABLISHER_URL = "http://u3d.as/5Fb";
        const string SUPPORT_URL = "http://bit.ly/vk-SupportNew";

        const string VERSION = "1.5.5";
        const string LOGO_NAME = "TCKLogoBig";

        const string
             MANUAL_URL = "https://goo.gl/1514Qz"
            , FORUM_URL = "http://forum.unity.com/threads/210040"
            , CHANGELOG_URL = "http://smart-assets.org/index/0-11"
            , ASSET_URL = "http://u3d.as/hS6";


        static Texture2D m_Logo;
        private static Texture2D logo
        {
            get
            {
                if( m_Logo == null ) {
                    m_Logo = TCKWindow.GetImage( TCKWindow.imagesPath + LOGO_NAME );
                }

                return m_Logo;
            }
        }


        // OnTabGUI
        public static void OnTabGUI()
        {
            var style = TCKEditorStyle.Get;

            // LINK's
            using( TCKEditorLayout.Vertical( "box", GUILayout.ExpandWidth( true ), GUILayout.ExpandHeight( true ) ) )
            {
                GUILayout.Space( 5f );

                using( TCKEditorLayout.Vertical( style.area ) )
                {
                    GUILayout.Label( "Documentation", style.headLabel );

                    GUILayout.BeginVertical( style.area );
                    TCKEditorHelper.DrawLink( "Online Manual", MANUAL_URL );
                    GUILayout.EndVertical();
                }

                using( TCKEditorLayout.Vertical( style.area ) )
                {
                    GUILayout.Label( "Support, News, More Assets", style.headLabel );

                    GUILayout.BeginVertical( style.area );
                    TCKEditorHelper.DrawLink( "Support", SUPPORT_URL );
                    GUILayout.Space( 10f );
                    TCKEditorHelper.DrawLink( "Forum", FORUM_URL );
                    GUILayout.Space( 25f );                    
                    TCKEditorHelper.DrawLink( "Get Pro", "http://u3d.as/5NP" );
                    GUILayout.Space( 10f );
                    TCKEditorHelper.DrawLink( "More Assets", PABLISHER_URL );
                    GUILayout.EndVertical();
                }

                using( TCKEditorLayout.Vertical( style.area ) )
                {
                    GUILayout.Label( "Release Notes", style.headLabel );

                    GUILayout.BeginVertical( style.area );
                    TCKEditorHelper.DrawLink( "Full Changelog", CHANGELOG_URL );
                    GUILayout.EndVertical();
                }
            }

            // LOGO
            using( TCKEditorLayout.Vertical( "box", GUILayout.Width( 280f ), GUILayout.ExpandHeight( true ) ) )
            {
                GUILayout.Space( 5f );
                GUILayout.Label( "<size=18>Touch Controls Kit - Lite</size>", style.centeredLabel );

                GUILayout.Space( 5f );
                GUILayout.Label( "<size=16> Developed by Victor Klepikov\n" +
                                 "Version <b>" + VERSION + "</b> </size>", style.centeredLabel );

                EditorGUILayout.Space();
                TCKEditorHelper.Separator();

                if( logo != null )
                {
                    GUILayout.FlexibleSpace();

                    using( TCKEditorLayout.Horizontal() )
                    {
                        GUILayout.FlexibleSpace();

                        Rect logoRect = EditorGUILayout.GetControlRect( GUILayout.Width( logo.width ), GUILayout.Height( logo.height ) );

                        if( GUI.Button( logoRect, new GUIContent( logo, "Open AssetStore Page" ), EditorStyles.label ) )
                        {
                            Application.OpenURL( ASSET_URL );
                        }

                        EditorGUIUtility.AddCursorRect( logoRect, MouseCursor.Link );

                        GUILayout.FlexibleSpace();
                    }

                    GUILayout.FlexibleSpace();
                }
                else
                {
                    GUILayout.Label( "<size=15>Logo not found</size> \n" + LOGO_NAME, style.centeredLabel );
                }
            }
        }
    };
}
