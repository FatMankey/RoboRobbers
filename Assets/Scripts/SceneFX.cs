using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneID))]
public class SceneFX : MonoBehaviour
{
    public Light SpotLight;
    public Canvas Message;
    private int _id;
    private string player = "Player";
	// Use this for initialization
	void Start () {
	    SpotLight.gameObject.SetActive(false);
        Message.gameObject.SetActive(false);
	    _id = gameObject.GetComponent<SceneID>().LevelID;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(player))
        {
            FX_on();
            if (other.gameObject.CompareTag(player) && Input.GetKeyDown(KeyCode.UpArrow))
            {
                SceneManager.LoadSceneAsync(_id);
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(player))
        {
            FX_off();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(player) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneManager.LoadSceneAsync(_id);
        }
    }
    private void FX_on()
    {
        SpotLight.gameObject.SetActive(true);
        Message.gameObject.SetActive(true);
    }

    private void FX_off()
    {
        SpotLight.gameObject.SetActive(false);
        Message.gameObject.SetActive(false);
    }
}
