using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.Animations;
using UnityEditor;
using System;

using Object = UnityEngine.Object;

public class RenameWindow : EditorWindow
{
    public string CaptionText;
    public string ButtonText;
    public string NewName;
    public Action<string> OnClickButton;

    private void OnGUI()
    {
        NewName = EditorGUILayout.TextField(CaptionText, NewName);
        if (GUILayout.Button(ButtonText))
        {
            if (OnClickButton != null)
                OnClickButton(NewName);
            Close();
            GUIUtility.ExitGUI();
        }
    }

}

public class NestedAnimationCreator : MonoBehaviour {

    [MenuItem("Assets/Create/Nested Animation")]
    public static void Create()
    {
        RuntimeAnimatorController selectedAnimatorController =
            Selection.activeObject as AnimatorController;

        if(selectedAnimatorController == null)
            return;

        RenameWindow renameWindow =
            EditorWindow.GetWindow<RenameWindow>("Create Nested Animation");

        renameWindow.CaptionText = "New Animation Name";
        renameWindow.NewName = "New Clip";
        renameWindow.ButtonText = "Create";

        renameWindow.OnClickButton = (string newName) =>
        {
            if (string.IsNullOrEmpty(newName))
                return;
            
            AnimationClip animationClip = AnimatorController.AllocateAnimatorClip(newName);

            AssetDatabase.AddObjectToAsset(animationClip, selectedAnimatorController);

            AssetDatabase.ImportAsset(
                AssetDatabase.GetAssetPath(selectedAnimatorController));
        };
    }

    [MenuItem("Assets/Delete Sub Asset")]
    public static void Delete()
    {
        Object[] selectedAssets = Selection.objects;
        if (selectedAssets.Length < 1)
            return;

        foreach (Object asset in selectedAssets)
        {
            if (AssetDatabase.IsSubAsset(asset))
            {
                string path = AssetDatabase.GetAssetPath(asset);
                DestroyImmediate(asset, true);
                AssetDatabase.ImportAsset(path);
            }
        }
    }
}
