using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    private int baseLevel = 1;
    public GameObject Coinn;
    private GameObject _coin;

    // Start is called before the first frame update
   
    void Update()
    {
        if(_coin == null && GameLoopManager.TotalCoin == 0)
        {
            _coin = Instantiate(Coinn,new Vector3(91.51f, -5.89f, 82.63454f), Quaternion.identity);
            StartCoroutine(Coin());
        }
        else if(_coin != null && GameLoopManager.TotalCoin == 500)
        {
            StopAllCoroutines();
        }
        Debug.Log(GameLoopManager.TotalCoin);
    }
    IEnumerator Coin()
    {
        yield return new WaitForSeconds(5);
        GameLoopManager.TotalCoin += (100 * baseLevel);
        StartCoroutine(Coin());

    } 
}
