/*********************
* Copyright (c) 2016, Rudolf Chrispens.  All rights reserved.
* found on http://www.rudolf-chrispens.de/posts/
* Copyrights licensed under the GNU License.
* https://www.gnu.org/licenses/gpl-3.0
***********************/

using UnityEngine;
using System;
using Object = System.Object;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]public class ShowIfAttribute : PropertyAttribute
{
    //the name of the attribute field
    public string Name
    {
        get; set;
    }

    public Object matchValue
    {
        get; set;
    }

    public string ParentName
    {
        get; set;
    }

    public bool Revert
    {
        get; set;
    }

    /// <summary>
    /// Short version.
    /// </summary>
    public ShowIfAttribute(string _PropertyName, Object _HideOnThisValue, bool _Revert = false)
    {
        Name = _PropertyName;
        matchValue = _HideOnThisValue;
        ParentName = "";
        Revert = _Revert;
    }

    /// <summary>
    /// Long version - Use this if the referenced property is capsulated.
    /// </summary>
    public ShowIfAttribute(string _PropertyName, Object _HideOnThisValue, string _RefParent, bool _Revert)
    {
        Name = _PropertyName;
        matchValue = _HideOnThisValue;
        ParentName = _RefParent;
        Revert = _Revert;
    }

    //Full list on:
    //https://msdn.microsoft.com/en-us/library/aa664615(v=vs.71).aspx
    public enum SupportedTypes
    {
        Bool = 0,
        Int,
        Float,
        Double,
        String,
        Enumeration
    }
}
