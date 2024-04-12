using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpener : MonoBehaviour
{
    [SerializeField] GameObject windowPrefab;
    [SerializeField] MonoBehaviour presentation;

    public void ShowWindow()
    {
        Game.WindowManagerView.ShowWindow(windowPrefab, presentation);
    }
}
