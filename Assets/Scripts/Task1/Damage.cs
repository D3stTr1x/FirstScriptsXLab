using UnityEngine;
using UnityEngine.InputSystem;

public class Damage : MonoBehaviour
{
    [SerializeField] private float health;

    private enum Elements {Empty, Fire, Ice, Magic};
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
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;
        
        if (health <= 0)
            return;

        float currentDamage = baseDamage - armor;
        bool immun = false;

        if (currentDamage <= 1)
            currentDamage = 1;

        if (elementPlayer != null && elementRb != null && elementRb != Elements.Empty && elementPlayer != Elements.Empty)
            {
                if (elementRb != elementPlayer)
                {
                    currentDamage *= 1.5f;
                }
                
                else
                {
                    currentDamage = 0f;
                    immun = true;
                    Debug.Log("Иммунитет");
                }
            }

            if (!immune && currentDamage > 0 && Random.value < criticalHit)
                currentDamage *= 2f;

            currentDamage = Mathf.FloorToInt(currentDamage);
            health -= currentDamage;
        }

        if (health <= 0){
            Destroy(gameObject);
            Debug.Log("Враг уничтожен!");
        }

        if (currentDamage > 0 && health >= 1)
        {
            Debug.Log("Урон: " + currentDamage + "\tХп: " + health);
        }
}

