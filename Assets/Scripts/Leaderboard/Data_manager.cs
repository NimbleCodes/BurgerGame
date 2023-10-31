using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_manager : MonoBehaviour
{
	const string privateCode ="VWF3sq6jnE-DteriktSahQTXQovP7hMESvC_BqILg1Hw";
	const string publicCode="5e785180fe232612b8d1b1e0";
	const string webURL = "http://dreamlo.com/lb/";

	public HighScore[] highscoresList;
	static Data_manager instance;
	DisplayHighscores highscoresDisplay;
	
	void Awake()
	{
		instance = this;
		highscoresDisplay = GetComponent<DisplayHighscores>();
	}


	public static void AddNewHighscore(string username, int score){
		instance.StartCoroutine(instance.UploadNewHighscore(username,score));
	}
	IEnumerator UploadNewHighscore(string username, int score){
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username)
			+ "/" + score);
		yield return www;

		if(string.IsNullOrEmpty(www.error))
		{
			print("upload Successful");
			DownloadHighscores();
		}else{
			print("Error uploading: " + www.error);
		}
	}
	public void DownloadHighscores(){
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if(string.IsNullOrEmpty(www.error))
		{
			FormatHighScores(www.text);
			highscoresDisplay.OnHighscoresDownloaded(highscoresList);
		}else{
			print("Error uploading: " + www.error);
		}
	}

	void FormatHighScores(string textStream){
		string[] entries = textStream.Split(new char[] {'\n'},System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new HighScore[entries.Length];
		for(int i = 0; i < entries.Length; i++){
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new HighScore(username,score);
		}
	}
}
public struct HighScore{
	public string username;
	public int score;
	public HighScore(string _username, int _score)
	{
		username = _username;
		score = _score;
	}
}