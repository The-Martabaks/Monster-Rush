using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
   public GameObject Panel_1;
   public GameObject Panel_2;
   public int Manager;

   public void ManageCanvas()
   {
        if(Manager == 0)
        {
            Pan_2();
            Manager = 1;
        }
        else
        {
            Pan_1();
            Manager = 0;
        }
   }

   void Pan_1()
   {
    Panel_1.SetActive(true);
    Panel_2.SetActive(false);
   }

   void Pan_2()
   {
    Panel_1.SetActive(false);
    Panel_2.SetActive(true);
   }
}
