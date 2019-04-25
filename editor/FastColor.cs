using UnityEngine;
using UnityEditor;

public class FastColor
{
    private static Color _copiedColor = Color.clear;
    
    [MenuItem("Tools/FastColor/CopySpriteColor _F7")]
    public static void CopySpriteColor()
    {
        var sr = Selection.transforms[0].GetComponent<SpriteRenderer>();
        if (sr == null) return;
        _copiedColor = sr.color;
        EditorGUIUtility.systemCopyBuffer = ColorUtility.ToHtmlStringRGB(sr.color);
    }
    
    [MenuItem("Tools/FastColor/PasteSpriteColor _F8")]
    public static void PasteSpriteColor()
    {
        foreach (var selected in Selection.transforms)
        {
            var sr = selected.GetComponent<SpriteRenderer>();
            if (sr == null) return;
            Undo.RecordObject(sr, "change sprite color");
            sr.color = _copiedColor;
            EditorUtility.SetDirty(sr);
        }
    }
}