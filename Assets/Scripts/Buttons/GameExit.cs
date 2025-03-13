using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
        // ���� � ���������, ������� ���������
    #if UNITY_EDITOR
            Debug.Log("���� ��������� (� ���������).");
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            // ���� � �����, ������� ����������
            Application.Quit();
    #endif
    }
}
