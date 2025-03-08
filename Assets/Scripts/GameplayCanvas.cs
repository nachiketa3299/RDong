using UnityEngine;

using TMPro;

namespace RDong
{
    [DisallowMultipleComponent]
    public class GameplayCanvas : MonoBehaviour
    {
        [SerializeField] TMP_Text _tmpText;

        void Start()
        {
            _tmpText.text = ScoreManager.Instance.Score.ToString();
            ScoreManager.Instance.ScoreChanged += GameplayCanvas_ScoreChanged;
        }

        void OnDestroy()
        {
            ScoreManager.Instance.ScoreChanged -= GameplayCanvas_ScoreChanged;
        }

        void GameplayCanvas_ScoreChanged(int score)
        {
            _tmpText.text = score.ToString();
        }
    }
}
