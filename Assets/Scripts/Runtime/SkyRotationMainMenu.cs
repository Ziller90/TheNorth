using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotationMainMenu : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    float rotation;

    void Update()
    {
        rotation += rotationSpeed;
        if (rotation > 360)
            rotation = 0;

        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}
