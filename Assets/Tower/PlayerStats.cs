using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int StartingMoney;
    private int CurrentMoney;
    // Start is called before the first frame update
    private void Start()
    {
        CurrentMoney = StartingMoney;
    }

    public void AddMoney(int MoneyToAdd)
    {
        CurrentMoney += MoneyToAdd;
    }

    public int GetMoney()
    {
        return CurrentMoney;
    }

    
    
}
