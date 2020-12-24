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
    public class LogicLabelAttribute : Attribute
    {
        public readonly string memberName, trueLabel, falseLabel;

        public LogicLabelAttribute( string memberName, string trueLabel, string falseLabel )
        {
            this.memberName = memberName;
            this.trueLabel = trueLabel;
            this.falseLabel = falseLabel;
        }
    };
}
