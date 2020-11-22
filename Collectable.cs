using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    HealthPotion,
    ManaPotion,
    Money
}

public class Collectable : MonoBehaviour
{
    public CollectableType Type = CollectableType.Money;

    private SpriteRenderer Sprite;
    private CircleCollider2D ItemColider;

    private bool HasBeenColected = false;
    public int Value = 1;

    private GameObject Player;

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        ItemColider = GetComponent<CircleCollider2D>();
    }

    void Show()
    {
        Sprite.enabled = true;
        ItemColider.enabled = true;
        HasBeenColected = false;
    }

    void Hide()
    {
        Sprite.enabled = false;
        ItemColider.enabled = false;
    }

    void Collect()
    {
        Hide();
        HasBeenColected = true;

        switch (this.Type)
        {
            case CollectableType.Money:
                GameManager.sharedInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;
            case CollectableType.HealthPotion:
                Player.GetComponent<PlayerController>().CollectHealth(this.Value);
                break;
            case CollectableType.ManaPotion:
                Player.GetComponent<PlayerController>().CollectManna(this.Value);
            break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player");
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            Collect();
        }
    }
}
