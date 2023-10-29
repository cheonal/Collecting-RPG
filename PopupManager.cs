using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject[] GO_Start;
    private void OnEnable()
    {
        int curStage = StageManager.instance.GetStage();

    }
}
