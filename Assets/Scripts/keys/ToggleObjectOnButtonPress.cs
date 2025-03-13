using UnityEngine;

public class ToggleObjectOnButtonPress : MonoBehaviour
{
    // Сериализуемое поле для кнопки
    [SerializeField] private KeyCode toggleButton = KeyCode.Space;

    // Сериализуемое поле для объекта, который будем включать/выключать
    [field: SerializeField] public GameObject TargetObject { get; private set; }
    //[SerializeField] private GameObject targetObject;

    void Update()
    {
        Activate(Input.GetKeyDown(toggleButton));
    }

    public void Activate(bool isKeyPressed)
    {
        if (isKeyPressed)
        {
            TargetObject.SetActive(!TargetObject.activeSelf);
        }
    }
}