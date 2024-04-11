using SiegeUp.Core;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "TheNorth/ApplicationServicesConfig", fileName = "ApplicationServicesConfig")]
public class ApplicationServicesConfig : ScriptableObject
{
    [SerializeField]
    GameObject[] prefabs;

    [SerializeField]
    ScriptableObject[] scriptableObjects;

    public GameObject[] Prefabs => prefabs;

    void Initialize()
    {
        Debug.Log($"Initializing services in {(Application.isPlaying ? "Play Mode" : "Editor Mode")}");
        var serviceType = typeof(Service<>);
        string setInstanceMethodName = nameof(Service<Object>.RegisterInstance);
        string instancePropertyName = nameof(Service<Object>.Instance);
        foreach (var scriptableObject in scriptableObjects)
        {
            var serviceGenericType = serviceType.MakeGenericType(scriptableObject.GetType());
            if (serviceGenericType.GetProperty(instancePropertyName).GetValue(null) == null)
                serviceGenericType.GetMethod(setInstanceMethodName).Invoke(null, new[] { scriptableObject });
        }
    }

    void OnEnable()
    {
        Initialize();
    }

#if UNITY_EDITOR
    public static T LoadScriptableObjectInstance<T>() where T : Object
    {
        string typeName = typeof(T).Name;

        string[] assetGUIDs = UnityEditor.AssetDatabase.FindAssets($"t:{typeName}");

        if (assetGUIDs.Length > 1)
        {
            Debug.LogError($"More than one {typeName} found in project");
            return null;
        }

        if (assetGUIDs.Length == 0)
        {
            Debug.LogError($"No {typeName} found in project");
            return null;
        }

        string assetGUID = assetGUIDs[0];
        string path = UnityEditor.AssetDatabase.GUIDToAssetPath(assetGUID);
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
    }

    [InitializeOnLoadMethod]
    static void InitializeOnLoadInEditor()
    {
        Debug.Log("Editor Services initialization");
        var instance = LoadScriptableObjectInstance<ApplicationServicesConfig>();
        instance.Initialize();
    }
#endif
}