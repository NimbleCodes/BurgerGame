using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        public recipeCell(){

            termRemain = 0;
        }
        public bool used;
        int _termRemain;
        public int termRemain{
            get{
                return _termRemain;
            }
            set{
                if(value <= 0){
                    _termRemain = 0;
                    used = false;
                }else{
                    _termRemain = value;
                    used = true;
                }
            }
        }
    }

    //recipeTable은 이전에 나왔던 버거 레시피가 다시 나오는 것을 방지하기 위해 이용된다.
    recipeCell[] recipeTable;
    //saveNum은 나왔던 레시피가 안나오게 하는 Term을 지정한다. (ex 3이면 다음 세 텀에는 중복 레시피가 나오지 않는다.)
    public int saveNum = 3;
    //curRecipe는 게임플레이 내에서의 현재 레시피를 가리킨다.
    string[] curRecipe;
    //correctionTable은 재료들이 모두 모였는지를 확인한다.
    bool[] correctionTable;
    //curRecipe의 index
    int RecipeIndex = 0;
    Image panel;
    public static BurgerRecipe burgerRec;
    RecipieonTop recipieonTop;
    private void Awake() {
        
        loadMenuFromJson();
        burgerRec = this;
        
        //Debug.Log("Count : " + getRecipeCount());
        recipeTable = new recipeCell[getRecipeCount()];
        for(int i=0; i<getRecipeCount(); i++){
            recipeTable[i] = new recipeCell();
        }
        goNextRecipe();
    }
    private void Start() {
        EventManager.eventManager.IngrObtainedEvent += OnIngrEaten;

        
    }
    // Update is called once per frame
    void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            goNextRecipe();
        }
    }

    //overlapbox 에서 재료를 받으면 발생되는 이벤트
    void OnIngrEaten(string ingr_info)
    {
        findIngreCorrect(ingr_info);
    }
    //int num에 맞는 recipe를 받는다.(Random 값으로 받게 하기 위함이다.)
    public string[] getRecipe(int num){
        return menu.BurgerMenu[num].BurgerRecipe;
    }
    //레시피의 개수를 받는다. 후에 Random 값의 최고치를 알기 위해 사용된다.
    public int getRecipeCount(){
        return menu.BurgerMenu.Length;
    }
    //들어온 재료가 curRecipe의 순서와 알맞은지 확인하는 function
    public void findIngreCorrect(string ingreName){
        //틀렸을 때
        if(curRecipe[RecipeIndex] != ingreName){
            burgerFail();
            showEaten.ShowObtain.InitiateObj();
            RecipeIndex = 0;
            //틀렸으므로 다음 레시피로 넘기기
            goNextRecipe();
        }
        //알맞은 재료를 먹었을 때
        else if(curRecipe[RecipeIndex] == ingreName && RecipeIndex < curRecipe.Length-1){
            correctIngre();
            showEaten.ShowObtain.showEatenToUser(ingreName);
            RecipeIndex += 1;
        }
        //버거가 완성되었을 때
        if(RecipeIndex == curRecipe.Length){
            burgerComplete();
            RecipeIndex = 0;
            //완성되었으므로 다음 레시피로 넘기기
            goNextRecipe();
        }
    }
    //다음 레시피로 넘어가기(랜덤)
    public void goNextRecipe(){
        List<int> tempList = new List<int>();
        for(int i=0; i<getRecipeCount(); i++)
        {
            if(recipeTable[i].used == false){
                tempList.Add(i);
            }
            if(recipeTable[i].termRemain != 0){
                recipeTable[i].termRemain -= 1;
            }
        }
        int randNum = Random.Range(0,tempList.Count);
        Debug.Log("list num : " + tempList[randNum]);
        curRecipe = menu.BurgerMenu[tempList[randNum]].BurgerRecipe;
        recipeTable[tempList[randNum]].termRemain = saveNum;
        Debug.Log("Name : " + menu.BurgerMenu[tempList[randNum]].BurgerName);
        
    }
    //버거가 모두 완성되었음을 알리는 함수
    public void burgerComplete(){
        EventManager.eventManager.Invoke_BurgerCompleteEvent(true);
    }
    //버거가 실패했음을 알리는 함수
    public void burgerFail(){
        EventManager.eventManager.Invoke_BurgerCompleteEvent(false);
    }
    //알맞은 재료를 먹을시 오른쪽 상단에 먹은 재료를 표시
    public void correctIngre(){
        panel = GameObject.FindGameObjectWithTag("T_Panel"+(RecipeIndex+1)).GetComponent<Image>();
        panel.color = UnityEngine.Color.green;
    }
    //현재 레시피를 받아 맞는 이름의 스프라이트를 스폰해 눈으로 레시피 확인시켜준다
    public void currrecTotop(ref string[] giveRecipie){
        giveRecipie = new string[curRecipe.Length];
        for (int i = 0; i < giveRecipie.Length; i++){
            giveRecipie[i] = curRecipe[i];
        }
    } 
    //레시피 Json 받아오는 function
    void loadMenuFromJson()
    {
        string menuJson = File.ReadAllText(Application.dataPath + "/Resources/Json/Recipe.json");
        menu = JsonUtility.FromJson<Menu>(menuJson);
    }
}
