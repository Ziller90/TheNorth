using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetDirection(float Horizontal, float Vertical, float FixAngle)
    {
        Quaternion FixQuaternion = Quaternion.Euler(0, FixAngle, 0);
        Vector3 Direction = Vector3.ClampMagnitude(new Vector3(Horizontal, 0, Vertical), 1);
        Direction = FixQuaternion * Direction;
        return Direction;
    }
    public static float Round(float Number, int RoundIndex)
    {
        float temp = Number * RoundIndex;
        temp = (int)temp;
        temp = temp / RoundIndex;
        return (temp);
    }
    public static float SpeedConverter(float kmPerHour)
    {
        return (kmPerHour * 1000f / 3600f) / 50f;
    }
}
