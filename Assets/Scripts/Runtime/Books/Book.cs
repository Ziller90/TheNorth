using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextSpace
{
    [TextArea]
    public string text;    
}

public class Book : MonoBehaviour
{
    [TextArea, SerializeField] string text;

    [SerializeField] BookWindowView bookView;

    public BookWindowView BookView => bookView;
    public string Text => text;
}
