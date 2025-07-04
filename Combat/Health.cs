using System;
using UnityEngine;

public class Health
{
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    
    public bool isCanTakeDamage = true;
    
    public event Action<int, int> OnDamageEvent;
    public event Action<int, int> OnHealEvent;
    public event Action OnHealthChanged;
    public event Action OnDeadEvent;

    public void SetHealth(int health)
    {
        MaxHealth = health;
        CurrentHealth = health;
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (!isCanTakeDamage) return;

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        OnDamageEvent?.Invoke(CurrentHealth, MaxHealth);
        OnHealthChanged?.Invoke();

        if (CurrentHealth <= 0) OnDeadEvent?.Invoke();
    }

    public void TakeHeal(int value)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + value, MaxHealth);
        OnHealthChanged?.Invoke();
        OnHealEvent?.Invoke(CurrentHealth, MaxHealth);
    }
}
