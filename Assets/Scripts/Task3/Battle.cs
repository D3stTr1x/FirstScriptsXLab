using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private PersonType m_person;
    [SerializeField] private EnemyType m_enemy;
    [SerializeField] private int m_maxRounds = 100;

    private m_person personBattle;
    private m_person enemyBattle;

    void Awake()
    {
        personBattle = UnitDatabase.Create(m_person);
        enemyBattle = UnitDatabase.Create(m_enemy);
    }

    void Start()
    {
        Debug.Log("Start Battle!");

        if (personBattle == null || enemyBattle == null)
            Debug.Log("Выберите противников в inspector");
        else
            Round();
    }

    private void Round()
    {
        int roundNumber = 0;

        while (personBattle.IsAlive && enemyBattle.IsAlive && roundNumber < m_maxRounds)
        {
            roundNumber++;

            int personDamage = DamageCalculator.CalculateDamage(personBattle, enemyBattle);
            enemyBattle.TakeDamage(personDamage);

            int enemyDamage = 0;
            if (enemyBattle.IsAlive)
            {
                enemyDamage = DamageCalculator.CalculateDamage(enemyBattle, personBattle);
                personBattle.TakeDamage(enemyDamage);
            }

            if (personBattle.IsAlive && enemyBattle.IsAlive)
                Debug.Log($"{personBattle.Name} нанёс {personDamage} урона, у противника {enemyBattle.Health} ХП; " +
                          $"{enemyBattle.Name} нанёс {enemyDamage} урона, у героя {personBattle.Health}");
        }

        Debug.Log("Бой завершён");
        if (personBattle.IsAlive && !enemyBattle.IsAlive)
            Debug.Log($"Winner {personBattle.Name}, {personBattle.Health} ХП осталось");
        else if (enemyBattle.IsAlive && !personBattle.IsAlive)
            Debug.Log($"Winner {enemyBattle.Name}, {enemyBattle.Health} ХП осталось");
        else if (personBattle.IsAlive && enemyBattle.IsAlive)
            Debug.Log("Ничья: лимит раундов");
        else
            Debug.Log("Ничья");
    }
}
