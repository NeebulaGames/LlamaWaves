﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenButtonManager : MonoBehaviour
{
    public enum ColliderType
    {
        Button_A,
        Button_B
    }

    private GameplayManager gameplay;
    private AudioManager audioManager;

    private bool started;
    private const float speedFactor = 6f / 1080f;
    private float speed;
    private float radixTime;
    private bool countdown = false;
    private bool smash = false;
    private Coroutine buttonsCoroutine;
    private Transform buttonsContainer;
    private RectTransform canvasRect;

    private MovingButton baseButton;

    void Awake()
    {
        started = false;
        gameplay = GetComponent<GameplayManager>();
        audioManager = GetComponent<AudioManager>();
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        speed = canvasRect.sizeDelta.x * speedFactor;
        radixTime = speed / 50f;
        buttonsContainer = GameObject.Find("ButtonsContainer").transform;

        baseButton = Resources.Load<MovingButton>("MovingButton");
    }

    public void Init()
    {
        smash = false;
        countdown = false;
        started = true;
        buttonsCoroutine = StartCoroutine(GenerateButtons());
    }

    private IEnumerator GenerateButtons()
    {
        while (!smash && !countdown)
        {
            MovingButton btn = Instantiate(baseButton, buttonsContainer);
            btn.transform.localScale = Vector3.one;
            Vector3 localPosition = Vector3.one * 100;
            localPosition.y = -100;
            btn.transform.localPosition = localPosition;
            // TODO: Implement me
            btn.Init(ColliderType.Button_A, speedFactor, canvasRect.sizeDelta.x);
            yield return new WaitForSeconds(radixTime * 2f + 0.1f);
        }
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(10f);
        countdown = false;
    }

    public void Update()
    {
        if (started && !smash && audioManager.TimeUntilNextStage() <= 10f)
        {
            smash = true;
            StopCoroutine(buttonsCoroutine);
            countdown = true;
            StartCoroutine(Countdown());
        }

        if (started && smash && !countdown && gameplay.smashMode != smash)
        {
            smash = false;
            buttonsCoroutine = StartCoroutine(GenerateButtons());
        }
    }
}