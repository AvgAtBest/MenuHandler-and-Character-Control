using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("FirstPerson/Character Movement")]
public class CharacterHandler : MonoBehaviour
{
    #region Variables
    [Header("Character")]
    #region Character 
    //bool to tell if the player is alive
    public bool alive;
    //connection to players character controller
    public CharacterController controller;
    public Enemy enemyH;
    private GameObject enemyCap;
    private Transform enemy;
    #endregion
    [Header("Health")]
    #region Health
    //max and min health
    public static float maxHealth;
    public static float curHealth;
    public GUIStyle healthBar;
    public GUIStyle healthColor;
    public bool isDead;
    private bool isRegeneratingHealth;
    private float healthRegen = 5;
    #endregion
    [Header("Levels and Exp")]
    #region Level and Exp
    //players current level
    public int bLevel;
    //max and min experience
    public int maxExp, curExp, baseExp;
    #endregion
    [Header("Camera Connection")]
    #region MiniMap
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    #endregion
    #region Stats
    [Header("Stats")]
    //base stats for player
    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public CharacterClass characterClass;
    #endregion
    #region Combat
    [Header("Attack")]
    public float timeBetweenAttacks = 1.5f;
    public int pDamage = 40;
    public float distToTarget = 5f;
    private float timer;
    public float attackRadius = 4f;
    #endregion
    void Start()
    {
        //set current health to max
        maxHealth = 100f;
        curHealth = maxHealth;

        alive = true;
        //make sure player is alive
        //baseExp = 0;
        //curExp = baseExp;
        maxExp = 60;
        //max exp starts at 60
        bLevel = 1;

        controller = this.GetComponent<CharacterController>();
        //connect the Character Controller to the controller variable
        enemyCap = GameObject.FindGameObjectWithTag("Enemy");
        enemyH = enemyCap.GetComponent<Enemy>();
        enemy = enemyCap.GetComponent<Transform>();
        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charism" };
        for (int i = 0; i < statArray.Length; i++)
        {
            stats[i] = PlayerPrefs.GetInt(statArray[i], 10);
        }
        characterClass = (CharacterClass)System.Enum.Parse(typeof(CharacterClass), PlayerPrefs.GetString("CharacterClass", "Barbarian"));
        //Sets characterClass from the characterClass enum, gets the "CharacterClas" save file and and sets the class listed in characterClass. If it doesn't, it sets the default class as "Barbarian"

    }

    // Update is called once per frame
    void Update()
    {
        //if our current experience is greater or equal to the maximum experience
        if (curExp >= maxExp)
        {
            curExp -= maxExp;
            //then the current experience is equal to our experience minus the maximum amount of experience
            bLevel++;
            //our level goes up by one
            maxExp += 50;
            //the maximum amount of experience is increased by 50
        }
        if (curHealth != maxHealth && !isRegeneratingHealth)
        {
            StartCoroutine(RegenHealth());
        }
        timer += Time.deltaTime;
        distToTarget = Vector3.Distance(transform.position, enemy.position);
        if (curHealth > 0)
        {
            if (distToTarget < attackRadius && timer >= timeBetweenAttacks && enemyH.maxHealth > 0)
            {
                Attack();
                if (enemyH.maxHealth <= 0)
                {
                    Destroy(enemy);
                }
            }

        }
    }
    private void LateUpdate()
    {
        //if our current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth < 0 || !alive)
        {
            curHealth = 0;
        }

        if (alive)
        {
            if (curHealth == 0)
            {
                alive = false;
                controller.enabled = false;
                Debug.Log("Disabled on death");
            }
        }
    }
    public void OnGUI()
    {
        //set up our aspect ratio for the GUI elements
        //scrW - 16
        float scrW = Screen.width / 16;
        //scrH - 9
        float scrH = Screen.height / 9;

        if (!PausedMenu.paused)
        {
            //GUI Box on screen for the healthbar background
            GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), "");
            //GUI Box for current health that moves in same place as the background bar
            //current Health divided by the posistion on screen and timesed by the total max health
            GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "", healthColor);
            //GUI Box on screen for the experience background
            GUI.Box(new Rect(6 * scrW, 0.75f * scrH, 4 * scrW, 0.25f * scrH), "");
            //GUI Box for current experience that moves in same place as the background bar
            //current experience divided by the posistion on screen and timesed by the total max experience
            GUI.Box(new Rect(6 * scrW, 0.75f * scrH, curExp * (4 * scrW) / maxExp, 0.25f * scrH), "");
            //GUI Draw Texture on the screen that has the mini map render texture attached
            GUI.DrawTexture(new Rect(13.75f * scrW, 0.25f * scrH, 2 * scrW, 2 * scrH), miniMap);
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Enemy>();
        if (other.gameObject.tag == "Enemy")
        {
            //enemy = other.gameObject;
            Attack();
        }
    }

    public void Attack()
    {
        timer = 0f;
        enemyH.TakeDamage(pDamage);
        if (enemyH.maxHealth < 0)
        {
            GainEXP();

        }
    }
    public void GainEXP()
    {
        curExp += 20;
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0) //&& !isDead)
        {
            Dead();
        }
    }
    private IEnumerator RegenHealth()
    {
        isRegeneratingHealth = true;
        while (curHealth < maxHealth)
        {
            HealthRegen();
            yield return new WaitForSeconds(1);
        }
        isRegeneratingHealth = false;
    }
    void HealthRegen()
    {
        curHealth += healthRegen;
    }
    public void Dead()
    {
        isDead = true;
        controller.enabled = false;
    }
    #endregion
}