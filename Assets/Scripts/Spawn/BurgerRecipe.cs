using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BurgerRecipe : MonoBehaviour
{
    public static BurgerRecipe burgerRecipe;
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
    public Menu menu;
    Image panel;

    float Correctingre = 3;
    float ingreLost = 1;
    float ScoreCounter;
    void LoadMenuFromJson()
    {
        string menuJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Recipe_test.json");
        menu = JsonUtility.FromJson<Menu>(menuJson);
    }

    //Dictionary<string, int> remainingIngr;
    public class myTuple
    {
        public myTuple(string _name)
        {
            name = _name;
            numLeft = 1;
        }
        public string name;
        public int numLeft;
    }
    List<myTuple> remainingIngr;
    int ChooseRecipe()
    {
        return GameManager.gameManager.getRandNum(menu.BurgerMenu.Length);
    }
    public int curBurgerOrder;
    void GoNextRecipe()
    {
        curBurgerOrder = ChooseRecipe();
<<<<<<< Updated upstream

        Debug.Log("Recipe Changed");
=======
        loadRecipeToList(curBurgerOrder);
    }
    void loadRecipeToList(int burgerOrderInd)
    {
        foreach(string n in menu.BurgerMenu[burgerOrderInd].BurgerRecipe)
        {
            bool contains = false;
            for(int i = 0; i < remainingIngr.Count; i++)
            {
                if(remainingIngr[i].name == n)
                {
                    remainingIngr[i].numLeft += 1;
                    contains = true;
                }
            }
            if (!contains)
                remainingIngr.Add(new myTuple(n));
        }
>>>>>>> Stashed changes
    }
    void OnIngrObtained(string ingr_info)
    {
        bool contains = false;
        foreach(myTuple mt in remainingIngr)
        {
            if(mt.name == ingr_info && mt.numLeft > 0)
            {
                contains = true;
                mt.numLeft--;
                correctIngre();
            }
        }
        if (!contains)
        {
            ScoreCounter = 0;
            EventManager.eventManager.Invoke_BurgerCompleteEvent(false);
            GoNextRecipe();
            showEaten.ShowObtain.InitiateObj();//보여주기 오브젝트 초기화
            curBurgerOrderInd = 0;
            return;
        }

        bool empty = true;
        foreach (myTuple mt in remainingIngr)
        {
            if (mt.numLeft > 0)
                empty = false;
        }
        if (empty)
        {
            ScoreCounter += 1;
            DisplayScore.Instance.AddScore(ScoreCounter);
            GoNextRecipe();
            curBurgerOrderInd = 0;
            remainingIngr.Clear();
            ScoreCounter = 0;
            showEaten.ShowObtain.showEatenToUser("Bun");
        }
        else if(ingr_info != "Bun")
        {
            //먹었다고 표시
            showEaten.ShowObtain.showEatenToUser(ingr_info);
            //체력 추가
            HealthManager.Instance.addHealth(Correctingre);
        }
    }
    /*
    List<string> curIngrInventory;
    int curBurgerOrderInd = 0;
    void OnIngrObtained(string ingr_info)
    {
        //correct ingr
        if(menu.BurgerMenu[curBurgerOrder].BurgerRecipe[curBurgerOrderInd++] == ingr_info)
        {
            correctIngre();
            //end of recipe
            if(curBurgerOrderInd == menu.BurgerMenu[curBurgerOrder].BurgerRecipe.Length)
            {
                //점수합산
                ScoreCounter += 1;
                DisplayScore.Instance.AddScore(ScoreCounter);
                GoNextRecipe();
                curBurgerOrderInd = 0;
                curIngrInventory.Clear();
                ScoreCounter = 0;
                //먹었다고 표시
                showEaten.ShowObtain.showEatenToUser(ingr_info);
            }
            else
            {
                //먹었다고 표시
                showEaten.ShowObtain.showEatenToUser(ingr_info);
                //리스트에 삽입
                curIngrInventory.Add(ingr_info);
                //체력 추가
                HealthManager.Instance.addHealth(Correctingre);
            }
        }
        else
        {
            ScoreCounter = 0;
            EventManager.eventManager.Invoke_BurgerCompleteEvent(false);
            GoNextRecipe();
            showEaten.ShowObtain.InitiateObj();//보여주기 오브젝트 초기화
            curBurgerOrderInd = 0;
        }
    }
    */

    private void Awake()
    {
        LoadMenuFromJson();
        //curIngrInventory = new List<string>();
        remainingIngr = new List<myTuple>();
        burgerRecipe = this;
    }
    private void Start()
    {
        EventManager.eventManager.IngrObtainedEvent += OnIngrObtained;
        GoNextRecipe();
    }

    public void currrecTotop(ref string[] giveRecipie){
        giveRecipie = menu.BurgerMenu[curBurgerOrder].BurgerRecipe;
    }
    int curBurgerOrderInd = 0;
    public void correctIngre(){
        panel = GameObject.FindGameObjectWithTag("T_Panel"+(++curBurgerOrderInd)).GetComponent<Image>();
        panel.color = UnityEngine.Color.green;
    }
}
