using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTool : MonoBehaviour
{
    private string Player = "Player";
    private bool _Grounded;
    public GameObject ChildGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Player))
        {
            _Grounded = other.GetComponent<playercontroller>().Grounded;
            if (!_Grounded)
            {
                ChildGameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Player))
        {
            ChildGameObject.GetComponent<BoxCollider>().enabled = true;
        }

        if (other.CompareTag(Player) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(waitforEnumerator());
        }
    }

    private IEnumerator waitforEnumerator()
    {
        yield return new WaitForSeconds(0.5f);
    }
}