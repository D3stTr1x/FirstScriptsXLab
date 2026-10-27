using UnityEngine;

public class Battle : MonoBehaviour
{
    private enum person {Empty, Warrior, Mage};
    private enum enemy {Empty, Goblin, Ork, Troll};

    private person choosePerson;
    private enemy chooseEnemy;


    
    void Awake()
    {
        switch (choosePerson)
        {
            case person.Enemy:
                Debug.Log("Choose person");
            case 1:
                
            default:
        }:

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Warrior : Person{
    public void Warrior(){
        health = 100;
        minDamage = 10;
        maxDamage = 16;
    }
}

public class Mage : Person{
    public void Mage(){
        health = 130;
        minDamage = 6;
        maxDamage = 12;
    }
}

public class Goblin : Person{
    public void Goblin{
        health = 50;
        minDamage = 4;
        maxDamage = 8;
    }
}

public class Ork : Person{
    public void Ork{
        health = 80;
        minDamage = 5;
        maxDamage = 10;
    }
}

public class Troll : Person{
    public void Troll{
        health = 140;
        minDamage = 8;
        maxDamage = 12;
    }
}

public abstract class Person
{
    int health;
    int minDamage;
    int maxDamage;
}
