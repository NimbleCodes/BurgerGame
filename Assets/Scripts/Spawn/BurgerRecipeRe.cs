using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerRecipeRe : MonoBehaviour
{
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
    void LoadMenuFromJson()
    {
        string menuJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Recipe.json");
        menu = JsonUtility.FromJson<Menu>(menuJson);
    }

    int ChooseRecipe()
    {
        return GameManager.gameManager.getRandNum(menu.BurgerMenu.Length);
    }
    int curBurgerOrder;
    void GoNextRecipe()
    {
        curBurgerOrder = ChooseRecipe();
        Debug.Log(menu.BurgerMenu[curBurgerOrder].BurgerName);
    }

    List<string> curIngrInventory;
    int curBurgerOrderInd = 0;
    void OnIngrObtained(string ingr_info)
    {
        //correct ingr
        if(menu.BurgerMenu[curBurgerOrder].BurgerRecipe[curBurgerOrderInd++] == ingr_info)
        {
            //end of recipe
            if(curBurgerOrderInd == menu.BurgerMenu[curBurgerOrder].BurgerRecipe.Length)
            {
                EventManager.eventManager.Invoke_BurgerCompleteEvent(true);
                GoNextRecipe();
                curBurgerOrder = 0;
                curIngrInventory.Clear();
            }
            else
            {
                //먹었다고 표시

                //리스트에 삽입
                curIngrInventory.Add(ingr_info);
            }
        }
        else
        {
            EventManager.eventManager.Invoke_BurgerCompleteEvent(false);
            GoNextRecipe();
            curBurgerOrderInd = 0;
        }
    }

    private void Awake()
    {
        LoadMenuFromJson();
        curIngrInventory = new List<string>();
    }
    private void Start()
    {
        EventManager.eventManager.IngrObtainedEvent += OnIngrObtained;
        GoNextRecipe();
    }
}
