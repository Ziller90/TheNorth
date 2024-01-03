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
    protected NodeState state;

    [SerializeField] protected Node parent;
    [SerializeReference, SubclassSelector] protected List<Node> children = new List<Node>();

    public void SetParent(Node parent)
    {
        this.parent = parent;
    }

    public Node()
    {
        SetParent(null);
    }

    public Node(List<Node> children)
    {
        foreach (Node child in children)
            Attach(child);
    }

    private void Attach(Node node)
    {
        node.SetParent(this);
        children.Add(node);
    }

    public virtual NodeState Evaluate() => NodeState.FAILURE;
}

