using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiceScript : MonoBehaviour
{
    public float NumberRolled;
    public float EnemyNumberRolled;
    public bool ButtonPressed = false;
    public GameObject DiceText;
    public GameObject EnemyDiceText;
    public Text ConsoleText;

    public Image playerHealth;

    public GameObject playerMissExplanation;
    public bool FirstTimePlayerMiss = true;

    //public bool PlayerMiss = false;

    public void ButtonClicked() //CODE DEALING WITH PLAYER ROLL
    {
        ButtonPressed = true;

        CombatSystem combatSystem = GameObject.FindWithTag("CombatSystem").GetComponent<CombatSystem>();

        if (ButtonPressed == true && combatSystem.CanRoll == true)
        {
            combatSystem.CanRoll = false;
            NumberRolled = Random.Range(1, 6);
            DiceText.GetComponent<UnityEngine.UI.Text>().text = NumberRolled.ToString("F0");
            ButtonPressed = false;
            
            Enemy enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

            if (enemy.defenceNumber >= NumberRolled) //Enemy defends itself. Player misses
            {
                //PlayerMiss = true;
                Invoke("EnemyTurn", 2f); //This is a problem. 2 seconds where button can be used. 
                combatSystem.PlayerMissText.SetActive(true);
                Invoke("FeedbackTextDisappear", 2f);

                if (FirstTimePlayerMiss == true && playerMissExplanation != null)
                {
                    playerMissExplanation.SetActive(true);
                    Invoke("MissTextDisappear", 3f);
                    FirstTimePlayerMiss = false;
                }
                

                AttackScript attackScript = GameObject.FindWithTag("CombatSystem").GetComponent<AttackScript>();
                attackScript.PlayerCanAttack = true;
                combatSystem.CanRoll = false;
            }
            
            if (enemy.defenceNumber < NumberRolled) //Option to deal damage to enemy. Attack buttons appear. 
            {
                combatSystem.state = CombatState.PLAYERCOMBAT;
                ConsoleText.text = "Your turn";

                AttackScript attackScript = GameObject.FindWithTag("CombatSystem").GetComponent<AttackScript>();
                attackScript.PlayerCanAttack = true;
            }
        }
    }

    public void EnemyTurn()
    {
        CombatSystem combatSystem = GameObject.FindWithTag("CombatSystem").GetComponent<CombatSystem>();
        combatSystem.state = CombatState.ENEMYTURN;
        combatSystem.EnemyCanRoll = true;

        ConsoleText.text = "Enemy Turn";
    }

    public void ReplayLvl1() //Replay scene button
    {
        SceneManager.LoadScene("Level1");
    }

    public void ReplayLvl2() //Replay scene button
    {
        SceneManager.LoadScene("Level2");
    }

    public void ReplayLvl3() //Replay scene button
    {
        SceneManager.LoadScene("Level3");
    }

    void MissTextDisappear()
    {
        playerMissExplanation.SetActive(false);
    }

    void FeedbackTextDisappear()
    {
        CombatSystem combatSystem = GameObject.FindWithTag("CombatSystem").GetComponent<CombatSystem>();
        combatSystem.PlayerMissText.SetActive(false);
    }
}
