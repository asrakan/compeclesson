/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System.Collections.Generic;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    public static class TCKInitialize
    {
        const string ASSET_DEFINE = "TOUCH_CONTROLS_KIT";


        readonly static BuildTargetGroup[] buildTargetGroups = new BuildTargetGroup[]
             {
                BuildTargetGroup.Standalone,
                BuildTargetGroup.Android,
                BuildTargetGroup.iOS
            };


        const string DEFS = "Define Symbols";


        [MenuItem( TCKWindow.MENU_ITEM_PATH + "Add " + DEFS, false, 500 )]
        private static void AddDefine()
        {
            SetDefineEnabled( ASSET_DEFINE, true );
        }
        [MenuItem( TCKWindow.MENU_ITEM_PATH + "Add " + DEFS, true, 500 )]
        private static bool AddDefineValidate()
        {
            return !CheckDefines( ASSET_DEFINE );
        }

        [MenuItem( TCKWindow.MENU_ITEM_PATH + "Remove " + DEFS, false, 501 )]
        private static void RemoveDefine()
        {
            SetDefineEnabled( ASSET_DEFINE, false );
        }
        [MenuItem( TCKWindow.MENU_ITEM_PATH + "Remove " + DEFS, true, 501 )]
        private static bool RemoveDefineValidate()
        {
            return CheckDefines( ASSET_DEFINE );
        }


        // CheckDefines
        private static bool CheckDefines( string defineName )
        {
            foreach( var group in buildTargetGroups )
            {
                if( GetDefinesList( group ).Contains( defineName ) )
                {
                    return true;
                }
            }

            return false;
        }


        // SetEnabled
        private static void SetDefineEnabled( string defineName, bool enable )
        {
            foreach( var group in buildTargetGroups )
            {
                var defines = GetDefinesList( group );

                if( enable )
                {
                    if( defines.Contains( defineName ) )
                    {
                        return;
                    }

                    defines.Add( defineName );
                }
                else
                {
                    if( defines.Contains( defineName ) == false )
                    {
                        return;
                    }

                    while( defines.Contains( defineName ) )
                    {
                        defines.Remove( defineName );
                    }
                }

                PlayerSettings.SetScriptingDefineSymbolsForGroup( group, string.Join( ";", defines.ToArray() ) );
            }
        }

        // Get DefinesList
        private static List<string> GetDefinesList( BuildTargetGroup group )
        {
            return new List<string>( PlayerSettings.GetScriptingDefineSymbolsForGroup( group ).Split( ';' ) );
        }
    };
}
