﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinText : MonoBehaviour {

    [SerializeField]private float       _endtimer;
    [SerializeField]private GameObject  _player1;
    [SerializeField]private GameObject  _player2;
                    private Text        _winText;
    
    

	// Use this for initialization
	void Start () {
        UIDelegates.TextEvent += CheckForWinner;
        _winText = GetComponent<Text>();
	}
	
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(_endtimer);
        SceneManager.LoadScene("EndGame");
    }

    void CheckForWinner()
    {
        if (!_player1)
        {
            _winText.text = "Player 2 Wins!";
            StartCoroutine(EndGame());
        }
        else if (!_player2)
        {
            _winText.text = "Player 1 Wins!";
            StartCoroutine(EndGame());
        }
    }

    void OnDisable()
    {
        UIDelegates.TextEvent -= CheckForWinner;
    }
}
