using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(WorldSelectorAttribute))]
public class WorldSelectorAttributeDrawer : PropertyDrawer {


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var options = AssetDatabase.GetSubFolders(WorldObjectProvider.AssetsWorldsPath)
            .Select(_ =>  _.Replace(WorldObjectProvider.AssetsWorldsPath + "/", ""))
            .ToList();

        var selectedIndex = options.IndexOf(property.stringValue);
        property.stringValue = options.ElementAtOrDefault(EditorGUI.Popup(position, label.text, selectedIndex, options.ToArray()));
    }
}
