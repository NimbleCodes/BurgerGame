﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)){
        	int score = Random.Range(0,2000);
        	string username = "";
        	string alphabet = "abcdefghijklmnopqrstuvwxyz";

        	for (int i = 0; i < Random.Range(5,10); i++){
        		username += alphabet[Random.Range(0,alphabet.Length)];
        	}

        	Data_manager.AddNewHighscore(username,score);
        }
    }
}
