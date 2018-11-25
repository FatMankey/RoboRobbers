using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[RequireComponent(typeof(playercontroller))]
public class Shooting : MonoBehaviour
{
    private SpriteRenderer _roboSprite;
    public List<GameObject> BulletPool;
    public GameObject SpriteShot;

    public int BulletSize = 10;
    private Animator _animator;
    //public Collider _collider;
    //public Collider _ColliderTrigger;

    private void Init()
    {
        _animator = GetComponent<Animator>();
        _roboSprite = GetComponent<SpriteRenderer>();
    }

    public void Movement(movement dir)
    {
        switch (dir)
        {
            case movement.Down:
                break;

            case movement.Up:
                break;

            case movement.Left:
                break;

            case movement.Right:
                break;
        }
    }

    // Use this for initialization
    private void Start()
    {
        BulletPool = new List<GameObject>();
        for (var i = 0; i < BulletSize; i++)
        {
            GameObject temp = Instantiate(SpriteShot);
            temp.SetActive(false);
            BulletPool.Add(temp);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public GameObject getPool()
    {
        for (var i = 0; i < BulletPool.Count; i++)
        {
            if (BulletPool[i].activeSelf)
            {
                var temp = Instantiate(SpriteShot);
                temp.SetActive(false);
                BulletPool[i] = temp;
                return BulletPool[i];
            }

            if (!BulletPool[i].activeInHierarchy)
            {
                return BulletPool[i];
            }
        }

        return null;
    }

    public void ResetPool()
    {
        foreach (var bulletGameObject in BulletPool)
        {
            bulletGameObject.transform.localPosition = Vector3.zero;
            bulletGameObject.SetActive(false);
        }
    }

    public GameObject UsePool(Vector3 dir)
    {
        var temp = getPool();
        temp.SetActive(true);
        temp.GetComponent<Rigidbody>().AddForce(dir, ForceMode.Force);
        return temp;
    }
}

public enum movement
{
    Right,
    Left,
    Up,
    Down
};