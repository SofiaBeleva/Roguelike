using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    private bool mushCollected = false;
    private bool meatCollected = false;

    public List<string> collectedNames = new List<string>();
    public static float Health { get => health; set => health = value;}
    public static int MaxHealth { get => maxHealth; set => maxHealth = value;}
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }                                   
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public TMP_Text HealthText;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        if(Health <= 0)
        {
            Health = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;
        if(Health <= 0)
        {
            KillPlayer();
        }
    }
    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }
    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }
    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }    

    public void UpdateCollectedItems(CollectionController item)
    {
        collectedNames.Add(item.item.name);

        foreach(string i in collectedNames)
        {
            switch(i)
            {
                case "Mushroom":
                    mushCollected = true; break;
                case "Meat":
                    meatCollected = true; break;
            }
        }

        if(mushCollected && meatCollected)
        {
            FireRateChange(0.25f);
        }
    }

    private static void KillPlayer()
    {
        SceneManager.LoadScene("Menu");
    }
}
