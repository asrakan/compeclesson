/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;

namespace TouchControlsKit
{
    [AttributeUsage( AttributeTargets.Field, Inherited = true, AllowMultiple = false )]
    public class LabelAttribute : Attribute
    {
        public readonly string label;

        public LabelAttribute( string label )
        {
            this.label = label;
        }
    };
}
