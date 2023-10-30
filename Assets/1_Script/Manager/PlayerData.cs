using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoSingleton<PlayerData>
{
    /// <summary> 현재 내가 보유중인 캐릭터 </summary>
    public CharaterData[] PlayerCharater;

    private void Start()
    {
        foreach(CharaterData charater in PlayerCharater)
        {
            charater.SaveDefaultState();
        }
    }
    public void ReturnCharaterState()
    {
        foreach (CharaterData charater in PlayerCharater)
        {
            charater.ReturnState();
        }
    }
}
