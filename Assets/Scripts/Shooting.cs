using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
[RequireComponent(typeof(playercontroller))]
public class Shooting : MonoBehaviour
{
    private SpriteRenderer _roboSprite;

    private Animator _animator;
    public Collider _collider;
    public Collider _ColliderTrigger;
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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum movement
{
    Right,
    Left,
    Up,
    Down
};