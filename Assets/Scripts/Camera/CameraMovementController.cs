using UnityEngine;
using Zenject;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private Transform _player; // Ссылка на игрока
    [SerializeField] private Transform cameraTransform; // Ссылка на камеру
    [SerializeField] private Transform _centerLocationObj; // Центр локации с границами движения камеры
    [SerializeField] private float followDelay = 0.3f; // Запаздывание камеры
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10); // Смещение камеры
    [SerializeField] private float tiltAngle = 50f; // Угол наклона камеры
    [SerializeField] private Vector2 boundarySize = new Vector2(5f, 5f);

    [Header("Zoom Settings")]
    [SerializeField] private float minZoom = 5f; // Минимальный зум
    [SerializeField] private float maxZoom = 15f; // Максимальный зум
    [SerializeField] private float zoomSpeed = 2f; // Скорость зума

    private Vector3 velocity = Vector3.zero; // Для плавного перемещения
    [SerializeField] private Vector3 squareCenter;

    [Inject]
    private void Construct(PlayerMoveController player)
    {
        _player = player.gameObject.transform;
    }

    void Start()
    {
        if (_player == null)
        {
            Debug.LogError("Player is not assigned!");
            enabled = false;
            return;
        }

        if (_centerLocationObj == null)
        {
            Debug.LogError("Center Location Object is not assigned!");
            enabled = false;
            return;
        }

        Camera.main.orthographic = false;
        transform.rotation = Quaternion.Euler(tiltAngle, 0, 0);

        squareCenter = _player.position;
    }

    void LateUpdate()
    {
        UpdateSquarePosition();
        FollowPlayer();
        HandleZoom();
    }

    void UpdateSquarePosition()
    {
        Vector3 playerPosition = _player.position;
        Vector3 boundarySize3d = new Vector3(boundarySize.x, 0, boundarySize.y) / 2;
                
        float minX = squareCenter.x - boundarySize3d.x;
        float maxX = squareCenter.x + boundarySize3d.x;
        float minZ = squareCenter.z - boundarySize3d.z;
        float maxZ = squareCenter.z + boundarySize3d.z;

        if (playerPosition.x < minX || playerPosition.x > maxX ||
            playerPosition.z < minZ || playerPosition.z > maxZ)
        {
            squareCenter = Vector3.SmoothDamp(squareCenter, playerPosition, ref velocity, followDelay);
            //squareCenter = playerPosition;
        }
    }

    void FollowPlayer()
    {
        if (_player == null || cameraTransform == null) return;

        float cameraAngleY = cameraTransform.eulerAngles.y * Mathf.Deg2Rad;
        Vector3 rotatedOffset = new Vector3(
            offset.x * Mathf.Cos(cameraAngleY) - offset.z * Mathf.Sin(cameraAngleY),
            offset.y,
            offset.x * Mathf.Sin(cameraAngleY) + offset.z * Mathf.Cos(cameraAngleY)
        );

        float yOffsetFactor = 0.25f;
        Vector3 cameraOffset = rotatedOffset + new Vector3(0, -offset.y * yOffsetFactor, 0);
        Vector3 targetPosition = squareCenter + cameraOffset;

        // Ограничение позиции камеры в пределах _centerLocationObj
        Vector3 minBounds = _centerLocationObj.position - _centerLocationObj.localScale / 2;
        Vector3 maxBounds = _centerLocationObj.position + _centerLocationObj.localScale / 2;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minBounds.z, maxBounds.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followDelay);
        //transform.position = targetPosition;
        transform.LookAt(squareCenter);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float newSize = offset.magnitude - scroll * zoomSpeed;
            newSize = Mathf.Clamp(newSize, minZoom, maxZoom);
            offset = offset.normalized * newSize;
        }
    }

    void OnDrawGizmos()
    {
        if (squareCenter == null) return;

        Gizmos.color = Color.blue;
        Vector3 halfScale = _centerLocationObj.localScale / 2;
        Vector3 minBounds = _centerLocationObj.position - halfScale;
        Vector3 maxBounds = _centerLocationObj.position + halfScale;

        Gizmos.DrawLine(new Vector3(minBounds.x, _centerLocationObj.position.y, minBounds.z), new Vector3(maxBounds.x, _centerLocationObj.position.y, minBounds.z));
        Gizmos.DrawLine(new Vector3(maxBounds.x, _centerLocationObj.position.y, minBounds.z), new Vector3(maxBounds.x, _centerLocationObj.position.y, maxBounds.z));
        Gizmos.DrawLine(new Vector3(maxBounds.x, _centerLocationObj.position.y, maxBounds.z), new Vector3(minBounds.x, _centerLocationObj.position.y, maxBounds.z));
        Gizmos.DrawLine(new Vector3(minBounds.x, _centerLocationObj.position.y, maxBounds.z), new Vector3(minBounds.x, _centerLocationObj.position.y, minBounds.z));
        
        if (_centerLocationObj == null) return;

        Gizmos.color = Color.green;
        Vector3 boundarySize3d = new Vector3(boundarySize.x, 0, boundarySize.y) / 2;
        //Vector3 halfScale = _centerLocationObj.localScale / 2;
        minBounds = squareCenter - boundarySize3d;
        minBounds = squareCenter + boundarySize3d;

        Gizmos.DrawLine(new Vector3(minBounds.x, squareCenter.y, minBounds.z), new Vector3(maxBounds.x, squareCenter.y, minBounds.z));
        Gizmos.DrawLine(new Vector3(maxBounds.x, squareCenter.y, minBounds.z), new Vector3(maxBounds.x, squareCenter.y, maxBounds.z));
        Gizmos.DrawLine(new Vector3(maxBounds.x, squareCenter.y, maxBounds.z), new Vector3(minBounds.x, squareCenter.y, maxBounds.z));
        Gizmos.DrawLine(new Vector3(minBounds.x, squareCenter.y, maxBounds.z), new Vector3(minBounds.x, squareCenter.y, minBounds.z));
    }
}
