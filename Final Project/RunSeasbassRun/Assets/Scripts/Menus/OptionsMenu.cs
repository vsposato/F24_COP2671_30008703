using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menus
{
    public class OptionsMenu : MonoBehaviour
    {
        private const string DifficultyLevelKey = "DifficultyLevel";
        private const string SfxMixerValueKey = "SfxVolume";
        private const string MusicMixerValueKey = "MusicVolume";
        private const string MasterMixerValueKey = "MasterVolume";

        [Header("Video Option Settings")]
        [Tooltip("Fullscreen Toggle UI Element")]
        [SerializeField]
        private Toggle fullscreenToggle;

        [Tooltip("vSync Toggle UI Element")]
        [SerializeField]
        private Toggle vsyncToggle;

        [Tooltip("Resolution Label UI Element")]
        [SerializeField]
        private TextMeshProUGUI resolutionLabel;

        [Header("Audio Option Settings")]
        [Tooltip("Master Volume Slider UI Element")]
        [SerializeField]
        private Slider masterVolumeSlider;

        [Tooltip("Master Volume Label UI Element")]
        [SerializeField]
        private TextMeshProUGUI masterVolumeLabel;

        [Tooltip("Music Volume Slider UI Element")]
        [SerializeField]
        private Slider musicVolumeSlider;

        [Tooltip("Music Volume Label UI Element")]
        [SerializeField]
        private TextMeshProUGUI musicVolumeLabel;

        [Tooltip("SFX Volume Slider UI Element")]
        [SerializeField]
        private Slider sfxVolumeSlider;

        [Tooltip("SFX Volume Label UI Element")]
        [SerializeField]
        private TextMeshProUGUI sfxVolumeLabel;

        [Tooltip("Audio Mixer Element")]
        [SerializeField]
        private AudioMixer theMixer;

        [Header("Gameplay Option Settings")]
        [Tooltip("Difficulty Level Dropdown UI Element")]
        [SerializeField]
        private TMP_Dropdown difficultyDropdown;

        private int _selectedResolution;

        private readonly ResolutionItem[] _resolutions =
        {
            new ResolutionItem(1920, 1080),
            new ResolutionItem(1366, 768),
            new ResolutionItem(1280, 720),
            new ResolutionItem(2560, 1440),
        };

        /// <summary>
        /// Start is called before the first frame update.
        /// Initializes the fullscreen and vsync toggles, and sets the base resolution.
        /// </summary>
        private void Start()
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            vsyncToggle.isOn = QualitySettings.vSyncCount != 0;

            SetBaseResolution();
            SetAudioSettings();
            GetDifficultyLevel();
        }

        private void SetAudioSettings()
        {
            GetAndRestoreMixerValue(MasterMixerValueKey);
            GetAndRestoreMixerValue(MusicMixerValueKey);
            GetAndRestoreMixerValue(SfxMixerValueKey);

            masterVolumeLabel.text = $"{masterVolumeSlider.value + 80}";
            musicVolumeLabel.text = $"{musicVolumeSlider.value + 80}";
            sfxVolumeLabel.text = $"{sfxVolumeSlider.value + 80}";
        }

        /// <summary>
        /// Sets the base resolution based on the current screen resolution.
        /// If the current resolution is not in the list of available resolutions, it updates the label with the current resolution.
        /// </summary>
        /// <summary>
        /// Sets the base resolution based on the current screen resolution.
        /// If the current resolution is not in the list of available resolutions, it updates the label with the current resolution.
        /// </summary>
        private void SetBaseResolution()
        {
            var foundRes = false;
            for (var i = 0; i < _resolutions.Length; i++)
            {
                // Check if the current screen resolution matches the available resolution
                if (Screen.width != _resolutions[i].Width || Screen.height != _resolutions[i].Height)
                {
                    continue;
                }

                foundRes = true;
                _selectedResolution = i;
                UpdateResolutionLabel();
            }

            // If the current resolution is not found in the available resolutions, update the label with the current resolution
            if (!foundRes)
            {
                UpdateResolutionLabel(Screen.width, Screen.height);
            }
        }

        /// <summary>
        /// Decrements the selected resolution index.
        /// If the index is less than 0, it sets it to 0.
        /// </summary>
        public void ResolutionSelectionDecrement()
        {
            // Decrement the selected resolution index
            _selectedResolution--;

            // If the index is less than 0, set it to 0
            if (_selectedResolution < 0)
            {
                _selectedResolution = 0;
            }

            // Update the resolution label with the new selected resolution
            UpdateResolutionLabel();
        }

        /// <summary>
        /// Increments the selected resolution index.
        /// If the index is greater than or equal to the length of the resolutions array, it sets it to the last index.
        /// </summary>
        public void ResolutionSelectionIncrement()
        {
            _selectedResolution++;
            if (_selectedResolution > _resolutions.Length - 1)
            {
                _selectedResolution = (_resolutions.Length - 1);
            }

            UpdateResolutionLabel();
        }

        /// <summary>
        /// Applies the selected graphics changes.
        /// Sets the vsync count based on the vsync toggle state, and sets the screen resolution based on the selected resolution and fullscreen toggle state.
        /// </summary>
        public void ApplyGraphicsChanges()
        {
            QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
            Screen.SetResolution(_resolutions[_selectedResolution].Width,
                _resolutions[_selectedResolution].Height, fullscreenToggle.isOn);
        }

        /// <summary>
        /// Updates the resolution label with the selected resolution.
        /// </summary>
        private void UpdateResolutionLabel()
        {
            UpdateResolutionLabel(_resolutions[_selectedResolution].Width,
                _resolutions[_selectedResolution].Height);
        }

        /// <summary>
        /// Updates the resolution label with the given width and height.
        /// </summary>
        /// <param name="width">The width of the resolution.</param>
        /// <param name="height">The height of the resolution.</param>
        private void UpdateResolutionLabel(int width, int height)
        {
            resolutionLabel.text = $"{width} x {height}";
        }

        /// <summary>
        /// Sets the master volume level and updates the corresponding label.
        /// It also saves the master volume level to PlayerPrefs for future use.
        /// </summary>
        /// <remarks>
        /// This function is called when the master volume slider value is changed.
        /// It updates the master volume label with the current slider value plus 80.
        /// It then calls the SetAndSaveMixerValue method to set the master volume in the AudioMixer and save the value in PlayerPrefs.
        /// </remarks>
        public void SetMasterVolume()
        {
            // Update the master volume label with the current slider value plus 80
            masterVolumeLabel.text = $"{masterVolumeSlider.value + 80}";

            // Set the master volume in the AudioMixer and save the value in PlayerPrefs
            SetAndSaveMixerValue(MasterMixerValueKey, masterVolumeSlider.value);
        }

        /// <summary>
        /// Sets the music volume level and updates the corresponding label.
        /// It also saves the music volume level to PlayerPrefs for future use.
        /// </summary>
        /// <remarks>
        /// This function is called when the music volume slider value is changed.
        /// It updates the music volume label with the current slider value plus 80.
        /// It then calls the SetAndSaveMixerValue method to set the music volume in the AudioMixer and save the value in PlayerPrefs.
        /// </remarks>
        public void SetMusicVolume()
        {
            // Update the music volume label with the current slider value plus 80
            musicVolumeLabel.text = $"{musicVolumeSlider.value + 80}";

            // Set the music volume in the AudioMixer and save the value in PlayerPrefs
            SetAndSaveMixerValue(MusicMixerValueKey, musicVolumeSlider.value);
        }

        /// <summary>
        /// Sets the SFX (Sound Effects) volume level and updates the corresponding label.
        /// It also saves the SFX volume level to PlayerPrefs for future use.
        /// </summary>
        /// <remarks>
        /// This function is called when the SFX volume slider value is changed.
        /// It updates the SFX volume label with the current slider value plus 80.
        /// It then calls the SetAndSaveMixerValue method to set the SFX volume in the AudioMixer and save the value in PlayerPrefs.
        /// </remarks>
        public void SetSfxVolume()
        {
            // Update the SFX volume label with the current slider value plus 80
            sfxVolumeLabel.text = $"{sfxVolumeSlider.value + 80}";

            // Set the SFX volume in the AudioMixer and save the value in PlayerPrefs
            SetAndSaveMixerValue(SfxMixerValueKey, sfxVolumeSlider.value);
        }

        /// <summary>
        /// Sets the specified mixer value and saves it to PlayerPrefs.
        /// </summary>
        /// <param name="mixerValueName">The name of the mixer value to be set.</param>
        /// <param name="mixerValue">The value to be set for the specified mixer.</param>
        /// <remarks>
        /// This function is used to set the specified mixer value in the AudioMixer and save it to PlayerPrefs for future use.
        /// It takes two parameters: the name of the mixer value and the value to be set.
        /// The function uses the AudioMixer.SetFloat method to set the specified mixer value, and then saves the value to PlayerPrefs using PlayerPrefs.SetFloat.
        /// </remarks>
        private void SetAndSaveMixerValue(string mixerValueName, float mixerValue)
        {
            theMixer.SetFloat(mixerValueName, mixerValue);
            PlayerPrefs.SetFloat(mixerValueName, mixerValue);
        }

        private void GetAndRestoreMixerValue(string mixerValueName)
        {
            if (!PlayerPrefs.HasKey(mixerValueName))
            {
                return;
            }

            theMixer.SetFloat(mixerValueName, PlayerPrefs.GetFloat(mixerValueName));
            switch (mixerValueName)
            {
                case MasterMixerValueKey:
                    masterVolumeSlider.value = PlayerPrefs.GetFloat(mixerValueName);
                    break;
                case MusicMixerValueKey:
                    musicVolumeSlider.value = PlayerPrefs.GetFloat(mixerValueName);
                    break;
                case SfxMixerValueKey:
                    sfxVolumeSlider.value = PlayerPrefs.GetFloat(mixerValueName);
                    break;
            }
        }

        public void SetDifficultyLevel()
        {
            var difficultyLevel = difficultyDropdown.value + 1;
            PlayerPrefs.SetInt(DifficultyLevelKey, difficultyLevel);
        }

        private void GetDifficultyLevel()
        {
            if (!PlayerPrefs.HasKey(DifficultyLevelKey))
            {
                difficultyDropdown.value = 0;
                PlayerPrefs.SetInt(DifficultyLevelKey, 1);
                return;
            }

            difficultyDropdown.value = PlayerPrefs.GetInt(DifficultyLevelKey) - 1;
        }
    }
}