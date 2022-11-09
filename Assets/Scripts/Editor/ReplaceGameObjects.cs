using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC (edited by Ilya Zdorovtsov)
// March 2010

public class ReplaceGameObjects : ScriptableWizard
{
    public bool copyValues = true;
    public GameObject useGameObject;
    public List<GameObject> Replaces;

    [MenuItem("Custom/Replace GameObjects")]


    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
    }

    void OnWizardCreate()
    {
        foreach (GameObject t in Replaces)
        {
            GameObject newObject;
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(useGameObject);
            newObject.transform.position = t.transform.position;
            newObject.transform.rotation = t.transform.rotation;
            newObject.transform.localScale = t.transform.localScale;

            DestroyImmediate(t.gameObject);
        }

    }
}
