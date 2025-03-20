using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position; // Используйте встроенное свойство transform
    }

    void Update()
    {
        // Если позиция изменилась, то запускаем анимацию ходьбы
        if (transform.position != lastPosition)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }

        // Обновляем позицию для следующей проверки
        lastPosition = transform.position;
    }
}
