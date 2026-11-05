using UnityEngine;
using System.Collections.Generic;

public class BattleModif : MonoBehaviour
{
    [SerializeField] public PersonType[] person;
    [SerializeField] public EnemyType[] enemy;
    [SerializeField] private int maxRounds = 1000;

    public List<Person> personTeam = new List<Person>();
    public List<Person> enemyTeam = new List<Person>();

    public Person personBattle;
    public Person enemyBattle;
    
    void Awake()
    {
        if (person != null)
        {
            foreach (var p in person)
            {
                Person created = CurrentPerson.Create(p);
                if (created != null)
                    personTeam.Add(created);
            }
        }

        if (enemy != null)
        {
            foreach (var e in enemy)
            {
                Person created = CurrentPerson.Create(e);
                if (created != null)
                    enemyTeam.Add(created);
            }
        }
        personBattle = personTeam.Count > 0 ? personTeam[0] : null;
        enemyBattle = enemyTeam.Count > 0 ? enemyTeam[0] : null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start Battle!");
        if (personTeam.Count == 0 || enemyTeam.Count == 0)
            Debug.Log("Выберите противников в inspector");
        else Round();
    }

    private void Round(){
        if (personTeam.Count == 0 || enemyTeam.Count == 0)
            return;

        int roundNumber = 0;
             
        while (HasAlive(personTeam) && HasAlive(enemyTeam) && roundNumber < maxRounds)
        {
            roundNumber++;

            foreach (var per in personTeam)
            {
                if (per.health <= 0) continue;
                Person target = GetFirstAlive(enemyTeam);
                if (target == null) break;
                DamageModif.CalculateDamage(per, target);
            }

            foreach (var enem in enemyTeam)
            {
                if (enem.health <= 0) continue;
                Person target = GetFirstAlive(personTeam);
                if (target == null) break;
                DamageModif.CalculateDamage(enem, target);
            }

            DeleteDead(personTeam);
            DeleteDead(enemyTeam);

            if (HasAlive(personTeam) && HasAlive(enemyTeam))
            {
                Debug.Log(TeamToString(personTeam));
                Debug.Log(TeamToString(enemyTeam));
            }
        }
        Debug.Log("Бой завершён");
        if (HasAlive(enemyTeam) && HasAlive(personTeam))
        {
            Debug.Log("Ничья: лимит раундов");
        }
        else if (HasAlive(enemyTeam))
        {
            Debug.Log("Winner Противники");
            Debug.Log(TeamToString(enemyTeam));
        }
        else if (HasAlive(personTeam))
        {
            Debug.Log("Winner Герои");
            Debug.Log(TeamToString(personTeam));
        }
        else
        {
            Debug.Log("Ничья");
        }
    }

    private string TeamToString(List<Person> team)
    {
        string result = "";
        foreach (var p in team)
            result += $"{p.characterType} ({p.health}HP); ";
        return result;
    }
    
    private void DeleteDead(List<Person> team)
    {
        for (int i = team.Count - 1; i>=0; i--)
        {
            if (team[i].health <=0)
                team.RemoveAt(i);
        }
    }

    private bool HasAlive(List<Person> team)
    {
        foreach (var person in team)
        {
            if (person.health > 0) return true;
        }
        return false;
    }

    private Person GetFirstAlive(List<Person> team)
    {
        foreach (var person in team)
        {
            if (person.health > 0) return person;
        }
        return null;
    }
}