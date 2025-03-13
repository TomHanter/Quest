using System;
using UnityEngine;

public class MiniGamePlayer : MonoBehaviour
{
    // ============================
    // ��������� ������� ���������� ������
    // ============================
    [SerializeField] private string playerName;  // ��� ������
    [SerializeField] private uint maxHealth;     // ������������ ��������
    [SerializeField] private uint health;        // ������� ��������
    [SerializeField] private uint damage;        // ����
    [SerializeField] private float speed;        // ��������
    [SerializeField] private float speedModifier;// ��������
    [SerializeField] private uint healingAmount; // ���������� �������
    /*[SerializeField] private*/public bool isDead = false;// ���������� �������

    private bool underDebufff;

    public string Name
    {
        get => playerName;
        set => playerName = value;
    }

    public uint MaxHealth => maxHealth;
    public uint Health
    {
        get => health;
        set => health = value;
    }
    public uint Damage => damage;

    public float SpeedModifier
    {
        get => speedModifier;
        set
        {
            //Debug.Log($"Setting speed: {value}, current speed: {speed}");
            speedModifier = value;
            OnSpeedChanged?.Invoke(speedModifier, underDebufff); // �������� ������� ��� ��������� ��������
        }
    }

    public float Speed
    {
        get => speed;
    }

    public uint HealingAmount => healingAmount;

    public event Action<float, bool> OnSpeedChanged;

    private void OnEnable()
    {
        ResetHealth();
    }

    // Unity-����� ��� ��������� �������������
    private void Start()
    {
        //this.health = this.maxHealth;
        //Debug.Log($"{Name} ������ � {health} �������� � ��������� {speed}");
    }

    public void Initialize(string playerName, uint maxHp, uint startHealth, uint dmg, float initialSpeed, uint healAmount)
    {
        Name = playerName;
        maxHealth = maxHp;
        health = startHealth;
        damage = dmg;
        speedModifier = initialSpeed;
        healingAmount = healAmount;
    }

    public void TakeDamage(uint damage)
    {
        health = health >= damage ? health - damage : 0;
        //Debug.Log($"{Name} ������� ����. ��������: {health}");
    }

    public void TakeHeal()
    {
        health += healingAmount;
        if (health > maxHealth) health = maxHealth;
        //Debug.Log($"{Name} ���������. ��������: {health}");
    }

    public void TakeSpeedboost(float speedMultiplier, bool isDebuff)
    {
        underDebufff = isDebuff;
        //Debug.Log($"{Name} ������� ���������� ��� {speedMultiplier}");
        SpeedModifier = (float)speedMultiplier; // �������� ��������, ������� �������
        //Debug.Log($"{Name} ����� �������� {Speed}");
    }

    private void ResetHealth()
    {
        health = maxHealth;
        isDead = false;
    }
}
