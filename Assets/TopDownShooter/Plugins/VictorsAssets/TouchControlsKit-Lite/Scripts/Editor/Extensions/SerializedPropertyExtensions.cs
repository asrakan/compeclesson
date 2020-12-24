/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using System.Reflection;
using UnityEditor;
using Object = UnityEngine.Object;

namespace TouchControlsKit.Inspector
{
    public static class SerializedPropertyExtensions
    {
        // SerializedProperty Info
        struct SPInfo<T>
        {
            public readonly Object targetObject;
            public readonly Type type;
            public readonly FieldInfo field;
            public readonly T attribute;

            public SPInfo( Object targetObject, Type type, FieldInfo field, T attribute )
            {
                this.targetObject = targetObject;
                this.type = type;
                this.field = field;
                this.attribute = attribute;
            }
        };

        
        const BindingFlags k_BindingFlags = ( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );


        // GetSP Info From SerializedProperty
        static SPInfo<T> GetSPInfoFrom<T>( SerializedProperty property ) where T : Attribute
        {
            Object targetObject = property.serializedObject.targetObject;
            Type type = targetObject.GetType();
            FieldInfo field = type.GetField( property.name, k_BindingFlags );
            T attr = Attribute.GetCustomAttribute( field, typeof( T ) ) as T;

            return new SPInfo<T>( targetObject, type, field, attr );
        }


        // GetAttribute
        public static T GetAttribute<T>( this SerializedProperty property ) where T : Attribute
        {
            return GetSPInfoFrom<T>( property ).attribute;
        }

        // GetLabel
        public static string GetLabel( this SerializedProperty property )
        {
            var attr = GetSPInfoFrom<LabelAttribute>( property ).attribute;
            return ( attr != null ) ? attr.label : property.displayName;
        }

        // GetLogicLabel
        public static string GetLogicLabel( this SerializedProperty property )
        {
            var spInfo = GetSPInfoFrom<LogicLabelAttribute>( property );
            FieldInfo boolField = spInfo.type.GetField( spInfo.attribute.memberName, k_BindingFlags );

            if( boolField == null ) {
                return property.displayName;
            }

            bool boolValue = ( bool )boolField.GetValue( spInfo.targetObject );
            return boolValue ? spInfo.attribute.trueLabel : spInfo.attribute.falseLabel;
        }
    };
}
