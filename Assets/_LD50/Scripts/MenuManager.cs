using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
namespace GildarGaming.LD50
{
    public class MenuManager : MonoBehaviour
    {
        public void OnBackBtnClick()
        {
            SceneManager.LoadScene(0);

        }
        public void OnPlayClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}
