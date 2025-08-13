using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class ScoreUI : MonoBehaviour
{
    
    [SerializeField] private TMP_Text scoreText;
    private Score _score;

    [Inject]
    private void Construct(Score score)
    {
        _score = score;
    }
    private void Start()
    {
        _score.CurrentScoreReactive
            .Subscribe(value => scoreText.text = value.ToString())
            .AddTo(this);
    }
}

