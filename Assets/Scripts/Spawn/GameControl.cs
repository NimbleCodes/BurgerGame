  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//GameController에서 이용되는 스크립트. GameController에는 ingrePool Component가 함께 존재해야 한다.

public class GameControl : MonoBehaviour
{   
    [System.Serializable]
    class IngreArr{
        //재료의 분류 (고기, 야채, 소스 등등)
        public string ingreClass;
        //재료의 이름
        public string ingreName;
        //재료의 Sprite 경로
        public Sprite ingreSprite;
    }

    [System.Serializable]
    class ingreTable{
        public IngreArr[] IngreArr;
    }


    
    string ingreJson;   
    ingreTable IngreTable;

    //오브젝트 풀링을 위해 코드 짜기


    // Start is called before the first frame update
    void Awake()
    {
        //Json 파일 받기
        getJson();

        //Json 파일 게임에서 쓸 수 있게 Table로 변환.
        IngreTable = JsonUtility.FromJson<ingreTable>(ingreJson);
        
        //ingreTable 각자에 맞는 Sprite를 Resources 폴더에서 가져오도록 한다.
        int index = 0;

        foreach(var cell in IngreTable.IngreArr){
            cell.ingreSprite = Resources.Load<Sprite>("Sprites/" + cell.ingreName);
            
            for(int i=0; i<5; i++){
                GameObject tempObj = Instantiate((GameObject)Resources.Load("Prefab/IngrePrefab"));
                tempObj.name = cell.ingreName + i;
                tempObj.GetComponent<Ingredient>().initIngre(cell.ingreClass,cell.ingreName);
            }
            
            index++;
        }
        

    }

    void getJson(){
        ingreJson = File.ReadAllText(Application.dataPath + "/Resources/Json/ingredient.json");
    }
}