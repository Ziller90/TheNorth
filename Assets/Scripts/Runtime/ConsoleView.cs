using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleView : MonoBehaviour
{
    [SerializeField] int messageSizeLimit = 300;
    [SerializeField] Transform list;
    [SerializeField] Font font;
    [SerializeField] int fontSize;

    private void Start()
    {
        Debug.Log("{EHWRE");
        Debug.Log("{EHWRE");
        Debug.Log("{EHWRE");
    }
    void OnEnable()
    {
        Application.logMessageReceived += OnMessageAdded;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= OnMessageAdded;
    }

    void OnMessageAdded(string message, string stackTrace, LogType logType)
    {
        AddMessage(message, stackTrace, logType, false);
    }

    void AddMessage(string message, string stackTrace, LogType logType, bool reverse)
    {
        var textObject = new GameObject("Log");
        var textComponent = textObject.AddComponent<Text>();
        textComponent.color =
            logType == LogType.Log ? Color.white :
            logType == LogType.Warning ? Color.yellow :
            Color.red;

        if (message.Length > messageSizeLimit)
        {
            textComponent.text = message.Substring(0, messageSizeLimit);
        }
        else
        {
            textComponent.text = message;
        }

        if (logType != LogType.Log && logType != LogType.Warning)
        {
            textComponent.text += "\n" + StackTraceSubstr(stackTrace);
        }

        textComponent.fontSize = fontSize;
        textComponent.font = font;
        var contentSizeFitter = textObject.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        textObject.transform.SetParent(list, false);
        textObject.transform.localPosition = Vector3.zero;
        textObject.transform.localScale = Vector3.one;
        textObject.transform.rotation = Quaternion.identity;
        if (reverse)
            textObject.transform.SetAsFirstSibling();
    }

    string StackTraceSubstr(string stackTrace)
    {
        if (stackTrace.Length < messageSizeLimit)
            return stackTrace;
        return stackTrace.Substring(0, messageSizeLimit) + "...";
    }
}
