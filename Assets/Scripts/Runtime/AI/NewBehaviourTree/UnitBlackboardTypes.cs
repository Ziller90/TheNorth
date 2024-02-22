using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class UnitBlackboardTypes : MonoBehaviour
{
    [System.Serializable]
    public class RangeKey : BlackboardKey<Range> { }

    [System.Serializable]
    public class RouteKey : BlackboardKey<Route> { }
}
