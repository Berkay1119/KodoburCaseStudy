using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float playerWalkSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float jumpHeight;
    private Vector3 _moveVector;
    private bool _onJump;
    private void Update()
    {
        _moveVector= new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move(_moveVector, Input.GetKey(KeyCode.LeftShift) ? playerRunSpeed : playerWalkSpeed);
    }

    private void Jump()
    {
        if (_onJump)
        {
            return;
        }
        _onJump = true;
        float currentHeight = transform.position.y;
        transform.DOMoveY(currentHeight+jumpHeight,playerRunSpeed).SetSpeedBased().SetLoops(2,LoopType.Yoyo).
            OnComplete(()=>_onJump=false);

        
    }

    private void Move(Vector3 moveVector,float speed)
    {
        if (_onJump)
        {
            return;
        }
        Quaternion rotation = transform.rotation;
        if (moveVector.z!=0)
        {
            Vector3 direction = rotation * new Vector3(0,0,moveVector.z);

            Vector3 movement = direction * (speed * Time.deltaTime);
            
            transform.Translate(movement);
        }
        
        if (moveVector.x!=0)
        {
            Vector3 direction = rotation * new Vector3(moveVector.x,0,0);

            Vector3 movement = direction * (speed * Time.deltaTime);
            
            transform.Translate(movement);
        }
    }
}
