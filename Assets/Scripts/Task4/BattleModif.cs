using UnityEngine;
using System.Collections.Generic;

public class BattleModif : MonoBehaviour
{
    [SerializeField] public PersonType[] person;
    [SerializeField] public EnemyType[] enemy;
    [SerializeField] private int maxRounds = 1000;

    public List<Person> personTeam = new List<Person>();
    public List<Person> enemyTeam = new List<Person>();

    private void Awake()
    {
        if (person != null)
        {
            foreach (var p in person)
            {
                Person created = UnitDatabase.Create(p);
                if (created != null)
                    personTeam.Add(created);
            }
        }

        if (enemy != null)
        {
            foreach (var e in enemy)
            {
                Person created = UnitDatabase.Create(e);
                if (created != null)
                    enemyTeam.Add(created);
            }
        }

        if (maxRounds < 1)
            Debug.Log("Меньше одного раунда не допустимо");
    }

    void Start()
    {
        Debug.Log("Start Battle!");
        if (personTeam.Count == 0 || enemyTeam.Count == 0)
        {
            Debug.Log("Выберите противников в inspector");
        }
        else Round();
    }

    private void Round()
    {
        int roundNumber = 0;

        while (HasAlive(personTeam) && HasAlive(enemyTeam) && roundNumber < maxRounds)
        {
            roundNumber++;

            foreach (var hero in personTeam)
            {
                if (!hero.IsAlive) 
                {
                    continue;
                }
                
                Person target = GetFirstAlive(enemyTeam);
                
                if (target == null) 
                {
                    break; 
                }
                
                DamageModif.CalculateDamage(hero, target);
            }

            foreach (var foe in enemyTeam)
            {
                if (!foe.IsAlive) 
                {
                    continue;
                }
                
                Person target = GetFirstAlive(personTeam);
                if (target == null) break;
                DamageModif.CalculateDamage(foe, target);
            }

            personTeam.RemoveAll(p => !p.IsAlive);
            enemyTeam.RemoveAll(p => !p.IsAlive);

            if (HasAlive(personTeam) && HasAlive(enemyTeam))
            {
                Debug.Log(TeamToString(personTeam));
                Debug.Log(TeamToString(enemyTeam));
            }
        }

        Debug.Log("Бой завершён");
        bool heroesAlive = HasAlive(personTeam);
        bool enemiesAlive = HasAlive(enemyTeam);

        if (heroesAlive && enemiesAlive)
        {
            Debug.Log("Ничья: лимит раундов");
        }
        else if (enemiesAlive)
        {
            Debug.Log("Winner Противники");
            Debug.Log(TeamToString(enemyTeam));
        }
        else if (heroesAlive)
        {
            Debug.Log("Winner Герои");
            Debug.Log(TeamToString(personTeam));
        }
        else
            Debug.Log("Ничья");
    }

    private string TeamToString(List<Person> team)
    {
        string result = "";
        foreach (var p in team)
            result += $"{p.Name} ({p.Health}HP); ";
        return result;
    }

    private bool HasAlive(List<Person> team)
    {
        return GetFirstAlive(team) != null;
    }

    private Person GetFirstAlive(List<Person> team)
    {
        foreach (var p in team)
        {
            if (p.IsAlive) return p;
        }
        return null;
    }
}
