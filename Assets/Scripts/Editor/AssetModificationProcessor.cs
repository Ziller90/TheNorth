using UnityEditor;

internal class MyAllPostprocessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets.Length > 0 || deletedAssets.Length > 0)
        {
            Game.ScriptableObjectManager?.UpdateList();
            Game.PrefabManager?.UpdatePrefabManager();
        }
    }
}