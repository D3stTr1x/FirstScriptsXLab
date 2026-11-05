using UnityEngine;
using UnityEngine.InputSystem;

    //во всех скриптах прописать namespace

public class Damage : MonoBehaviour
{
    [SerializeField] private float m_health;
    [SerializeField] private float m_criticalHit;
    [SerializeField] private float m_armor;
    
    [SerializeField] private Elements m_elementRb;
    [SerializeField] private Elements m_elementPlayer;

    [SerializeField] private int m_baseDamage;

    private void Update()
    {
        CalculateDamage();
    }

    private void CalculateDamage()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)//{}
        {
            return;
        }
        
        if (m_health <= 0)
        {
            return;
        }

        int finalDamage = DamageCalculator.CalculateDamage(m_baseDamage, m_armor,
            m_criticalHit, m_elementPlayer, m_elementRb);

        if (finalDamage < 0)
        {
            finalDamage = 0;
        }
        
        m_health -= finalDamage;
        
        if (m_health <= 0)
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
