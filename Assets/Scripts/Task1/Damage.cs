using UnityEngine;
using UnityEngine.InputSystem;

    //во всех скриптах прописать namespace

public class Damage : MonoBehaviour
{
    [SerializeField] private float m_health;  //порядок обьявления переменных: float рядом с float, int с int и т.п.
    [SerializeField] private float m_criticalHit;
    [SerializeField] private float m_armor;
    
    [SerializeField] private Elements m_elementRb; //лучше после [SerializeField] ставить enter
    [SerializeField] private Elements m_elementPlayer;

    [SerializeField] private int m_baseDamage;

    // Update is called once per frame
    private void Update() //не указан модификатор доступа
    {
        CalculateDamage();
    }

    private void CalculateDamage()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)//{}
        {
            return;
        }
        
        if (m_health <= 0)//{}
        {
            return;
        }

        int finalDamage = DamageCalculator.CalculateDamage(m_baseDamage, m_armor,
            m_criticalHit, m_elementPlayer, m_elementRb); //длинная строка, разбить на две

        if (m_health - finalDamage <= 0)
        {
            m_health = 0;
        }
        else 
        {
            m_health -= finalDamage; //лучше сделать ограничение урона не меньше нуля, иначе здоровье может пойти в плюс
        }

        if (m_health <= 0) //перед фигурной скобкой забыл enter
        { 
            Destroy(gameObject);
            Debug.Log("Враг уничтожен!");
        }

        if (finalDamage > 0 && m_health >= 1)
        {
            Debug.Log("Урон: " + finalDamage + "\tХп: " + m_health);
        }
    }
}
