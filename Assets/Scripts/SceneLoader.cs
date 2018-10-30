using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(BoxCollider))]
public class SceneLoader : MonoBehaviour
{
    //for player to enter areas without special effects / fx
    public int level_id;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("World1") && Input.GetKeyDown(KeyCode.E))
        {
            level_id = other.gameObject.GetComponent<SceneID>().LevelID;
            SceneManager.LoadSceneAsync(level_id);
        }
    }
}
