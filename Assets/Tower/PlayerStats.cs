using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyDisplayText;
    [SerializeField] private int StartingMoney;
    private int CurrentMoney;
    // Start is called before the first frame update
    private void Start()
    {
        CurrentMoney = StartingMoney;
        MoneyDisplayText.SetText($"${StartingMoney}");
    }

    public void AddMoney(int MoneyToAdd)
    {
        CurrentMoney += MoneyToAdd;
        MoneyDisplayText.SetText($"${StartingMoney}");
    }

    public int GetMoney()
    {
        return CurrentMoney;
    }

    
    
}
