using UnityEngine;

public class DamageModif
{
    public static void CalculateDamage(Person attacker, Person target)
    {
        if (attacker == null || target == null)
        {
            return;
        }

        if (!attacker.IsAlive || !target.IsAlive)
        {
            return;
        }

        int damage = DamageCalculator.CalculateDamage(attacker, target);
        target.TakeDamage(damage);

        if (!target.IsAlive)
        {
            Debug.Log($"{target.Name} уничтожен!");
        }
        else if (damage > 0)
        {
            Debug.Log($"Нанесено {damage} урона от {attacker.Name} в {target.Name}");
        }
    }
}
