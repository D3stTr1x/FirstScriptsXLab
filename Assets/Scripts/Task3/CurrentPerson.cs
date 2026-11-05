using UnityEngine;

//public enum PersonType {Empty, Warrior, Mage}; //enum отдельным скриптом
//public enum EnemyType {Empty, Goblin, Ork, Troll};

public class UnitData
{
    public Elements element;
    public WeaponType weapon;
    
    public string name; //можжет лучше что-то сделать приватным?
    
    public int health;
    public int minDamage;
    public int maxDamage;
    
    public float armor;
    public float criticalChance;
    

    public UnitData(string name, int health, int minDamage, int maxDamage, float armor, float criticalChance, Elements element, WeaponType weapon)
    {
        this.name = name;
        this.health = health;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.armor = armor;
        this.criticalChance = criticalChance;
        this.element = element;
        this.weapon = weapon;
    }
}

public class Person //свойства, вроде, лучше в начале скрипта объявлять 
{
    public string Name { get; }
    public int Health { get; private set; }
    public int MinDamage { get; }
    public int MaxDamage { get; }
    public float Armor { get; }
    public float CriticalChance { get; }
    public Elements Element { get; }
    public WeaponType Weapon { get; }

    public bool IsAlive => Health > 0;

    public Person(UnitData data)
    {
        Name = data.name;
        Health = data.health;
        MinDamage = data.minDamage;
        MaxDamage = data.maxDamage;
        Armor = data.armor;
        CriticalChance = data.criticalChance;
        Element = data.element;
        Weapon = data.weapon;
    }

    public int GetDamage()
    {
        return Random.Range(MinDamage, MaxDamage + 1);
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;
        Health = Mathf.Max(0, Health - amount);
    }
}

public static class UnitDatabase
{
    public static UnitData Get(PersonType type)
    {
        switch (type)
        {
            case PersonType.Warrior: 
                return new UnitData("Warrior", 100, 10, 16, 3f, 0.10f, 
                                    Elements.Fire,  WeaponType.Sword);
            case PersonType.Mage:    
                return new UnitData("Mage",     130, 6,  12, 1f, 0.15f, 
                                    Elements.Magic, WeaponType.Magic);
            default: return null;
        }
    }

    public static UnitData Get(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Goblin: 
                return new UnitData("Goblin", 50,  4, 8,  1f, 0.05f, 
                                    Elements.Ice,   WeaponType.Knife);
            case EnemyType.Ork:    
                return new UnitData("Ork",    80,  5, 10, 2f, 0.05f, 
                                    Elements.Empty, WeaponType.None);
            case EnemyType.Troll:  
                return new UnitData("Troll",  140, 8, 12, 3f, 0.05f, 
                                    Elements.Ice,   WeaponType.None);
            default: return null;
        }
    }

    public static Person Create(PersonType type)
    {
        UnitData data = Get(type);
        return data == null ? null : new Person(data);
    }

    public static Person Create(EnemyType type)
    {
        UnitData data = Get(type);
        return data == null ? null : new Person(data);
    }
}
