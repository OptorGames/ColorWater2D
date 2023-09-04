using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorController : MonoBehaviour
{
    private const string Happy = "Happy";
    private const string Sad = "Sad";
    private const int BeachTheme = 5;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject beads;
    [SerializeField] private Animator animator;

    private void Start()
    {
        gameManager.OnSelectedBackgroundSet += UpdateProfessorLook;
        gameManager.OnTubeInteraction += UpdateAnimation;

        UpdateProfessorLook();
    }

    private void UpdateProfessorLook()
    {
        int theme = PlayerPrefs.GetInt("Theme");
        beads.SetActive(theme == BeachTheme);
    }

    private void UpdateAnimation(bool success)
    {
        animator.SetTrigger(success ? Happy : Sad);
    }

    private void OnDestroy()
    {
        gameManager.OnSelectedBackgroundSet -= UpdateProfessorLook;
        gameManager.OnTubeInteraction -= UpdateAnimation;
    }
}
