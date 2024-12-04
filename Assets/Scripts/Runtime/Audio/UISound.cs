using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISound : MonoBehaviour
{
    [SerializeField] AudioClip customSound;

    private void Awake()
    {
#if UNITY_EDITOR
        if (GetComponents<UISound>().Length > 1)
            Debug.LogError($"UI Object {gameObject.name} has more then one UISound component! Please, remove one of them!");
#endif

        if (Application.isPlaying)
        {
            var buttons = GetComponents<Button>();
            var checkboxes = GetComponents<Toggle>();

            foreach (var button in buttons)
                button.onClick.AddListener(() => Game.ButtonsSoundManager.Play(customSound));

            foreach (var checkbox in checkboxes)
                checkbox.onValueChanged.AddListener((isOn) => Game.ButtonsSoundManager.Play(customSound));
        }
    }
}
