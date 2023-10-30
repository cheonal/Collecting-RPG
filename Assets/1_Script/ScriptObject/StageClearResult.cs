using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New MapData", menuName = "MapData", order =0)]
public class StageClearResult : ScriptableObject
{
    [SerializeField] private GameObject[] GO_ResultImage;
    public GameObject[] Result => GO_ResultImage;
}
