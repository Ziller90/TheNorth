using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using SiegeUp.Core;

[System.Serializable]
public class UnitRangeKey : BlackboardKey<Range>
{
    [AutoSerialize(3)] public Range serializedValue;
}

[System.Serializable]
public class UnitRouteKey : BlackboardKey<Route>
{
    [AutoSerialize(3)] public Route serializedValue;
}
