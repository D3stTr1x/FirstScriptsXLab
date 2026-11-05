using UnityEngine;

//public enum Elements { Empty, Fire, Ice, Magic } //енамы выноси одтельным скриптом 
//public enum WeaponType { None, Knife, Sword, Magic }

public static class DamageCalculator
{
    public static int WeaponBonus(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Knife: return 2;
            case WeaponType.Sword: return 4;
            case WeaponType.Magic: return 4;
            default: return 0;
        }
    }

    public static int CalculateDamage(float baseDamage, float armor, 
                                      float criticalChance, Elements attackerElement, 
                                      Elements targetElement, float weaponBonus = 0f)
    {
        bool hasElements = attackerElement != Elements.Empty && targetElement != Elements.Empty;

        if (hasElements && attackerElement == targetElement)
        {
            Debug.Log("Иммунитет");
            return 0;
        }

        float damage = Mathf.Max(baseDamage - armor, 1f);

        if (hasElements)
        {
            damage *= 1.5f;
        }

        if (Random.value < criticalChance)
        {
            damage *= 2f;
        }

        damage += weaponBonus;

        return Mathf.Max(1, Mathf.FloorToInt(damage));
    }

    public static int CalculateDamage(Person attacker, Person target)
    {
        return CalculateDamage(
            attacker.GetDamage(),
            target.Armor,
            attacker.CriticalChance,
            attacker.Element,
            target.Element,
            WeaponBonus(attacker.Weapon));
    }
}
