/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    public sealed class TCKEditorChangeCheck : IDisposable
    {
        public Action OnChangeCheck = () => { };


        // Constructor
        public TCKEditorChangeCheck()
        {
            EditorGUI.BeginChangeCheck();
        }

        // Constructor
        public TCKEditorChangeCheck( Action OnChange )
        {
            OnChangeCheck = OnChange;
            EditorGUI.BeginChangeCheck();
        }
        

        // Dispose
        public void Dispose()
        {
            if( EditorGUI.EndChangeCheck() ) {
                OnChangeCheck.Invoke();
            }                
        }
    };
}
