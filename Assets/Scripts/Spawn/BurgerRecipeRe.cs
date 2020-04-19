using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerRecipeRe : MonoBehaviour
{
    public static BurgerRecipeRe BurgerRecipere;
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
    Image panel;

    float Correctingre = 3;
    float ingreLost = 1;
    float ScoreCounter;
    void LoadMenuFromJson()
    {
        string menuJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Recipe_test.json");
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

        Debug.Log("Recipe Changed");
    }

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

    private void Awake()
    {
        LoadMenuFromJson();
        curIngrInventory = new List<string>();
        BurgerRecipere = this;
    }
    private void Start()
    {
        EventManager.eventManager.IngrObtainedEvent += OnIngrObtained;
        GoNextRecipe();
    }

    public void currrecTotop(ref string[] giveRecipie){
        giveRecipie = menu.BurgerMenu[curBurgerOrder].BurgerRecipe;
    } 
    public void correctIngre(){
        panel = GameObject.FindGameObjectWithTag("T_Panel"+(curBurgerOrderInd)).GetComponent<Image>();
        panel.color = UnityEngine.Color.green;
    }
}
