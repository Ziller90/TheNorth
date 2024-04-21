using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenuWindowView : WindowView
{
    [SerializeField] TMP_Dropdown qualitySettingsDropdown;

    public void Start()
    {
        List<TMP_Dropdown.OptionData> qualityLevels = new();

        foreach (var levelName in QualitySettings.names)
            qualityLevels.Add(new TMP_Dropdown.OptionData { text = levelName });

        qualitySettingsDropdown.AddOptions(qualityLevels);
        qualitySettingsDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
    }

    public void SetQUalitySettings(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
