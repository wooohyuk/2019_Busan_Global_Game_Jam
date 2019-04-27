using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

public class LoadStartScene : MonoBehaviour {

    private const string startSceneName = "TitleScene.unity";

    [MenuItem("Play/PlayStartScene %h")]
    public static void PlayStartScene()
    {
        if (EditorSceneManager.GetActiveScene().isDirty)
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        string scenePath = "Assets/Scenes/" + startSceneName;
        EditorSceneManager.OpenScene(scenePath);
        EditorApplication.isPlaying = true;
    }

}
