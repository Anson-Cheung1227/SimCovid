using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Global Stat", menuName = "Scriptable Objects/Global Stat")]
public class GlobalStatsSO : ScriptableObject
{
    public float RecoveryRate;
    public float DeathRate;
}