using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BurgerRecipe : MonoBehaviour
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

    class recipeCell{

    }

    //recipeTable은 이전에 나왔던 버거 레시피가 다시 나오는 것을 방지하기 위해 이용된다.
    bool[] recipeTable;
    //saveNum은 나왔던 레시피가 안나오게 하는 Term을 지정한다. (ex 3이면 다음 세 텀에는 중복 레시피가 나오지 않는다.)
    int saveNum;
    //curRecipe는 게임플레이 내에서의 현재 레시피를 가리킨다.
    string[] curRecipe;
    //correctionTable은 재료들이 모두 모였는지를 확인한다.
    bool[] correctionTable;

    private void Awake() {
        loadMenuFromJson();
        Debug.Log("Count : " + getRecipeCount());
        recipeTable = new bool[getRecipeCount()];
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    //int num에 맞는 recipe를 받는다.(Random 값으로 받게 하기 위함이다.)
    public string[] getRecipe(int num){
        return menu.BurgerMenu[num].BurgerRecipe;
    }

    //레시피의 개수를 받는다. 후에 Random 값의 최고치를 알기 위해 사용된다.
    public int getRecipeCount(){
        return menu.BurgerMenu.Length;
    }


    public void setRandomRecipe(){
        
    }

    //레시피 Json 받아오는 function
    void loadMenuFromJson()
    {
        string menuJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Recipe.json");
        menu = JsonUtility.FromJson<Menu>(menuJson);
    }
}
