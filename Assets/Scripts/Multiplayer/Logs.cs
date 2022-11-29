using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Logs : MonoBehaviour
{
    string myLog;
    string output;
    string stack;
    [SerializeField] TMP_Text logsText;
    private void Update()
    {
        logsText.text = myLog;
    }
    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }
    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        myLog = output + "\n" + myLog;
        if (myLog.Length > 5000)
        {
            myLog = myLog.Substring(0, 4000);
        }
    }
}





