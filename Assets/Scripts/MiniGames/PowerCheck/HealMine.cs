using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMine : Mine
{
    public HealMine(uint number, float �ooldown, GameObject mine) : base(number, �ooldown, mine) {}

    public void Heal(MiniGamePlayer player)
    {
        player.TakeHeal();
    }
}
