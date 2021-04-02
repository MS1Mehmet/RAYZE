using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Code kopiert und modifiziert von Tony Bhimani, Link verfügbar unter: https://www.youtube.com/user/TonyBhimani

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    AssetPalette assetPalette;

    bool isGameOver;
    bool playerReady;
    bool initReadyScreen;

    int playerScore;
    int bonusCount;
    int bonusScore;             // Für später, wenn wir Bonusballs reinpacken

    float gameRestartTime;
    float gamePlayerReadyTime;

    public float gameRestartDelay = 2f;
    public float gamePlayerReadyDelay = 1f;


    public struct WorldViewCoordinates
    {
        public float Top;
        public float Right;
        public float Bottom;
        public float Left;
    }
    public WorldViewCoordinates worldViewCoords;


    TextMeshProUGUI playerScoreText;
    TextMeshProUGUI screenMessageText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerReady)
        {
            if(initReadyScreen)
            {
                FreezePlayer(true);
                FreezeEnemies(true);
                screenMessageText.alignment = TextAlignmentOptions.Center;
                screenMessageText.alignment = TextAlignmentOptions.Top;
                screenMessageText.fontStyle = FontStyles.UpperCase;
                screenMessageText.fontSize = 24;
                screenMessageText.text = "\n\n\nSTART";
                initReadyScreen = false;
            }

            gamePlayerReadyTime -= Time.deltaTime;
            if(gamePlayerReadyTime < 0)
            {
                FreezePlayer(false);
                FreezeEnemies(false);
                TeleportPlayer(true);
                screenMessageText.text = "";
                playerReady = false;
            }
            return;
        }

        if(playerScoreText != null)
        {
            playerScoreText.text = String.Format("<mspace=\"{0}\">{1:0000000}</mspace>", playerScoreText.fontSize, playerScore);

        }

        if(!isGameOver)
        {

        }
        else
        {
            gameRestartTime -= Time.deltaTime;
            if(gameRestartTime < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // DIe Szene wird neu geladen, wenn wir sterben
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;


    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartGame();
    }

    private void StartGame()
    {
        isGameOver = false;
        playerReady = true;
        initReadyScreen = true;
        gamePlayerReadyTime = gamePlayerReadyDelay;
        playerScoreText = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
        screenMessageText = GameObject.Find("ScreenMessage").GetComponent<TextMeshProUGUI>();
    }

    public void AddScorePoints(int points)
    {
        playerScore += points;
    }

    public void AddBonusPoints(int points)
    {
        bonusCount++;
        bonusScore += points;
    }
    private void FreezePlayer (bool freeze)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().FreezeInput(freeze);
            player.GetComponent<PlayerController>().FreezePlayer(freeze);

        }

    }

    private void FreezeEnemies(bool freeze)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
           // enemy.GetComponent<EnemyController>().FreezeEnemy(freeze);
        }
    }

    private void FreezeBullets(bool freeze)
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<BulletScript>().FreezeBullet(freeze);
        }
    }

    private void TeleportPlayer (bool teleport)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().Teleport(teleport);
        }
    }

    public void PlayerDefeated()
    {
        isGameOver = true;
        gameRestartTime = gameRestartDelay;
        FreezePlayer(true);
        FreezeEnemies(true);
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
       // GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
        //foreach (GameObject explosion in explosions)
        //{
        //    Destroy(explosion);
        //}

    }

    private void DestroyStrayBullets()
    {
        // destroy all bullets that are outside the camera world view
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            // bullet is out of view so destroy it
            if (bullet.transform.position.x < worldViewCoords.Left ||
                bullet.transform.position.x > worldViewCoords.Right ||
                bullet.transform.position.y > worldViewCoords.Top ||
                bullet.transform.position.y < worldViewCoords.Bottom)
            {
                // buh bye bullet
                Destroy(bullet);
            }
        }
    }
    private ItemScript.ItemTypes PickRandomBonusItem()
    {
        float[] probabilities =
        {
        12,53,15,15,2,2,1,12,16
        };
        float total = 0;

        ItemScript.ItemTypes[] items = {
            ItemScript.ItemTypes.Nothing,
            ItemScript.ItemTypes.WeaponEnergySmall,
            ItemScript.ItemTypes.LifeEnergySmall,
            ItemScript.ItemTypes.WeaponEnergyBig,
            ItemScript.ItemTypes.LifeEnergyBig,
            ItemScript.ItemTypes.ExtraLife,
            ItemScript.ItemTypes.Nothing,
        };

        foreach (float prob in probabilities)
        {
            total += prob;

        }

        float randomPoint = UnityEngine.Random.value * total;

        for(int i = 0; i<probabilities.Length;i++)
        {
            if(randomPoint < probabilities[i])
            {
                return items[i];

            }
            else
            {
                randomPoint -= probabilities[i];
            }
        }
        return items[probabilities.Length - 1];
    }

    public GameObject GetBonusItem(ItemScript.ItemTypes itemType)
    {
        GameObject bonusItem = null;

        if(itemType == ItemScript.ItemTypes.Random)
        {
            itemType = PickRandomBonusItem();
        }

        switch (itemType)
        {
            case ItemScript.ItemTypes.Nothing:
                bonusItem = null;
                break;
            //case ItemScript.ItemTypes.BonusBall:
            //bonusItem = assetPalette.ItemPrefabs[(int)AssetPalette.ItemList.BonusBall];
            case ItemScript.ItemTypes.ExtraLife:
                bonusItem = assetPalette.itemPrefabs[(int)AssetPalette.ItemList.ExtraLife];
                break;
            case ItemScript.ItemTypes.LifeEnergyBig:
                bonusItem = assetPalette.itemPrefabs[(int)AssetPalette.ItemList.LifeEnergyBig];
                break;
            case ItemScript.ItemTypes.LifeEnergySmall:
                bonusItem = assetPalette.itemPrefabs[(int)AssetPalette.ItemList.LifeEnergySmall];
                break;
         
            case ItemScript.ItemTypes.WeaponEnergyBig:
                bonusItem = assetPalette.itemPrefabs[(int)AssetPalette.ItemList.WeaponEnergyBig];
                break;
            case ItemScript.ItemTypes.WeaponEnergySmall:
                bonusItem = assetPalette.itemPrefabs[(int)AssetPalette.ItemList.WeaponEnergySmall];
                break;
            case ItemScript.ItemTypes.MagnetBeam:
                bonusItem = assetPalette.itemPrefabs[(int)AssetPalette.ItemList.MagnetBeam];
                break;
            case ItemScript.ItemTypes.Yashichi:
                bonusItem = assetPalette.itemPrefabs[(int)AssetPalette.ItemList.Yashichi];
                break;


        }

        return bonusItem;
    }
}
