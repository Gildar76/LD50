using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GildarGaming.LD50
{
    
    public class MenuManager : MonoBehaviour
    {
        string masterVolName = "MasterVolume";
        public AudioMixer mixer;
        bool first = true;
        private void Start()
        {
            Slider slider = GetComponentInChildren<Slider>();
            if (slider != null)
            {
                slider.value = -11;
            }
        }
        public void OnBackBtnClick()
        {
            SceneManager.LoadScene(0);

        }

        public void OnQuitClick()
        {
            Application.Quit();
        }
        public void OnPlayClick()
        {
            SceneManager.LoadScene(1);
        }

        public void OnVolumeSliderChange(float v)
        {
            Debug.Log(v);
            mixer.SetFloat(masterVolName, v);
            AudioSource source = GetComponent<AudioSource>();
            if (source != null && !first)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(source.clip);
                }
                
            }
            first = false;
        }
    }
}
