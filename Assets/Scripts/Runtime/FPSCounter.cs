using SiegeUp.Core;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnityStandardAssets.Utility
{
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f;
        const string fpsFormat = "Fps {0}";

        int m_FpsAccumulator;
        float m_FpsNextPeriod;
        int m_CurrentFps;
        TMP_Text m_Text;

        Int64 totalFrames;
        double totalTime;

        public float GetAverageFps()
        {
            return (float)(totalFrames / totalTime);
        }

        void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            m_Text = GetComponent<TMP_Text>();
        }

        void Update()
        {
            totalTime += Time.deltaTime;
            totalFrames++;

            m_FpsAccumulator++;
            float diff = Time.realtimeSinceStartup - m_FpsNextPeriod;
            if (diff >= 0.0f)
            {
                m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod - diff % fpsMeasurePeriod;
                m_Text.text = string.Format(fpsFormat, m_CurrentFps);
            }
        }
    }
}