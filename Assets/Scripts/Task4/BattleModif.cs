using UnityEngine;

public enum PersonType {Empty, Warrior, Mage};
public enum EnemyType {Empty, Goblin, Ork, Troll};

public class BattleModif : MonoBehaviour
{
    

    [SerializeField] public PersonType person;
    [SerializeField] public EnemyType enemy;

    private int personHealth;
    private int personMinDamage;
    private int personMaxDamage;
    private string personType;

    private int enemyHealth;
    private int enemyMinDamage;
    private int enemyMaxDamage;
    private string enemyType;

    public Person personBattle;
    public Person enemyBattle;

    
    void Awake()
    {
        /*switch (person)
        {
            case Person.Warrior:
            {
                personHealth = 100;
                personMinDamage = 10;
                personMaxDamage = 16;
                personType = "Warrior";
                break;
            }
            case Person.Mage:
            {
                personHealth = 130;
                personMinDamage = 6;
                personMaxDamage = 12;
                personType = "Mage";
                break;
            }
            default:
                return;
        }
        switch (enemy)
        {
            case Enemy.Goblin:{
                enemyHealth = 50;
                enemyMinDamage = 4;
                enemyMaxDamage = 8;
                enemyType = "Goblin";
                break;
            }
            case Enemy.Ork:{
                enemyHealth = 80;
                enemyMinDamage = 5;
                enemyMaxDamage = 10;
                enemyType = "Ork";
                break;
            }
            case Enemy.Troll:{
                enemyHealth = 140;
                enemyMinDamage = 8;
                enemyMaxDamage = 12;
                enemyType = "Troll";
                break;
            }
            default:
                return;
        }*/
        personBattle = CurrentPerson.Create(person);
        enemyBattle = CurrentPerson.Create(enemy);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start Battle!");
        if (personBattle == null || enemyBattle == null)
            Debug.Log("Выберите противников в inspector");
        else Round();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Round(){
        if (personBattle == null || enemyBattle == null)
            return;
             
        while (enemyBattle.health > 0 && personBattle.health > 0)
        {
            DamageModif.CalculateDamage(personBattle, enemyBattle);
            
            if (enemyBattle.health > 0)
            {
                DamageModif.CalculateDamage(enemyBattle, personBattle);
            }
            Debug.Log($"Осталось HP в сражении: {personBattle.GetType()} - {personBattle.health}HP и {enemyBattle.GetType()} - {enemyBattle.health}HP");

        }
        Debug.Log("Бой завершён");
        if (enemyHealth >= 0)
            Debug.Log($"Winner {enemyBattle.GetType()}, {enemyBattle.health} ХП осталось");
        else
            Debug.Log($"Winner {personBattle.GetType()}, {personBattle.health} ХП осталось");
    }
}

public static class CurrentPerson {
    public static Person Create(PersonType type)
    {
        switch (type)
        {
            case PersonType.Warrior:
                return new Warrior();
            case PersonType.Mage:
                return new Mage();
            default:
                return null;
        }
    }

    public static Person Create(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Goblin:
                return new Goblin();
            case EnemyType.Ork:
                return new Ork();
            case EnemyType.Troll:
                return new Troll();
            default:
                return null;
        }
    }
}

public class Warrior : Person{
    public Warrior(){
        health = 100;
        minDamage = 10;
        maxDamage = 16;
        armor = 2;
        criticalHit = 0.1f;
        element = Elements.Empty;
    }
}

public class Mage : Person{
    public Mage(){
        health = 130;
        minDamage = 6;
        maxDamage = 12;
        armor = 0;
        criticalHit = 0.2f;
        element = Elements.Fire;
    }
}

public class Goblin : Person{
    public Goblin(){
        health = 50;
        minDamage = 4;
        maxDamage = 8;
        armor = 1;
        criticalHit = 0.1f;
        element = Elements.Empty;
    }
}

public class Ork : Person{
    public Ork(){
        health = 80;
        minDamage = 5;
        maxDamage = 10;
        armor = 3;
        criticalHit = 0.2f;
        element = Elements.Ice;
    }
}

public class Troll : Person{
    public Troll(){
        health = 140;
        minDamage = 8;
        maxDamage = 12;
        armor = 0;
        criticalHit = 0.3f;
        element = Elements.Empty;
    }
}

public abstract class Person
{
    public int health {get; set;}
    public int minDamage {get; set;}
    public int maxDamage {get; set;}
    public int armor {get;set;}
    public float criticalHit{get;set;}
    public Elements element{get;set;}
}


