﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    public string interactText;
    private static bool hasTools;
    private bool isIn;

    void Start()
    {
        interactText = "";
    }

    private void Update()
    {

        //Interact with objects in scene
        if (Input.GetKeyDown(KeyCode.E) && gameObject.name == "ToolBox" && isIn)
        {
            interactText = "You found some tools!";
            hasTools = true;           
            GameManager.Instance.interactText.text = interactText;
            StartCoroutine(DestroyInteractScript());
        }

        if (Input.GetKeyDown(KeyCode.E) && gameObject.name == "Teleport_Terminal" && isIn)
        {
            if (!hasTools)
            {
                interactText = "Terminal is broken! If only you had some tools to fix it...";
                GameManager.Instance.interactText.text = interactText;
            }

            if (hasTools)
            {
                interactText = "Terminal is now repaired!";
                GameManager.Instance.interactText.text = interactText;
                CustomTeleporter.teleportPadOn = true;
                GameManager.Instance.interactBttn.SetActive(false);
                Destroy(this);
            }
        }

        // Show the interact button on screen
        if (isIn)
        {
            if (GameManager.Instance.dialogBox.activeSelf || GameManager.Instance.sign.activeSelf)
                GameManager.Instance.interactBttn.SetActive(false);
            else
                GameManager.Instance.interactBttn.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isIn = false;
            GameManager.Instance.interactBttn.SetActive(false);
        }
    }

    IEnumerator DestroyInteractScript()
    {
        GameManager.Instance.interactBttn.SetActive(false);
        isIn = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(this);
    }
}
