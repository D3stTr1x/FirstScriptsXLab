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
            case 0:
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

public class Warrior : IPerson{
    health = 100;
    damage = Random.Range(10,16);
}

public class Mage : IPerson{
    health = 130;
    damage = Random.Range(6,12);
}

public class Goblin : IPerson{
    health = 50;
    damage = Random.Range(4,8);
}

public class Ork : IPerson{
    health = 80;
    damage = Random.Range(5,10);
}

public class Troll : IPerson{
    health = 140;
    damage = Random.Range(8,12);
}

public interface IPerson
{
    public int health;
    public int damage;
}
