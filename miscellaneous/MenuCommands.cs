using UnityEngine;
using UnityEditor;

public class CreateMaterialExample : MonoBehaviour
{
    [MenuItem("GameObject/InsertAnimationClip")]
    static void IAC()
    {
        string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
        AnimationClip animationClip = new AnimationClip();
        animationClip.name = "" + Random.Range(1000,10000);
        AssetDatabase.AddObjectToAsset(animationClip, path);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(animationClip));
    }

    [MenuItem("GameObject/MeshRendererSortingOrderUp")]
    static void SOU()
    {
        MeshRenderer mrenderer = Selection.activeGameObject.GetComponent<MeshRenderer>();
        if (mrenderer != null) mrenderer.sortingOrder++;
    }

    [MenuItem("GameObject/MeshRendererSortingOrderDown")]
    static void SOD()
    {
        MeshRenderer mrenderer = Selection.activeGameObject.GetComponent<MeshRenderer>();
        if (mrenderer != null) mrenderer.sortingOrder--;
    }
}
