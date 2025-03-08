using System;
using System.Collections;

using UnityEngine;

namespace RDong
{
    [DisallowMultipleComponent]
    public class ScoreManager : MonoBehaviour
    {
        public Action<int> ScoreChanged;

        static ScoreManager _instance;
        public static ScoreManager Instance => _instance;

        public int Score => _score;

        int _score;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        bool _isCounting = false;

        public void AddToScore(int toAdd) 
        { 
            _score += toAdd; 
            ScoreChanged?.Invoke(_score);
        }

        public void StartCountScore()
        {
            if (!_isCounting)
            {
                StartCoroutine(ScoreCountingRoutine());
                _isCounting = true;
            }
        }

        public void FinishCountScore()
        {
            StopAllCoroutines();
            _isCounting = false;
        }

        IEnumerator ScoreCountingRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                _score += 1;
                ScoreChanged?.Invoke(_score);
            }
        }
    }
}