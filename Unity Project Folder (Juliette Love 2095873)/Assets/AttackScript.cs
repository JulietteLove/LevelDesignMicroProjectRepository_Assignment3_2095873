using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackScript : MonoBehaviour
{
    public GameObject MeleeAttackButton;
    public GameObject FireballAttackButton;

    public Image enemyHealth;
    public Text ConsoleText;

    public int FireballBurn;

    public bool PlayerCanAttack = true;

    public GameObject WinUI;
    public GameObject LoseUI;

    public GameObject PlayerHealthGlow;
    public GameObject EnemyHealthGlow;
    public GameObject PlayerHealGlow;
    public GameObject healGlow;
    public Text healGlowText;

    public Text EnemyDamage;
    public Text PlayerDamage;
    public Text BurnText;
    public Text playerDefenceText;

    public float fillAmountHealth;
    public float playerfillAmountHealth;

    //CODE DEALING WITH PLAYER COMBAT STAGE

    public void HealButton()
    {
        EnemyChange enemyChange = GameObject.FindWithTag("EnemyChange").GetComponent<EnemyChange>();

        if (enemyChange.IsLevel1 == true || enemyChange.IsLevel3 == true)//This code will only run during lvl 1 and 3
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            DiceScript diceScript = GameObject.FindWithTag("Dice").GetComponent<DiceScript>();

            player.currentHealth = 10f;
            playerfillAmountHealth = player.currentHealth / player.maxHealth;
            diceScript.playerHealth.fillAmount = player.currentHealth / 1;

            PlayerHealGlow.SetActive(true);
            Invoke("PlayerGlowDisappear", 0.5f);

            Invoke("EnemyTurnChange", 2f);
            PlayerCanAttack = false;

            if (0 < player.defenceNumber)
            {
                player.defenceNumber -= 1;
                playerDefenceText.text = player.defenceNumber.ToString();            
                
                healGlowText.text = player.defenceNumber.ToString();
                healGlow.SetActive(true);
                Invoke("PlayerGlowDisappear", 0.5f);
            }


        }

        CombatSystem combatSystem = GameObject.FindWithTag("CombatSystem").GetComponent<CombatSystem>();
        combatSystem.CanRoll = false;
    }

    public void FireballButton()
    {
        EnemyChange enemyChange = GameObject.FindWithTag("EnemyChange").GetComponent<EnemyChange>();

        if (enemyChange.IsLevel2 == true || enemyChange.IsLevel3 == true) //This code only runs during lvl 2 and 3
        {
            Enemy enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
           
            FireballBurn = Random.Range(1, 6);

            //EnemyHealthGlow.SetActive(true);
            //Invoke("EnemyGlowDisappear", 0.5f);

            //EnemyDamage.text = "4";
            //Invoke("DamageDisappear", 2f);

            //ScreenshakeController screenShakeController = GameObject.FindWithTag("MainCamera").GetComponent<ScreenshakeController>();
            //StartCoroutine(screenShakeController.CameraShake(0.15f, 0.1f));

            if (FireballBurn >= 3 && PlayerCanAttack == true) //Player is burned
            {
                EnemyHealthGlow.SetActive(true);
                Invoke("EnemyGlowDisappear", 0.5f);

                EnemyDamage.text = "4";
                Invoke("DamageDisappear", 2f);

                ScreenshakeController screenShakeController = GameObject.FindWithTag("MainCamera").GetComponent<ScreenshakeController>();
                StartCoroutine(screenShakeController.CameraShake(0.15f, 0.1f));

                Debug.Log("BURN TEXT HAS RUN");

                Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
                DiceScript diceScript = GameObject.FindWithTag("Dice").GetComponent<DiceScript>();
                
                enemy.currentHealth -= 4;
                fillAmountHealth = enemy.currentHealth / enemy.maxHealth;
                enemyHealth.fillAmount = fillAmountHealth / 1;

                player.currentHealth -= 3f;
                playerfillAmountHealth = player.currentHealth / player.maxHealth;
                diceScript.playerHealth.fillAmount = playerfillAmountHealth / 1;

                BurnText.text = "You were burned";
                Invoke("DamageDisappear", 2f);

                PlayerDamage.text = "3";
                Invoke("DamageDisappear", 2f);

                PlayerHealthGlow.SetActive(true);
                Invoke("PlayerGlowDisappear", 0.5f);

                if (enemy.currentHealth < 1f) //Player Wins
                {
                    Debug.Log("Has Won");
                    WinUI.SetActive(true);
                }

                if (player.currentHealth < 1f) //Player Loses
                {
                    Debug.Log("Has Lost");
                    LoseUI.SetActive(true);
                }

                PlayerCanAttack = false;
            }

            if (FireballBurn < 3 && PlayerCanAttack == true) //Player does not get burned
            {
                EnemyHealthGlow.SetActive(true);
                Invoke("EnemyGlowDisappear", 0.5f);

                EnemyDamage.text = "4";
                Invoke("DamageDisappear", 2f);

                ScreenshakeController screenShakeController = GameObject.FindWithTag("MainCamera").GetComponent<ScreenshakeController>();
                StartCoroutine(screenShakeController.CameraShake(0.15f, 0.1f));

                enemy.currentHealth -= 4;
                fillAmountHealth = enemy.currentHealth / enemy.maxHealth;
                enemyHealth.fillAmount = fillAmountHealth / 1;


                if (enemy.currentHealth < 1f) //Player Wins
                {
                    Debug.Log("Has Won");
                    WinUI.SetActive(true);
                }

                PlayerCanAttack = false;
            }

            Invoke("EnemyTurnChange", 3f);
        }

        PlayerCanAttack = false;
        CombatSystem combatSystem = GameObject.FindWithTag("CombatSystem").GetComponent<CombatSystem>();
        combatSystem.CanRoll = false;
    }

    public void EnemyTurnChange()
    {
        CombatSystem combatSystem = GameObject.FindWithTag("CombatSystem").GetComponent<CombatSystem>();

        combatSystem.state = CombatState.ENEMYTURN;
        //ConsoleText.text = "Enemy Turn";
        combatSystem.EnemyCanRoll = true;
    }

    public void MeleeButton()
    {
        Enemy enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        EnemyChange enemyChange = GameObject.FindWithTag("EnemyChange").GetComponent<EnemyChange>();

        enemy.currentHealth -= player.attackPower;
        fillAmountHealth = enemy.currentHealth / enemy.maxHealth;
        enemyHealth.fillAmount = fillAmountHealth / 1;


        EnemyHealthGlow.SetActive(true);
        Invoke("EnemyGlowDisappear", 0.5f);

        EnemyDamage.text = player.attackPower.ToString();
        Invoke("DamageDisappear", 2f);

        ScreenshakeController screenShakeController = GameObject.FindWithTag("MainCamera").GetComponent<ScreenshakeController>();
        StartCoroutine(screenShakeController.CameraShake(0.1f, 0.05f));

        if (enemy.currentHealth < 1f /*|| enemy.currentHealth > 1f*/) //Player Wins
        {
            Debug.Log("Has Won");
            WinUI.SetActive(true);
        }

        CombatSystem combatSystem = GameObject.FindWithTag("CombatSystem").GetComponent<CombatSystem>();

        Invoke("EnemyTurnChange", 0.5f);
        //ConsoleText.text = "Enemy Turn";
        combatSystem.EnemyCanRoll = true;
        combatSystem.CanRoll = false;
    }

    public void PlayerGlowDisappear()
    {
        PlayerHealthGlow.SetActive(false);
        PlayerHealGlow.SetActive(false);
        healGlow.SetActive(false);
    }

    public void EnemyGlowDisappear()
    {
        EnemyHealthGlow.SetActive(false);
    }

    public void DamageDisappear()
    {
        EnemyDamage.text = "";
        PlayerDamage.text = "";
        BurnText.text = "";
    }
}
