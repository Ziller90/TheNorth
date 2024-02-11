using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AIBehaviourTree : MonoBehaviour
{
    [SerializeReference, SubclassSelector] List<BlackboardKey> blackBoard;

    [SerializeField] AINavigationManager navigationManager;
    [SerializeField] ActionManager actionManager;
    [SerializeField] Sensors sensors;

    public ActionManager ActionManager { get => actionManager; set => actionManager = value; }
    public AINavigationManager NavigationManager { get => navigationManager; set => navigationManager = value; }
    public Sensors Sensors { get => sensors; set => sensors = value; }

    [SerializeReference, SubclassSelector] Node root;

    public Action validated;

    public object GetBlackboardValue(string id)
    {
        foreach (BlackboardKey key in blackBoard)
        {
            if (key.Id == id)
            {
                return key.GetValue();
            }
        }
        return null;
    }

    public object GetBlackboardValue(BlackboardKey key)
    {
        return GetBlackboardValue(key.Id) ?? key.GetValue();
    }

    public void SetBlackBoardKeyValue(BlackboardKey key, object value)
    {
        var blackBoardKey = GetBlackboardKey(key);
        blackBoardKey.SetValue(value);
    }

    public BlackboardKey GetBlackboardKey(BlackboardKey key)
    {
        foreach (BlackboardKey blackBoardKey in blackBoard)
        {
            if (key.Id == blackBoardKey.Id)
            {
                return blackBoardKey;
            }
        }
        return null;
    }

    void Start()
    {
        root.Initialize(this, null);
    }

    void Update() 
    {
        if (root != null)
            root.Evaluate();
    }

    private void OnValidate()
    {
        root.SetName();
    }
}
