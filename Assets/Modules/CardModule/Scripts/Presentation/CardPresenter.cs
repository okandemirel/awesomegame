using CardMatch.Modules.Audio;
using CardMatch.Modules.Haptics;

namespace CardMatch.Modules.Card
{
    public class CardPresenter
    {
        public readonly CardModel Model;
        public readonly ICardView View;

        private readonly IAudioService _audioService;
        private readonly IHapticsEngine _hapticsEngine;

        public CardPresenter(CardModel model, ICardView view, IAudioService audioService, IHapticsEngine hapticsEngine)
        {
            Model = model;
            View = view;
            _audioService = audioService;
            _hapticsEngine = hapticsEngine;
        }

        public void Flip(bool showFront)
        {
            Model.IsFlipped = showFront;
            View.Flip(showFront, 0.3f);

            if (showFront)
            {
                _audioService?.PlaySound(SoundType.CardFlip);
                _hapticsEngine?.TriggerLight();
            }
        }

        public void SetMatched()
        {
            Model.IsMatched = true;
            View.SetMatched();
            View.PlayMatchEffect();

            _audioService?.PlaySound(SoundType.Match);
            _hapticsEngine?.TriggerMedium();
        }

        public void SetMismatch()
        {
            _audioService?.PlaySound(SoundType.Mismatch);
            _hapticsEngine?.TriggerLight();
        }

        public bool CanFlip() => !Model.IsMatched;
    }
}