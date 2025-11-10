using UnityEngine;
using System.Collections.Generic;

namespace CardMatch.Modules.Audio
{
    public class AudioService : IAudioService
    {
        private AudioConfig _config;
        private List<AudioSource> _audioSourcePool;
        private Transform _poolParent;

        public AudioService(AudioConfig config, Transform poolParent)
        {
            _config = config;
            _poolParent = poolParent;
        }

        public void Initialize()
        {
            if (_config.settings.useObjectPooling)
            {
                _audioSourcePool = new List<AudioSource>();
                for (int i = 0; i < _config.settings.poolSize; i++)
                {
                    var go = new GameObject($"AudioSource_{i}");
                    go.transform.SetParent(_poolParent);
                    var source = go.AddComponent<AudioSource>();
                    source.playOnAwake = false;
                    _audioSourcePool.Add(source);
                }
            }
        }

        public void PlaySound(SoundType type)
        {
            var soundData = _config.GetSound(type);
            if (soundData == null || soundData.clip == null) return;

            var source = GetAvailableAudioSource();
            if (source != null)
            {
                source.clip = soundData.clip;
                source.volume = soundData.volume * _config.settings.masterVolume;
                source.Play();
            }
        }

        public void SetMasterVolume(float volume)
        {
            _config.settings.masterVolume = Mathf.Clamp01(volume);
        }

        private AudioSource GetAvailableAudioSource()
        {
            if (_audioSourcePool == null) return null;

            foreach (var source in _audioSourcePool)
            {
                if (!source.isPlaying) return source;
            }

            return _audioSourcePool[0];
        }

        public void Cleanup()
        {
            if (_audioSourcePool != null)
            {
                foreach (var source in _audioSourcePool)
                {
                    if (source != null && source.gameObject != null)
                        Object.Destroy(source.gameObject);
                }
                _audioSourcePool.Clear();
            }
        }
    }
}