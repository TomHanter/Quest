using System.Collections;
using UnityEngine;

public class PokemonNPC : MonoBehaviour
{
    [SerializeField] private GameObject _miniGame;
    [SerializeField] private GameObject _castle;
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private CameraSwitch _switch;
    [SerializeField] private GameProcess _miniGameProcessor;
    [SerializeField] private GameObject _menu;

    private GameObject _winPanel;
    private GameObject _losePanel;

    private bool _isPanelActive = false;

    private bool _isMiniGameActive = false; // Отвечает за старт мини-игры
    private bool _isMiniGameFinished = false; // Отвечает за конец мини-игры
    private bool _isWaitingForMiniGame = false; // Флаг ожидания перед стартом мини-игры

    private void Start()
    {
        _switch = GetComponentInChildren<CameraSwitch>();
        _miniGameProcessor.OnEndGame += EndMiniGame;
    }

    private void Update()
    {
        if (_isMiniGameActive || _isMiniGameFinished || _isWaitingForMiniGame)
            return; // Не запускаем мини-игру повторно

        string pokemonName = ((Ink.Runtime.StringValue)DialogueManager
            .GetInstance()
            .GetVariablesState("pokemon_name")).value;

        if (pokemonName == "Charmander")
        {
            StartMiniGame();
        }
        if (_menu != null)
        {
            _winPanel = _menu.transform.Find("WinPanel")?.gameObject;
            _losePanel = _menu.transform.Find("LosePanel")?.gameObject;

            if (_winPanel == null || _losePanel == null)
            {
                Debug.LogWarning("Панели WinPanel или LosePanel не найдены в объекте _menu.");
            }
            else
            {
                // Деактивируем панели при старте
                _winPanel.SetActive(false);
                _losePanel.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Объект _menu не назначен.");
        }
    }

    private void ActivatePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf); 
        }
    }


    private void StartMiniGame()
    {
        _switch.SwitchCamera();
        _miniGame.SetActive(true);
        _castle.SetActive(false);
        _isMiniGameActive = true;
    }

    public void EndMiniGame(string winnerName, string loserName)
    {
        if (_isMiniGameFinished) return; // Не вызываем повторно

        // Запускаем корутину для обработки конца игры
        StartCoroutine(EndMiniGameCoroutine(winnerName, loserName));
    }

    private IEnumerator EndMiniGameCoroutine(string winnerName, string loserName)
    {
        _miniGame.SetActive(false);

        GameObject currentPannel = null;
        // Активируем соответствующую панель
        if (winnerName == "Player")
        {
            currentPannel = _winPanel;
        }
        else
        {
            currentPannel = _losePanel;
        }
        
        ActivatePanel(currentPannel);
        yield return new WaitForSeconds(2f);
        ActivatePanel(currentPannel);


        // Выполняем оставшуюся логику конца игры
        _switch.SwitchCamera();
        _castle.SetActive(true);
        _isMiniGameFinished = true;

        Debug.LogWarning("Мини-игра завершена. Победитель: " + winnerName);
    }

}
