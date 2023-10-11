using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Collectible : MonoBehaviour,ISpawnable
{
    [SerializeField] protected int contentAmount;
    [SerializeField] protected GameSettings gameSettings;
    [SerializeField] private float moveDuration=0.5f;
    [SerializeField] private float moveAmount=1;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private Canvas canvas;
    private Player _player;

    public SpawnLocation SpawnLocation { get; set; }
    public float SpawnCooldown { get; set; }


    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        
        SetCooldown();
        transform.DOMoveY(transform.position.y + moveAmount, moveDuration).SetLoops(-1,LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Collect(player);
        }
    }

    private void Update()
    {
        canvas.transform.LookAt(_player.transform);
    }

    protected abstract void Collect(Player player);

    public void SetAmount(int amount)
    {
        contentAmount = amount;
        contentText.text = contentAmount.ToString();
    }
    
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        if (SpawnLocation!=null)
        {
            SpawnLocation.MakeSpawnPointFull(false);
        }
    }

    public virtual void Spawn(SpawnLocation spawnLocation)
    {
        transform.position = spawnLocation.transform.position;
        SpawnLocation = spawnLocation;
        gameObject.SetActive(true);
        contentText.text = contentAmount.ToString();
    }
    public abstract void SetCooldown();
    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public abstract int MaxAmount();

}
