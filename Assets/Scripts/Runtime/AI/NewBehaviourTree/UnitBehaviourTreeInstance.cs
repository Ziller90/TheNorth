using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class UnitBehaviourTreeInstance : BehaviourTreeInstanceBase
{
    public override ContextBase CreateBehaviourTreeContext()
    {
        return new UnitContext(gameObject);
    }
}
