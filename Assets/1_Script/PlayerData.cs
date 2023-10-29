using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoSingleton<PlayerData>
{
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
