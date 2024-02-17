using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class UnitContext : ContextBase
{
    public UnitContext(GameObject gameObject) : base(gameObject) { }

    [SerializeField] Sensors sensors;
    [SerializeField] AINavigationManager navigationManager;

    public Sensors Sensors => sensors;
    public AINavigationManager NavigationManager => navigationManager;

    public override void AddAddictiveContext(GameObject gameObject)
    {
        sensors = gameObject.GetComponent<Sensors>();
        navigationManager = gameObject.GetComponent<AINavigationManager>();
    }
}
