 using UnityEngine;

namespace XrMotionDataExplorer.Playback
{
    public class PlaybackController : MonoBehaviour
    {
        private double _playbackTime;
        public float playbackSpeed;
        public double CurrentTime { get { return _playbackTime; } }

        private bool _isPlaying;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playbackTime = 0.0;
            _isPlaying = true;
        }
        [ContextMenu("Restart Playback")]
        public void RestartPlayback()
        {
            _playbackTime = 0.0;
        }
        [ContextMenu("Pause Playback")]
        public void PausePlayback()
        {
            if (!_isPlaying) return;
            _isPlaying = false;
        }
        [ContextMenu("Resume Playback")]
        public void ResumePlayback()
        {
            if (_isPlaying) return;
            _isPlaying = true;
        }

        void Update()
        {
            if(_isPlaying) _playbackTime += Time.deltaTime *  playbackSpeed;
        }
    }
}
