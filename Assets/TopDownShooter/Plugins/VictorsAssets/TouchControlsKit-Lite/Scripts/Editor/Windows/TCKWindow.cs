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
    public class TCKWindow : EditorWindow
    {
        const string TITLE = "Touch Kit";
        const string LOGO_NAME = "TCKLogoIcon";

        public const string MENU_ITEM_PATH = "Tools/Victor's Assets/Touch Controls Kit - Lite/";

        static TCKWindow window;

        static readonly Dictionary<string, Texture2D> m_Images = new Dictionary<string, Texture2D>();
        
        public static string editorDirectory { get; private set; }

        public static string imagesPath { get { return editorDirectory + "/Images/"; } }
        public static string iconsPath { get { return imagesPath + "Icons/"; } }




        // GetImage
        public static Texture2D GetImage( string imgPath )
        {
            Texture2D image;

            if( m_Images.TryGetValue( imgPath, out image ) == false )
            {
                image = AssetDatabase.LoadAssetAtPath<Texture2D>( imgPath + ".png" );

                if( image != null )
                {
                    m_Images.Add( imgPath, image );
                }
            }

            return image;
        }


        // Show About
        [MenuItem( MENU_ITEM_PATH + "About", false, 151 )]
        static void ShowAbout()
        {
            Init();
        }

        // Init
        static void Init()
        {
            window = GetWindow<TCKWindow>();
            window.minSize = new Vector2( 725f, 535f );
            window.Focus();

            SetupIt();

            window.titleContent = new GUIContent( TITLE, GetImage( imagesPath + LOGO_NAME ) );
        }

        // SetupIt
        static void SetupIt()
        {
            var monoScript = MonoScript.FromScriptableObject( window );
            editorDirectory = GetEditorPath( monoScript );
        }

        // Get EditorPath
        private static string GetEditorPath( MonoScript monoScript )
        {
            string assetPath = AssetDatabase.GetAssetPath( monoScript );
            const string endFolder = "/Editor";

            if( assetPath.Contains( endFolder ) )
            {
                int endIndex = assetPath.IndexOf( endFolder, 0 );
                string between = assetPath.Substring( 0, endIndex );
                return between + endFolder;
            }

            return string.Empty;
        }



        // OnGUI
        void OnGUI()
        {
            GUILayout.Space( 5f ); 

            using( TCKEditorLayout.Horizontal() )
            {
                TCKAboutTab.OnTabGUI();
            }
        }
    };
}
