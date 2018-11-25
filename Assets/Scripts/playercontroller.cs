using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio.Google;
using UnityEngine.Experimental.UIElements;

public class playercontroller : MonoBehaviour
{
    public bool LookingDown;

    public Animator CurrentAnimator;
    // public bool LookingUp;

    public bool MovingRight = true;

    //jumping for this obj

    #region jumpingVar

    [Range(1, 10)] public float JumpVelocity;
    public float JumpModifer = 0f;
    public int MaxJumps = 2;
    public int CurrentJump = 0;

    #endregion jumpingVar

    // deals with collision of this object currently
    // There is a trigger on this obj

    #region collision

    private Collider _currentCollider;

    private Rigidbody _currentRigidbody;

    //@Speed - how fast this obj moves Horizontal
    public float Speed = 0f;

    #endregion collision

    #region camera

    // distance to the player from camera
    public float DistanceY = 2f;

    public float DistanceZ = 12f;

    // distance for how far the camera will follow
    public float MinDistance = -17f;

    public float MaxDistance = 10;
    //Getting camera to follow
    //Todo - try spring arm next time

    #endregion camera

    #region Shooter

    private Shooting ObjPool;
    private bool flipx;

    #endregion Shooter

    public Camera CurrentCamera;
    public bool Grounded;

    public GameObject RightGunPosition;

    public GameObject LeftGunPosition;

    public GameObject CurrentObjFromPool;

    // getting components
    private void Start()
    {
        _currentCollider = GetComponent<Collider>();
        _currentRigidbody = GetComponent<Rigidbody>();
        ObjPool = GetComponent<Shooting>();
    }

    // Currently gets input to move horizontally and jump
    private void Update()
    {
        CurrentCamera.transform.position = Vector3.Lerp(CurrentCamera.transform.position, new Vector3(Mathf.Clamp(transform.position.x, MinDistance, MaxDistance), DistanceY, DistanceZ),
            Time.fixedDeltaTime * Speed);
        //CurrentCamera.transform.position = new Vector3(_currentRigidbody.position.x,2,12);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovingRight = false;
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                CurrentAnimator.Play("TopLeft");
            }
            else
                CurrentAnimator.Play("IdleFlip");
            //currentRigidbody.transform.Translate(Vector3.right, transform);
            Vector3 temp = this.transform.position;
            this.transform.position = Vector3.LerpUnclamped(this.transform.position, temp - Vector3.left, Time.deltaTime * Speed);
            flipx = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 temp2 = this.transform.position;
            //alt movement
            MovingRight = true;
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                CurrentAnimator.Play("TopRight");
            }
            else
                CurrentAnimator.Play("idle");
            //this.transform.position = Vector3.LerpUnclamped(this.transform.position, temp2 - Vector3.right, Time.deltaTime*Speed);
            transform.Translate(Vector3.right * Speed * Time.fixedDeltaTime, Space.Self);
            flipx = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (CurrentJump >= MaxJumps && Grounded)
            {
                CurrentJump = 0;
            }
            if (CurrentJump < MaxJumps)
            {
                GetComponent<Rigidbody>().velocity = Vector3.up * JumpVelocity;
                CurrentJump++;
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (flipx)
            {
                SetCorrectCollider(CurrentObjFromPool, Vector3.left, LeftGunPosition);
            }
            else
            {
                SetCorrectCollider(CurrentObjFromPool, Vector3.right, RightGunPosition);
            }
        }
    }

    private void SetCorrectCollider(GameObject tempGameObject, Vector3 dir, GameObject parentGameObject)
    {
        tempGameObject = ObjPool.getPool();
        parentGameObject.transform.localPosition = Vector3.zero;
        tempGameObject.SetActive(true);
        if (!tempGameObject.GetComponent<Rigidbody>())
            tempGameObject.AddComponent<Rigidbody>();
        tempGameObject.GetComponent<Rigidbody>().AddForceAtPosition(dir * 25, parentGameObject.transform.localPosition, ForceMode.Force);
    }

    //cheap way of getting grounded flag met
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Grounded = true;
            CurrentJump = 0;
        }

        if (other.gameObject.CompareTag("Crate"))
        {
            Grounded = true;
            CurrentJump = 1;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Grounded = false;
        }
        if (other.gameObject.CompareTag("Crate"))
        {
            Grounded = false;
            CurrentJump = 1;
        }
    }
}