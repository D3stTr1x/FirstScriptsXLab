using UnityEngine;

public class DamageModif : MonoBehaviour
{
    [SerializeField] private BattleModif battle;
    [SerializeField] private bool isEnemy;

    private static Person currentPerson;

    void Start()
    {
        if (battle != null)
            currentPerson = isEnemy ? battle.enemyBattle : battle.personBattle;
    }

    public static void CalculateDamage(Person attacker, Person target)
    {
        if (target == null || attacker == null)
            return;

        if (attacker.health <= 0 || target.health <= 0)
            return;

        int damage = UnityEngine.Random.Range(attacker.minDamage, attacker.maxDamage + 1);
        float currentDamage = damage - target.armor;

        if (currentDamage > 1)
        {
            if (attacker.element != Elements.Empty && target.element != Elements.Empty)
            {
                if (attacker.element != target.element)
                {
                    currentDamage *= 1.5f;

                    if (UnityEngine.Random.value < attacker.criticalHit)
                    {
                        currentDamage *= 2f;
                    }
                            
                    if (attacker.weapon != null)
                    {
                        if (attacker.weapon.name == "Knife")
                        {
                            currentDamage += 2;
                        }
                        else if (attacker.weapon.name == "Sword")
                        {
                            currentDamage += 4;
                        }
                        else if (attacker.weapon.name == "Magic" && attacker.characterType == "Mage")
                        {
                            currentDamage += 4;
                        }
                    }
                }
                else
                {
                    currentDamage = 0f;
                    Debug.Log("Иммунитет");
                }
            }
        }
        else 
        {
            currentDamage = 1;
            if (UnityEngine.Random.value < attacker.criticalHit)
            {
                currentDamage *= 2f;
            }
        }

        int finalDamage = Mathf.FloorToInt(currentDamage);
        target.health -= finalDamage;

        if (target.health <= 0){
            Debug.Log("Враг уничтожен!");
        }
        else 
            Debug.Log($"Нанесено {currentDamage} урона от {attacker.GetType()} в {target.GetType()}");
                
    }
}
