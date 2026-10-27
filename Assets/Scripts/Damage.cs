using UnityEngine;
using UnityEngine.InputSystem;

public class Damage : MonoBehaviour
{
    [SerializeField] private float health;

    private enum Elements {Empty, Fire, Ice};
    [SerializeField] private Elements elementRb;
    [SerializeField] private Elements elementPlayer;

    [SerializeField] private int baseDamage;
    [SerializeField] private float criticalHit;
    [SerializeField] private float armor;
    private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDamage();
    }

    private void CalculateDamage()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (health > 1)
            {
                float currentDamage = baseDamage - armor;
                if (currentDamage > 1)
                {
                    if (elementPlayer != null && elementRb != null && elementRb != Elements.Empty && elementPlayer != Elements.Empty)
                    {
                        if (elementRb != elementPlayer)
                        {
                            currentDamage *= 1.5f;
                            if (Random.value < criticalHit)
                            {
                                currentDamage *= 2f;
                            }
                        }
                        else if (elementRb == elementPlayer)
                        {
                            currentDamage = 0f;
                            Debug.Log("Иммунитет");
                        }
                    }

                }
                else 
                {
                    currentDamage = 1;
                    if (Random.value < criticalHit)
                    {
                        currentDamage *= 2f;
                    }
                }

                health -= currentDamage;

                if (health < 1){
                    Destroy(gameObject);
                    Debug.Log("Враг уничтожен!");
                }
                if (currentDamage >= 1 && health >= 1)
                    Debug.Log("Урон: " + currentDamage + "\tХп: " + health);
                
            }
        } 
    }
}
