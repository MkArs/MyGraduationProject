using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Отвечает за звуки
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _mainTheme;
        [SerializeField]
        private AudioSource _bossTheme;
        [SerializeField]
        private AudioSource _tearShot;
        [SerializeField]
        private AudioSource _tearBreak;
        [SerializeField]
        private AudioSource _bombExplodes;
        [SerializeField]
        private AudioSource _rockCrumble;
        [SerializeField]
        private AudioSource _treasureRoomEnter;
        [SerializeField]
        private AudioSource _powerUp;
        [SerializeField]
        private AudioSource _hurt;
        [SerializeField]
        private AudioSource _death;
        [SerializeField]
        private AudioSource _monsterGrunt;
        [SerializeField]
        private AudioSource _monsterCuteGrunt;
        [SerializeField]
        private AudioSource _HPGrab;
        [SerializeField]
        private AudioSource _keyGrab;
        [SerializeField]
        private AudioSource _pennyGrab;
        [SerializeField]
        private AudioSource _nickelGrab;
        [SerializeField]
        private AudioSource _dimeGrab;
        [SerializeField]
        private AudioSource _doorOpen;
        [SerializeField]
        private AudioSource _doorClose;
        [SerializeField]
        private AudioSource _zombieGrawl;
        [SerializeField]
        private AudioSource _pennyDrop;
        [SerializeField]
        private AudioSource _nickelDrop;
        [SerializeField]
        private AudioSource _dimeDrop;
        [SerializeField]
        private AudioSource _keyDrop;

        public AudioSource ZombieGrawl { get => _zombieGrawl; set => _zombieGrawl = value; }

        // Start is called before the first frame update
        void Start()
        {
            _mainTheme.Play();
        }

        public void PlayBossTheme()
        {
            _mainTheme.Stop();
            _bossTheme.Play();
        }

        public void ContinueMainTheme()
        {
            _bossTheme.Stop();
            _mainTheme.PlayDelayed(1.5f);
        }

        public void PlayTearShot()
        {
            _tearShot.Play();
        }

        public void PlayTearBreak()
        {
            _tearBreak.Play();
        }

        public void PlayBombExplodes()
        {
            _bombExplodes.Play();
        }

        public void PlayRockCrumble()
        {
            _rockCrumble.Play();
        }

        public void PlayTreasureRoomEnter()
        {
            _treasureRoomEnter.Play();
        }

        public void PlayPowerUp()
        {
            _powerUp.Play();
        }

        public void PlayHurt()
        {
            _hurt.Play();
        }

        public void PlayDeath()
        {
            _death.Play();
        }

        public void PlayMonsterGrunt()
        {
            _monsterGrunt.Play();
        }

        public void PlayMonsterCuteGrunt()
        {
            _monsterCuteGrunt.Play();
        }

        public void PlayHPGrab()
        {
            _HPGrab.Play();
        }

        public void PlayKeyGrab()
        {
            _keyGrab.Play();
        }

        public void PlayPennyGrab()
        {
            _pennyGrab.Play();
        }

        public void PlayNickelGrab()
        {
            _nickelGrab.Play();
        }

        public void PlayDimeGrab()
        {
            _dimeGrab.Play();
        }

        public void PlayDoorOpen()
        {
            _doorOpen.Play();
        }

        public void PlayDoorClose()
        {
            _doorClose.Play();
        }

        public void PlayZombieGrawl()
        {
            _zombieGrawl.Play();
        }

        public void PlayPennyDrop()
        {
            _pennyDrop.Play();
        }

        public void PlayNickelDrop()
        {
            _nickelDrop.Play();
        }

        public void PlayDimeDrop()
        {
            _dimeDrop.Play();
        }

        public void PlayKeyDrop()
        {
            _keyDrop.Play();
        }
    }
}