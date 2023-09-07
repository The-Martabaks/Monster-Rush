using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    private int baseLevel = 1;
    public GameObject Coinn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coin());
    }

    IEnumerator Coin()
    {
        yield return new WaitForSeconds(10);
        Instantiate(Coinn);
        GameLoopManager.TotalCoin += 100 * baseLevel;
        StartCoroutine(Coin());

    } 
}
