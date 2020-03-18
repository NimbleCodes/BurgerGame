using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public string[] Patty = new string[4] { "MeatP", "Lobster", "Chickintender", "Bulgogi" };
    public string[] Vegy = new string[5] { "Lettus", "Onion", "Relish", "Pickle", "Potato" };
    public string[] BreadCheese = new string[3] { "BaseB", "UpperB", "Cheese" };


    public string[] Orders;
    public int Patnum = Random.Range(0,4);
    public int Vegnum = Random.Range(0,5);
    Spawner() {
            
            Orders = new string[5]{ BreadCheese[0], Vegy[Vegnum], Patty[Patnum], BreadCheese[1], BreadCheese[2] };
        }



}
