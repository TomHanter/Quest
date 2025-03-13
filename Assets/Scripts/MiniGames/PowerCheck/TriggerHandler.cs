using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public System.Action<GameObject, GameObject> OnObjectEnteredTrigger; // ������� ��� �������� ������

    private void OnTriggerEnter(Collider other)
    {

        if (!gameObject.activeSelf || !other.gameObject.activeSelf)
        {
            return; 
        }
        //Debug.Log($"������ {other.name} ����� � ������� {gameObject.name}");
        OnObjectEnteredTrigger?.Invoke(gameObject, other.gameObject); // ����� �������
    }
}