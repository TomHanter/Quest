using UnityEngine;
using System;

public class Lever : MonoBehaviour, ITriggerable
{
    public event Action OnActivated;
    public void Trrigered()
    {
        //Debug.Log("����� �����������!");
        // ������ ��������� ������

        OnActivated?.Invoke();
    }
}
