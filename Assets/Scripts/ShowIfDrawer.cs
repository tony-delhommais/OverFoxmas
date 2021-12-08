/*********************
* Copyright (c) 2016, Rudolf Chrispens.  All rights reserved.
* found on http://www.rudolf-chrispens.de/posts/
* Copyrights licensed under the GNU License.
* https://www.gnu.org/licenses/gpl-3.0
***********************/

using UnityEngine;
using UnityEditor;
using System;
using Object = System.Object;
using System.Collections;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    bool show = true;
    bool typeNotSupported = false;
    ShowIfAttribute hideINAttribute;
    SerializedProperty refProperty;

    float infoButtonWidth = 20f;
    Color storedBGColor = GUI.backgroundColor;
    Color customColor = new Color(0.639215f, 0.96078f, 1.0f);

    public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
    {
        hideINAttribute = (ShowIfAttribute)attribute;

        //Search for refProperty
        if (hideINAttribute.ParentName != "")
        {
            //incapsulated
            refProperty = GetCapsulatedProperty(_Property, hideINAttribute);
        }
        else
        {
            //same depth
            refProperty = _Property.serializedObject.FindProperty(hideINAttribute.Name);
        }

        if (refProperty != null)
        {
            show = ReturnValue(refProperty, hideINAttribute.matchValue);
        }

        if (show)
        {
            return base.GetPropertyHeight(_Property, _Label);
        }
        else
            return -1f;
    }

    public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
    {
        if (refProperty == null)
        {
            EditorGUI.HelpBox(_Position, "Could not find: '" + hideINAttribute.Name + "' reference property missing!", MessageType.Error);
        }
        else if (typeNotSupported)
        {
            EditorGUI.HelpBox(_Position, "Type of '" + refProperty.name + "' (" + refProperty.type + ") is not supported!", MessageType.Error);
        }
        else
        {
            if (show)
            {
                GUI.backgroundColor = customColor;
                EditorGUI.indentLevel = 1;
                EditorGUI.PropertyField(new Rect(_Position.x, _Position.y, _Position.width - infoButtonWidth, _Position.height), _Property, _Label, true);
                if (GUI.Button(new Rect(_Position.x + _Position.width - infoButtonWidth, _Position.y, infoButtonWidth, _Position.height), "i"))
                {
                    if (!hideINAttribute.Revert)
                    {
                        Debug.Log("This property will be visible if (" + refProperty.name + ")== " + hideINAttribute.matchValue);
                    }
                    else
                    {
                        Debug.Log("This property will be hidden if (" + refProperty.name + ") == " + hideINAttribute.matchValue);
                    }
                }
                GUI.backgroundColor = storedBGColor;
            }
        }
    }

    public SerializedProperty GetCapsulatedProperty(SerializedProperty _PropertyOfAttribute, ShowIfAttribute _Attribute)
    {
        SerializedProperty refPropertyHolder = _PropertyOfAttribute.serializedObject.FindProperty(_Attribute.ParentName);

        if (refPropertyHolder == null)
        {
            return null;//didnt find an array
        }

        //search for refProperty array
        for (int i = 0; i < refPropertyHolder.arraySize; i++)
        {
            if (refPropertyHolder.GetArrayElementAtIndex(i).FindPropertyRelative(_Attribute.Name).propertyPath == _PropertyOfAttribute.propertyPath)
            {
                return refPropertyHolder.GetArrayElementAtIndex(i).FindPropertyRelative(_Attribute.Name);
            }
        }

        return null; //didnt find the right property in the array
    }

    public bool ReturnValue(SerializedProperty _RefProp, System.Object _CompareValue)
    {
        bool drawProperty = false;
        typeNotSupported = false;

        switch (_RefProp.type.ToLower())
        {
            default: typeNotSupported = true; return true;
            case "bool": if (_RefProp.boolValue == (bool)_CompareValue) drawProperty = true; else drawProperty = false; break;
            case "int": if (_RefProp.intValue == (int)_CompareValue) drawProperty = true; else drawProperty = false; break;
            case "float": if (_RefProp.floatValue == (float)_CompareValue) drawProperty = true; else drawProperty = false; break;
            case "double": if (_RefProp.doubleValue == (double)_CompareValue) drawProperty = true; else drawProperty = false; break;
            case "string": if (_RefProp.stringValue == (string)_CompareValue) drawProperty = true; else drawProperty = false; break;
            case "enum": if (_RefProp.enumValueIndex == (int)_CompareValue) drawProperty = true; else drawProperty = false; break;
        }

        if (hideINAttribute.Revert)
            return !drawProperty;
        else
            return drawProperty;
    }
}