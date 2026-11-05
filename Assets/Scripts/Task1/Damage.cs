using UnityEngine;
using UnityEngine.InputSystem;

public class Damage : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private Elements elementRb;
    [SerializeField] private Elements elementPlayer;

    [SerializeField] private int baseDamage;
    [SerializeField] private float criticalHit;
    [SerializeField] private float armor;

    // Update is called once per frame
    void Update()
    {
        CalculateDamage();
    }

    private void CalculateDamage()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
            return;
        
        if (health <= 0)
            return;

        int finalDamage = DamageCalculator.CalculateDamage(baseDamage, armor, criticalHit, elementPlayer, elementRb);

        health -= finalDamage;

        if (health <= 0){
            Destroy(gameObject);
            Debug.Log("Враг уничтожен!");
        }

        if (finalDamage > 0 && health >= 1)
        {
            Debug.Log("Урон: " + finalDamage + "\tХп: " + health);
        }
    }
}
