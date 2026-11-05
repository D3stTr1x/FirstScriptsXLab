public enum PersonType {Empty, Warrior, Mage};

public enum EnemyType {Empty, Goblin, Ork, Troll};

public class Person
{
    public int health;
    public int minDamage;
    public int maxDamage;
    public int armor;
    public string characterType;
    public float criticalHit { get; set; }
    public Elements element { get; set; }
    public Item weapon { get; set; }

    public Person(int health, int minDamage, int maxDamage, int armor, string characterType, float criticalHit, Elements element, Item weapon)
    {
        this.health = health;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.armor = armor;
        this.characterType = characterType;
        this.criticalHit = criticalHit;
        this.element = element;
        this.weapon = weapon;
    }

    public int GetDamage()
    {
        return UnityEngine.Random.Range(minDamage, maxDamage + 1);
    }
}

public static class CurrentPerson
{
    public static Person Create(PersonType type)
    {
        switch (type)
        {
            case PersonType.Warrior:
                return new Person(100, 10, 16, 3, "Warrior", 0.10f, Elements.Fire, new Item("Sword"));
            case PersonType.Mage:
                return new Person(130, 6, 12, 1, "Mage", 0.15f, Elements.Magic, new Item("Magic"));
            default:
                return null;
        }
    }

    public static Person Create(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Goblin:
                return new Person(50, 4, 8, 1, "Goblin", 0.05f, Elements.Ice, new Item("Knife"));
            case EnemyType.Ork:
                return new Person(80, 5, 10, 2, "Ork", 0.05f, Elements.Empty, null);
            case EnemyType.Troll:
                return new Person(140, 8, 12, 3, "Troll", 0.05f, Elements.Ice, null);
            default:
                return null;
        }
    }
}
