using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    string cur_burgerOrder;
    bool newBurgerOrder = true;
    public float newBurgerOrderTime = 10f;
    System.Random rand;

    string inventory = "";

    bool click = false;

    [System.Serializable]
    public class burgerMenu
    {
        public string BurgerName;
        public string[] BurgerRecipe;
    }
    [System.Serializable]
    public class Menu
    {
        public burgerMenu[] BurgerMenu;
    }
    Menu menu;

    private void Start()
    {
        rand = new System.Random();
        EventManager.eventManager.IngrEatenEvent += OnIngrEaten;

        loadMenuFromJson();

        cur_burgerOrder = createNewBurgerOrder();
        newBurgerOrder = false;
        StartCoroutine("newBurgerOrderTimer");
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) & !click)
        {
            bool success = cmpToBurgerOrder(inventory);
            inventory = "";
            EventManager.eventManager.Invoke_BurgerCompleteEvent(success);
            click = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) & click)
        {
            click = false;
        }

        if (newBurgerOrder)
        {
            cur_burgerOrder = createNewBurgerOrder();
            newBurgerOrder = false;
            StartCoroutine("newBurgerOrderTimer");
        }
    }

    void OnIngrEaten(string ingr_info)
    {
        inventory += (ingr_info + ", ");
        Debug.Log(inventory);
    }

    string createNewBurgerOrder()
    {
        int nextBurgerInd = rand.Next(0,menu.BurgerMenu.Length);
        Debug.Log(menu.BurgerMenu[nextBurgerInd].BurgerName);
        Debug.Log(menu.BurgerMenu[nextBurgerInd].BurgerRecipe[0]);
        return menu.BurgerMenu[nextBurgerInd].BurgerRecipe[0];
    }
    bool cmpToBurgerOrder(string burger_info)
    {
        if (cur_burgerOrder.Equals(burger_info))
            return true;
        return false;
    }
    IEnumerator newBurgerOrderTimer()
    {
        yield return new WaitForSeconds(newBurgerOrderTime);
        newBurgerOrder = true;
    }

    void loadMenuFromJson()
    {
        string menuJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Recipe.json");
        menu = JsonUtility.FromJson<Menu>(menuJson);
    }
}
