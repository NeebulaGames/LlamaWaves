﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LamaWaves.Scripts;

public class GameplayManager : MonoBehaviour
{
    public const uint audioDelay = 1;
    public bool smashMode = false;
    public int numberOfPlayers;

    public GameObject inGameUI;

    public GameObject countdown3;
    public GameObject countdown2;
    public GameObject countdown1;
    public GameObject countdownGO;

    private LamaGameManager gameManager;
    private AudioManager gameAudio;
    private OnScreenButtonManager buttonsManager;
    private PlayerManager playerManager;

    void Awake()
    {
        gameManager = GetComponent<LamaGameManager>();
        gameAudio = GetComponent<AudioManager>();
        buttonsManager = GetComponent<OnScreenButtonManager>();
        playerManager = GetComponent<PlayerManager>();

    }

    void Start()
    {

    }
    
    void Update()
    {
        
    }

    public void StartGame(Dictionary<int, int> players)
    {
        gameAudio.PlaySound("HighFive", audioDelay);
        smashMode = false;
        gameAudio.enabled = true;
        //TOOD: playercotroller preare players on screen
        StartCoroutine(CountdownStartGame());
        //CRIDA COROUTINE per llançar GameReady
        playerManager.enabled = true;
        playerManager.InitPlayers(players);

        inGameUI.SetActive(true);
    }

    public void GameReady()
    {
        buttonsManager.enabled = true;
        buttonsManager.Init();
    }

    public void EndGame()
    {
        Debug.Log("Game ended");
        buttonsManager.enabled = false;
        gameAudio.enabled = false;
        playerManager.enabled = false;
        
        gameManager.EndGame();
    }

    void OnDisable()
    {
        buttonsManager.enabled = false;
        gameAudio.enabled = false;
    }

    public IEnumerator CountdownStartGame()
    {
        GameObject instance;
        GameObject canvas = GameObject.Find("Canvas");
        Vector3 position = new Vector3(0f, 250f, 0f);
        Vector3 scale = new Vector3(1f, 1f, 0f);
       
        instance = Instantiate(countdown3, canvas.transform);
        instance.transform.localPosition = position;
        instance.transform.localScale = scale;
        instance.GetComponent<Image>().CrossFadeAlpha(0, 1.4f, false);
        instance.transform.DOScale(2, 1.4f).OnComplete(() => Destroy(instance));
        yield return new WaitForSeconds(1.4f);

        instance = Instantiate(countdown2, canvas.transform);
        instance.transform.localPosition = position;
        instance.transform.localScale = scale;
        instance.GetComponent<Image>().CrossFadeAlpha(0, 1.4f, false);
        instance.transform.DOScale(2, 1.4f).OnComplete(() => Destroy(instance));
        yield return new WaitForSeconds(1.4f);

        instance = Instantiate(countdown1, canvas.transform);
        instance.transform.localPosition = position;
        instance.transform.localScale = scale;
        instance.GetComponent<Image>().CrossFadeAlpha(0, 1.4f, false);
        instance.transform.DOScale(2, 1.4f).OnComplete(() => Destroy(instance));
        instance.transform.localPosition = position;
        yield return new WaitForSeconds(1.4f);

        instance = Instantiate(countdownGO, canvas.transform);
        instance.transform.localPosition = position;
        instance.transform.localScale = scale;
        instance.GetComponent<Image>().CrossFadeAlpha(0, 1.4f, false);
        instance.transform.DOScale(2, 1.4f).OnComplete(() => Destroy(instance));
        instance.transform.localPosition = position;
        yield return new WaitForSeconds(1.4f);

        GameReady();
    }
}
