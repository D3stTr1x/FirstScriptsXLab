using UnityEngine;

public enum Elements {Empty, Fire, Ice, Magic}

public class DamageCalculator
{
    public static int CalculateDamage(float baseDamage, float armor, float criticalChance, Elements attackerElement, Elements targetElement, float weaponBonus = 0f)
    {
        float damage = baseDamage - armor;

        if (damage <= 1f)
            damage = 1f;

        bool hasElements = attackerElement != Elements.Empty && targetElement != Elements.Empty;

        if (hasElements && attackerElement == targetElement)
        {
            Debug.Log("Иммунитет");
            return 0;
        }

        if (hasElements && attackerElement != targetElement)
            damage *= 1.5f;

        if (Random.value < criticalChance)
            damage *= 2f;

        damage += weaponBonus;

        return Mathf.FloorToInt(damage);
    }
}
