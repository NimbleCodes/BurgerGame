using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//GameController에서 이용되는 스크립트. GameController에는 ingrePool Component가 함께 존재해야 한다.

[System.Serializable]
public class ingredient{
    //재료의 분류 (고기, 야채, 소스 등등)
    public string ingreClass;
    //재료의 이름
    public string ingreName;
    //재료의 Sprite 경로
    public Sprite ingreSprite;
}

[System.Serializable]
public class ingreTable{
    public ingredient[] Ingredient;
}

public class GameControl : MonoBehaviour
{   

    
    string ingreJson;   
    ingreTable IngreTable;
    //오브젝트 풀링을 위해 ingrePool의 Component를 받아오도록 한다.
    public ingrePool objPool;
    //각 재료의 Class에 맞추어 게임오브젝트를 만들어 그 안에서 Child 로 clone 들을 관리한다.
    public GameObject[] meatArr, vegeArr, sauceArr;
    // Start is called before the first frame update
    void Awake()
    {
        //Json 파일 받기
        getJson();
        //Json 파일 게임에서 쓸 수 있게 Table로 변환.
        IngreTable = JsonUtility.FromJson<ingreTable>(ingreJson);
        //objPool 초기화
        objPool = GetComponent<ingrePool>();
        objPool.poolArray = new GameObject[IngreTable.Ingredient.Length,10];
        
        //ingreTable 각자에 맞는 Sprite를 Resources 폴더에서 가져오도록 한다.
        int index = 0; 
        foreach(var cell in IngreTable.Ingredient){
            cell.ingreSprite = Resources.Load<Sprite>("Sprites/" + cell.ingreName);
            for(int i=0; i<5; i++){
                objPool.poolArray[index, i] = Instantiate((GameObject)Resources.Load("Prefab/IngrePrefab"));
                objPool.poolArray[index, i].GetComponent<IngreClass>().setIngrValue(cell);
                objPool.poolArray[index, i].name = cell.ingreName + i;
            }
            
            index++;
        }
        

    }

    void getJson(){
        ingreJson = File.ReadAllText(Application.dataPath + "/Resources/Json/ingredient.json");
    }
}
