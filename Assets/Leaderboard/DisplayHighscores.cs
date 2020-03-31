using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayHighscores : MonoBehaviour
{
	public Text[] highscoreText;
	public Text[] highscoreScore;
	Data_manager highscoreManager;
    
    void Start()
    {
        for(int i=0;i < highscoreText.Length; i++){
        	highscoreText[i].text = ".Fetching..";
			highscoreScore[i].text = "0";
		}

		highscoreManager = GetComponent<Data_manager>();
    	StartCoroutine("RefreshHighScores");
    }

    public void OnHighscoresDownloaded(HighScore[] highscoreList){
    	for(int i=0;i < highscoreText.Length; i++){
        	highscoreText[i].text = i+1 + ". ";
        	if (highscoreList.Length > i){
        		highscoreText[i].text = highscoreList[i].username;
        		highscoreScore[i].text ="" + highscoreList[i].score;
        	}
		}
    }

    IEnumerator RefreshHighScores(){
    	while(true){
    		highscoreManager.DownloadHighscores();
    		yield return new WaitForSeconds(30);
    	}
    }
}
