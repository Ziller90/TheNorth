using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE
}

[Serializable]
public class Node
{
    [SerializeReference, SubclassSelector] protected List<Node> children = new List<Node>();

    protected AIBehaviourTree tree;
    protected Node parent;
    protected NodeState state;

    public virtual NodeState Evaluate() => NodeState.FAILURE;

    public void Initialize(AIBehaviourTree hostTree, Node parent)
    {
        this.tree = hostTree;
        this.parent = parent;

        OnInitialize();
        InitializeChildren();
    }

    public virtual void OnInitialize() { }

    public void InitializeChildren()
    {
        foreach (Node child in children)
            InitializeChild(child);
    }

    private void InitializeChild(Node childNode)
    {
        childNode.Initialize(tree, this);
    }
}

