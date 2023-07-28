using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{

    public bool LoopShouldEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator GameLoop()
    {
        while(LoopShouldEnd == false)
        {
            //Spawn Enimies

            //Spawn Tower

            //Move Enemies

            //Tick Tower

            //Apply Effects

            //Damage Enemies

            //Remove Enemies

            //Remove Towers

            yield return null;
        }

    }
}
