using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float playerWalkSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private GameSettings gameSettings;
    private Vector3 _moveVector;
    private bool _onJump;
    private int _movementLevel;
    private int _jumpLevel;

    //TODO: Revise this script.

    private void Awake()
    {
        playerWalkSpeed = gameSettings.speedTalentLevels[_movementLevel].newWalkSpeed;
        playerRunSpeed = gameSettings.speedTalentLevels[_movementLevel].newRunSpeed;
        jumpHeight = gameSettings.jumpHeightLevels[_jumpLevel];
    }

    private void OnEnable()
    {
        EventManager.PlayerDied += Stop;
        EventManager.Upgrade += Upgrade;
        EventManager.StopPlayerControl += Stop;
        EventManager.StartPlayerControl += StartControl;
    }
    private void OnDisable()
    {
        EventManager.PlayerDied -= Stop;
        EventManager.Upgrade -= Upgrade;
        EventManager.StopPlayerControl -= Stop;
        EventManager.StartPlayerControl -= StartControl;
    }

    private void StartControl()
    {
        this.enabled = true;
    }

    private void Upgrade(Upgrades upgrades)
    {
        if (upgrades==Upgrades.MovementUpgrade)
        {
            _movementLevel++;
            playerWalkSpeed = gameSettings.speedTalentLevels[_movementLevel].newWalkSpeed;
            playerRunSpeed = gameSettings.speedTalentLevels[_movementLevel].newRunSpeed;
            if (gameSettings.speedTalentLevels.Length-1==_movementLevel)
            {
                EventManager.OnMaxUpgradeReached(Upgrades.MovementUpgrade);
            }
        }
        else if (upgrades==Upgrades.JumpUpgrade)
        {
            _jumpLevel++;
            jumpHeight = gameSettings.jumpHeightLevels[_jumpLevel];
            if (gameSettings.jumpHeightLevels.Length-1==_jumpLevel)
            {
                EventManager.OnMaxUpgradeReached(Upgrades.JumpUpgrade);
            }
        }
    }

    private void Stop()
    {
        this.enabled=false;
    }
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
