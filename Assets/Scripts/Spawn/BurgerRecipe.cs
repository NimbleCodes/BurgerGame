using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerRecipe : MonoBehaviour
{
    public static BurgerRecipe burgerRecipe;
    [System.Serializable]
    public class burgerMenu
    {
        public string Character;
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
    float ScoreCounter;
    void LoadMenuFromJson()
    {
        string menuJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Recipe.json");
        menu = JsonUtility.FromJson<Menu>(menuJson);
    }

    int ChooseRecipe()
    {
        int ret = curBurgerOrder;
        while(ret == curBurgerOrder)
            ret = GameManager.gameManager.getRandNum(menu.BurgerMenu.Length);
        return ret;
    }
    public int curBurgerOrder;
    void GoNextRecipe()
    {
        curBurgerOrder = ChooseRecipe();
    }

    List<string> curIngrInventory;
    int curBurgerOrderInd = 0;
    void OnIngrObtained(string ingr_info)
    {
        //correct ingr
        if(menu.BurgerMenu[curBurgerOrder].BurgerRecipe[curBurgerOrderInd] == ingr_info)
        {
            correctIngre();
            //end of recipe
            if(curBurgerOrderInd+1 == menu.BurgerMenu[curBurgerOrder].BurgerRecipe.Length)
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
                curBurgerOrderInd++;
            }
        }
        else
        {
            ScoreCounter = 0;
            //여기서 애니메이션
            GoNextRecipe();
            EventManager.eventManager.Invoke_BurgerCompleteEvent(false);
            showEaten.ShowObtain.InitiateObj();//보여주기 오브젝트 초기화
            curBurgerOrderInd = 0;
        }
    }

    private void Awake()
    {
        LoadMenuFromJson();
        curIngrInventory = new List<string>();
        burgerRecipe = this;
        GoNextRecipe();
    }
    private void Start()
    {
        EventManager.eventManager.IngrObtainedEvent += OnIngrObtained;
    }
    //현재 레시피 보내주기
    public void currrecTotop(ref string[] giveRecipie){
        giveRecipie = menu.BurgerMenu[curBurgerOrder].BurgerRecipe;
    } 
    //맞는 재료를 먹었을때 패널 색상변경
    public void correctIngre(){
        panel = GameObject.FindGameObjectWithTag("T_Panel"+(curBurgerOrderInd+1)).GetComponent<Image>();
        panel.color = UnityEngine.Color.green;
    }
    //캐릭터 이름 받아오기
    public void currentChar(ref string CharName){
        CharName = menu.BurgerMenu[curBurgerOrder].Character;
    }
}
