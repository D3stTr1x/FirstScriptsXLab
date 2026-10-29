using UnityEngine;
using UnityEngine.InputSystem;
using System;

public enum Elements {Empty, Fire, Ice, Magic};

public class DamageModif : MonoBehaviour
{
    
    //[SerializeField] private float health;


    //[SerializeField] private Elements elementRb;
    //[SerializeField] private Elements elementPlayer;

    //[SerializeField] private int baseDamage;
    //[SerializeField] private float criticalHit;
    //[SerializeField] private float armor;
    //private Rigidbody rb;

    [SerializeField] private BattleModif battle;
    [SerializeField] private bool isEnemy;

    private static Person currentPerson;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPerson = isEnemy ? battle.enemyBattle : battle.personBattle;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void CalculateDamage(Person attacker, Person target)
    {
        if (currentPerson == null || attacker == null)
            return;

        if (currentPerson.health <= 0)
            return;

        int damage = UnityEngine.Random.Range(attacker.minDamage, attacker.maxDamage + 1);


            if (target.health > 1)
            {
                float currentDamage = damage - target.armor;
                if (currentDamage > 1)
                {
                    if (attacker.element != Elements.Empty && target.element != Elements.Empty)
                    {
                        if (attacker.element != target.element)
                        {
                            currentDamage *= 1.5f;
                            if (UnityEngine.Random.value < attacker.criticalHit)
                            {
                                currentDamage *= 2f;
                            }
                        }
                        else
                        {
                            currentDamage = 0f;
                            Debug.Log("Иммунитет");
                        }
                    }

                }
                else 
                {
                    currentDamage = 1;
                    if (UnityEngine.Random.value < attacker.criticalHit)
                    {
                        currentDamage *= 2f;
                    }
                }

                currentDamage = Mathf.CeilToInt(currentDamage);
                target.health -= Convert.ToInt32(currentDamage);

                if (target.health < 1){
                    Debug.Log("Враг уничтожен!");
                }
                else 
                    Debug.Log($"Нанесено {currentDamage} урона");
                
            }
         
    }
}
