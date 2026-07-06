using UnityEngine;

public class S_Health : MonoBehaviour
{
    
    public int maxHealth = 5;
    public int currentHealth;
    
    [SerializeField] private GameObject _deathUI;
    
    private GameObject _gUI;
    private S_HeartChange _hearts;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _gUI = GameObject.FindGameObjectWithTag("GUI");
        _hearts = _gUI.GetComponent<S_HeartChange>();
        
        currentHealth = maxHealth;
    }

    //Function to heal the adventurer
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        _hearts.SetLife(currentHealth);
    }

    //Function to fully heal the adventurer
    public void FullHeal()
    {
        currentHealth = maxHealth;
        _hearts.SetLife(currentHealth);
    }

    //Function to deal damage to the adventurer
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        _hearts.SetLife(currentHealth);
        if (currentHealth == 0)
        {
            Dead();
        }
    }

    //If the Adventurer life fall to 0, put dead screen
    private void Dead()
    {
        Instantiate(_deathUI);
    }
}
