using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    private ITriggerable currentTriggerable;
    private GameObject interactionIndicator; // ��������� ���������� ��� �������� ����������

    private void OnTriggerEnter(Collider collider)
    {
        currentTriggerable = null;
        currentTriggerable = collider.gameObject.GetComponent<ITriggerable>();

        if (currentTriggerable != null)
        {
            // ���� ��������� InteractionIndicator �� �������
            interactionIndicator = collider.gameObject.transform.Find("InteractionIndicator")?.gameObject;

            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(true); // ���������� ���������
            }
            //Debug.Log("����� ����� ����������������� � ��������.");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<ITriggerable>() == currentTriggerable)
        {
            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(false); // ������������ ���������
            }

            currentTriggerable = null;
            interactionIndicator = null; // ������� ������ �� ���������
            //Debug.Log("����� ������� ���� ��������������.");
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        currentTriggerable = collider.gameObject.GetComponent<ITriggerable>();

        if (currentTriggerable != null)
        {
            //Debug.Log("����� ��������� ������ �������� � ����� �����������������.");
        }
    }

    private void Update()
    {
        bool isInteract = InputManager.GetInstance().GetInteractPressed();
        if (isInteract)
        {
            Debug.Log("isInteract " + isInteract);
        }
        if (currentTriggerable != null)
        {
            Debug.Log("currentTriggerable " + currentTriggerable);
        }
        if (currentTriggerable != null && isInteract)
        {
            //Debug.Log("����� ��������������� � ��������.");
            currentTriggerable.Trrigered();

            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(false); // ������������ ��������� ����� ��������������
            }
        }
    }
}