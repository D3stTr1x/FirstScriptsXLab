using UnityEngine;

public class Battle : MonoBehaviour
{
    public enum Person {Empty, Warrior, Mage};
    public enum Enemy {Empty, Goblin, Ork, Troll};

    [SerializeField] private Person person;
    [SerializeField] private Enemy enemy;

    private int personHealth;
    private int personMinDamage;
    private int personMaxDamage;
    private string personType;

    private int enemyHealth;
    private int enemyMinDamage;
    private int enemyMaxDamage;
    private string enemyType;


    
    void Awake()
    {
        switch (person)
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
        }
        //personBattle = CurrentPerson.Create(person);
        //enemyBattle = CurrentPerson.Create(enemy);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start Battle!");
        if (enemy == Enemy.Empty || person == Person.Empty)
            Debug.Log("Выберите противников в inspector");
        else Round();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Round(){
        while (enemyHealth > 0 && personHealth > 0)
        {
            int personDamage = Random.Range(personMinDamage, personMaxDamage + 1);
            int enemyDamage = Random.Range(enemyMinDamage, enemyMaxDamage  + 1);
            
            enemyHealth -= personDamage;
            if (enemyHealth > 0)
            {
                personHealth -= enemyDamage;
            }
            if (personHealth > 0 && enemyHealth > 0)
                Debug.Log($"{personType} нанёс {personDamage} урона, у противника {enemyHealth} ХП; {enemyType} нанёс {enemyDamage} урона, у героя {personHealth}");
        }
        Debug.Log("Бой завершён");
        if (enemyHealth >= 0)
            Debug.Log($"Winner {enemyType}, {enemyHealth} ХП осталось");
        else
            Debug.Log($"Winner {personType}, {personHealth} ХП осталось");
    }

/*    private bool CheckHP(Person p){
        if (p.health <= 0)
            return false;
        return true;
    }
    */
}

/*public static class CurrentPerson {
    public static Person Create(person type)
    {
        switch (person)
        {
            case person.Warrior:
                return new Warrior();
            case person.Mage:
                return new Mage();
            default:
                return null;
        }
    }

    public static Person Create(enemy type)
    {
        switch (enemy)
        {
            case enemy.Goblin:
                return new Goblin();
            case enemy.Ork:
                return new Ork();
            case enemy.Troll:
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
    }
}

public class Mage : Person{
    public Mage(){
        health = 130;
        minDamage = 6;
        maxDamage = 12;
    }
}

public class Goblin : Person{
    public Goblin(){
        health = 50;
        minDamage = 4;
        maxDamage = 8;
    }
}

public class Ork : Person{
    public Ork(){
        health = 80;
        minDamage = 5;
        maxDamage = 10;
    }
}

public class Troll : Person{
    public Troll(){
        health = 140;
        minDamage = 8;
        maxDamage = 12;
    }
}

public abstract class Person
{
    public int Health { get; set; }
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }
}
*/

