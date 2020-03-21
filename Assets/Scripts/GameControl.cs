using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


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
public class LobsterBurger{
	//Order 분류
	public string NBClass;
	//재료명
	public string ingreName;
	//sprite 경로
	public Sprite ingreSprite;
}

[System.Serializable]
public class DoublePatty{
	public string DPClass;
	public string ingreName;
	public string ingreSprite;
}

[System.Serializable]
public class NormalBurger{
	public string NBClass;
	public string ingreName;
	public string ingreSprite;
}


[System.Serializable]
public class ingreTable{
    public ingredient[] Ingredient;
}

public class GameControl : MonoBehaviour
{   

    
    string ingreJson;   
    public ingreTable IngreTable;

    // Start is called before the first frame update
    void Awake()
    {
        getJson();
        Debug.Log(ingreJson);
        IngreTable = JsonUtility.FromJson<ingreTable>(ingreJson);
        Debug.Log(IngreTable.Ingredient[0].ingreName);
        foreach(var cell in IngreTable.Ingredient){
            cell.ingreSprite = Resources.Load<Sprite>("Sprites/" + cell.ingreName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getJson(){
        ingreJson = File.ReadAllText(Application.dataPath + "/Resources/Json/ingredient.json");
    }

    void getNBOrder(){
    	ingreJson = File.ReadAllText(Application.dataPath + "/Resources/Json/NormalBurger.json");
    	IngreTable = JsonUtility.FromJson<ingreTable>(ingreJson);
    	foreach(var cell in IngreTable.Ingredient){
    		cell.ingreSprite=Resources.Load<Sprite>("Sprite/" + cell.ingreName);
    	}
    }

    void getDPOrder(){
    	IngreTable = JsonUtility.FromJson<ingreTable>(ingreJson);
    	foreach(var cell in IngreTable.Ingredient){
    		cell.ingreSprite = Resources.Load<Sprite>("Srite/" + cell.ingreName);
    	}
    }
}
