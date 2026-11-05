using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private PersonType person;
    [SerializeField] private EnemyType enemy;
    [SerializeField] private int maxRounds = 100;

    private Person personBattle;
    private Person enemyBattle;

    
    void Awake()
    {
        personBattle = CurrentPerson.Create(person);
        enemyBattle = CurrentPerson.Create(enemy);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start Battle!");

        if (personBattle == null || enemyBattle == null)
        {    
            Debug.Log("Выберите противников в inspector");
        }
        else Round();
    }

    private void Round(){
        int roundNumber = 0;

        while (enemyBattle.health > 0 && personBattle.health > 0 && roundNumber < maxRounds)
        {
            roundNumber++;

            int personDamage = personBattle.GetDamage();
            int enemyDamage = personBattle.GetDamage();
            
            enemyBattle.health -= personDamage;

            if (enemyBattle.health > 0)
            {
                personBattle.health -= enemyDamage;
            }

            if (personBattle.health > 0 && enemyBattle.health > 0)
                Debug.Log($"{person} нанёс {personDamage} урона, у противника {enemyBattle.health} ХП; {enemy} нанёс {enemyDamage} урона, у героя {personBattle.health}");
        }

        Debug.Log("Бой завершён");
        if (personBattle.health > 0 && enemyBattle.health <= 0)
            Debug.Log($"Winner {person}, {personBattle.health} ХП осталось");
        else if (enemyBattle.health > 0 && personBattle.health <= 0)
            Debug.Log($"Winner {enemy}, {enemyBattle.health} ХП осталось");
        else if (roundNumber >= maxRounds && personBattle.health > 0 && enemyBattle.health > 0)
            Debug.Log("Ничья: Лимит раундов");
        else
            Debug.Log("Ничья");
    }
}

