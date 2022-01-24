using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class Main5 : MonoBehaviour
{
    [Header("Set in Unity")]
    [SerializeField]
    Button LoadButton;
    [SerializeField]
    Button SaveButton;

    [SerializeField]
    Text PlayerHP;

    [SerializeField]
    GameObject Apple;

    [SerializeField]
    GameObject EndGameDialog;

    [SerializeField]
    GameObject WinText;

    [SerializeField]
    GameObject LoseText;

    void Start()
    {
        StateManager.GetState()
            .PlayerHealth.Value = 20;

        LoadButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                StateManager.Instance.LoadState()
                    .SubscribeOnMainThread()
                    .Subscribe(_ =>
                    {
                        GetComponent<TopDownMove>().enabled = true;
                        EndGameDialog.SetActive(false);
                        WinText.SetActive(false);
                        LoseText.SetActive(false);

                        Debug.Log("Loaded state");
                    });
            }, ex => Debug.LogError(ex));

        SaveButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                StateManager.Instance.SaveState()
                    .Subscribe(_ =>
                    {
                        Debug.Log("Saved state");
                    });
            });

        var winCondition = Apple.AddComponent<ObservableTrigger2DTrigger>()
            .OnTriggerEnter2DAsObservable()
            .Where(other => other.gameObject.tag == "Player")
            .Select(_ => true);
        var loseCondition = StateManager.GetState().PlayerHealth
            .Where(health => health <= 0)
            .Select(_ => false);

        var endGame = Observable.Merge(winCondition, loseCondition);
        endGame.Subscribe(win =>
        {
            EndGameDialog.SetActive(true);
            WinText.SetActive(win);
            LoseText.SetActive(!win);
            GetComponent<TopDownMove>().enabled = false;
        });

        StateManager.GetState()
            .PlayerHealth
            // .Where(health => health >= 0)
            // .TakeUntil(endGame)
            .Subscribe(playerHealth => PlayerHP.text = playerHealth.ToString());
    }

}
