using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffers : MonoBehaviour
{
    public int maxJumps = 2;

    public int currentJumps;

    public bool ground;

    public int buffersize = 12;

    public InputBufferItem[] InputBuffer;

    public Vector3 velocity;

    public float jumpPow = 0.4f;

    public float gravity = -0.025f;
    
	// Use this for initialization
	void Start () {
		InputBuffer = new InputBufferItem[buffersize];
	    for (var i = 0; i < InputBuffer.Length; i++)
	    {
            InputBuffer[i] = new InputBufferItem();
	    }


	}
	
	// Update is called once per frame
	void Update () {
		UpdateBuffer();
        UpdateCommand();
        UpdatePhysics();
	}

    public void UpdateBuffer()
    {

        if (Input.GetAxisRaw("Jump") > 0)
        {
            InputBuffer[InputBuffer.Length-1].Hold();
        }
        else
        {
            InputBuffer[InputBuffer.Length - 1].ReleasedHold();
        }

        for (var i = 0; i < InputBuffer.Length - 1; i++)
        {
            InputBuffer[i].hold = InputBuffer[i + 1].hold;
            InputBuffer[i].used = InputBuffer[i + 1].used;
        }
    }

    public void UpdateCommand()
    {
        foreach (var t in InputBuffer)
        {
            if (t.CanExecute()) continue;
            if (!Jump()) continue;
           // t.Execute();
            break;
        }
    }

    public bool Jump()
    {
        if (currentJumps <= 0) return false;
        velocity.y = jumpPow;
        currentJumps--;
        return true;

    }

    public void UpdatePhysics()
    {
        velocity.y += gravity;
        transform.position += velocity;
        if (transform.position.y < 0)
        {
            ground = true;
            currentJumps = maxJumps;
            transform.position = new Vector3(transform.position.x, 0 , transform.position.z);
        }
        else
        {
            ground = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            ground = true;
        }
    }
}

[System.Serializable]
public class InputBufferItem
{
    public int hold;
    public bool used;

    public bool CanExecute()
    {
        return hold == 1 && !used;
    }

    public void Execute()
    {
        used = true;
    }

    public void Hold()
    {
        hold = hold < 0 ? 1 : 0;
    }

    public void ReleasedHold()
    {
        if (hold > 0)
        {
            hold = -1;
            used = false;
        }
        else
        {
            hold = 0;
        }
    }
    public void Forcehold()
    {
        hold = 2;
    }

    public void Reset()
    {
        hold = 0;
        used = false;
    }
}
