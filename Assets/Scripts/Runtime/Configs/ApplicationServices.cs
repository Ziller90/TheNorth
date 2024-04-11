using SiegeUp.Core;
using UnityEngine;

[DefaultExecutionOrder(ExecutionOrder)]
public class ApplicationServices : MonoBehaviour
{
    public const int ExecutionOrder = -500;

    [SerializeField]
    ApplicationServicesConfig applicationServicesConfig;

    [SerializeField]
    GameObject[] sceneServices;

    static GlobalServicesRoot globalServicesRoot;

    readonly System.Type serviceType = typeof(Service<>);
    const string setInstanceMethodName = nameof(Service<Object>.RegisterInstance);
    const string instancePropertyName = nameof(Service<Object>.Instance);

    public static void ReloadServices()
    {
        if (!globalServicesRoot)
            globalServicesRoot = FindObjectOfType<GlobalServicesRoot>();
        Destroy(globalServicesRoot.gameObject);
    }

    void Awake()
    {
        foreach (var prefab in applicationServicesConfig.Prefabs)
            RegisterServices(prefab, null, false);

        foreach (var sceneObject in sceneServices)
            RegisterServices(sceneObject, sceneObject, true);
    }

    void RegisterServices(GameObject prefab, GameObject instantiatedObj, bool overwrite)
    {
        if (!globalServicesRoot)
            globalServicesRoot = FindObjectOfType<GlobalServicesRoot>();
        if (!globalServicesRoot)
        {
            globalServicesRoot = new GameObject("GlobalServicesRoot").AddComponent<GlobalServicesRoot>();
            DontDestroyOnLoad(globalServicesRoot.gameObject);
        }
        foreach (var component in prefab.GetComponents<MonoBehaviour>())
        {
            var serviceGenericType = serviceType.MakeGenericType(component.GetType());
            if (overwrite || !(serviceGenericType.GetProperty(instancePropertyName).GetValue(null) as Object))
            {
                if (!instantiatedObj)
                {
                    instantiatedObj = Instantiate(prefab, globalServicesRoot.transform);
                }
                var realComponent = instantiatedObj.GetComponent(component.GetType());
                serviceGenericType.GetMethod(setInstanceMethodName).Invoke(null, new[] { realComponent });
            }
        }
    }
}
