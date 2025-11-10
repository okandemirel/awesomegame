using Modules.ScoringModule.Scripts.Core.Data.UnityObjects;
using UnityEngine;

namespace Modules.ScoringModule.Scripts
{
    public class ScoreCalculator : IScoreCalculator
    {
        private ScoringConfig _config;
        private int _currentCombo;
        private float _lastMatchTime;

        public int CurrentCombo => _currentCombo;

        public ScoreCalculator(ScoringConfig config)
        {
            _config = config;
        }

        public void Initialize()
        {
            _currentCombo = 0;
            _lastMatchTime = 0;
        }

        public int CalculateScore(ScoringContext context)
        {
            int score = 0;

            if (context.IsMatch)
            {
                score = _config.rules.matchBasePoints;

                if (_config.rules.enableCombo && _currentCombo > 0)
                {
                    int comboBonus = _currentCombo * _config.rules.comboMultiplierIncrement * _config.rules.matchBasePoints;
                    score += comboBonus;
                }

                if (context.TimeTaken < 1f)
                {
                    score += _config.rules.perfectMatchBonus;
                }
            }
            else
            {
                score = _config.rules.mismatchPenalty;
            }

            if (!_config.rules.allowNegativeScore)
            {
                score = Mathf.Max(0, score);
            }

            return score;
        }

        public void AddCombo()
        {
            if (!_config.rules.enableCombo) return;

            float currentTime = Time.time;
            if (currentTime - _lastMatchTime > _config.rules.comboResetTime)
            {
                _currentCombo = 0;
            }

            _currentCombo++;
            _lastMatchTime = currentTime;
        }

        public void ResetCombo()
        {
            _currentCombo = 0;
        }

    }
}