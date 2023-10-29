using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 2)]
public class StageEnemyData : ScriptableObject
{
    public CharaterData[] Enemy;
}
