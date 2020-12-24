/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEditor;

namespace TouchControlsKit.Inspector
{
    [CustomEditor( typeof( TCKTouchpad ) )]
    public class TCKTouchpadEditor : AxesBasedControllerEditor
    {
        // OnEnable
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        
        // ShowParameters
        protected override void ShowParameters()
        {
            DrawUpdateMode();
            DrawEnable( false );
            DrawIdentProp();
            DrawTouchZone();
            DrawSensitivityProp();
        }
    };
}