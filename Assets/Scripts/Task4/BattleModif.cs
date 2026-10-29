using UnityEngine;
using System.Collections.Generic;

public enum PersonType {Empty, Warrior, Mage};
public enum EnemyType {Empty, Goblin, Ork, Troll};

public class BattleModif : MonoBehaviour
{
    

    [SerializeField] public PersonType[] person;
    [SerializeField] public EnemyType[] enemy;

    public List<Person> personTeam = new List<Person>();
    public List<Person> enemyTeam = new List<Person>();

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

    private InventoryModif inventory;

    
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
        //personTeam.Clear();
        //enemyTeam.Clear();
        if (person != null)
        {
            foreach (var p in person)
            {
                Person created = CurrentPerson.Create(p);
                if (created != null)
                    personTeam.Add(created);
            }
        }

        if (enemy != null)
        {
            foreach (var e in enemy)
            {
                Person created = CurrentPerson.Create(e);
                if (created != null)
                    enemyTeam.Add(created);
            }
        }
        personBattle = personTeam.Count > 0 ? personTeam[0] : null;
        enemyBattle = enemyTeam.Count > 0 ? enemyTeam[0] : null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = new InventoryModif();
        Debug.Log("Start Battle!");
        if (personTeam.Count == 0 || enemyTeam.Count == 0)
            Debug.Log("Выберите противников в inspector");
        else Round();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Round(){
        if (personTeam.Count == 0 || enemyTeam.Count == 0)
            return;
             
        while (HasAlive(personTeam) && HasAlive(enemyTeam))
        {
            foreach (var per in personTeam)
            {
                if (per.health <= 0) continue;
                Person target = GetFirstAlive(enemyTeam);
                if (target == null) break;
                DamageModif.CalculateDamage(per, target);
            }

            foreach (var enem in enemyTeam)
            {
                if (enem.health <= 0) continue;
                Person target = GetFirstAlive(personTeam);
                if (target == null) break;
                DamageModif.CalculateDamage(enem, target);
            }

            DeleteDead(personTeam);
            DeleteDead(enemyTeam);

            if (HasAlive(personTeam) && HasAlive(enemyTeam))
            {
                string srtingLog = "";
                foreach (var per in personTeam)
                    srtingLog += $"{per.GetType().Name} ({per.health}HP); ";
                Debug.Log(srtingLog);
                srtingLog = "";
                foreach (var per in enemyTeam)
                    srtingLog += $"{per.GetType().Name} ({per.health}HP); ";
                Debug.Log(srtingLog);
            }
            /*if (enemyBattle.health > 0)
            {
                DamageModif.CalculateDamage(enemyBattle, personBattle);
            }
            if (enemyBattle.health > 0 && personBattle.health > 0)
                Debug.Log($"Осталось HP в сражении: {personBattle.GetType()} - {personBattle.health}HP и {enemyBattle.GetType()} - {enemyBattle.health}HP");
            */
        }
        Debug.Log("Бой завершён");
        if (HasAlive(enemyTeam)){
            Debug.Log($"Winner Противники");
            string srtingLog = "";
            foreach (var per in enemyTeam)
                srtingLog += $"{per.GetType().Name} ({per.health}HP); ";
            Debug.Log(srtingLog);
        }
        else{
            Debug.Log($"Winner Герои");
            string srtingLog = "";
            foreach (var per in personTeam)
                srtingLog += $"{per.GetType().Name} ({per.health}HP); ";
            Debug.Log(srtingLog);
        }
    }
    
    private void DeleteDead(List<Person> team)
    {
        for (int i = team.Count - 1; i>=0; i--)
        {
            if (team[i].health <=0)
                team.RemoveAt(i);
        }
    }

    private bool HasAlive(List<Person> team)
    {
        foreach (var person in team)
        {
            if (person.health > 0) return true;
        }
        return false;
    }

    private Person GetFirstAlive(List<Person> team)
    {
        foreach (var person in team)
        {
            if (person.health > 0) return person;
        }
        return null;
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
        ItemModif sword = new ItemModif("Sword");
        weapon.AddItem(sword);
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
        ItemModif magic = new ItemModif("Magic");
        weapon.AddItem(magic);
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
        ItemModif knife = new ItemModif("Knife");
        weapon.AddItem(knife);
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
    public InventoryModif weapon { get; set; } = new InventoryModif();
}


