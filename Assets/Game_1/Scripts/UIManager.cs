using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Link.HouseStack
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;   
        [SerializeField] GameObject[] canvas;
        [SerializeField] Animation animation;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            canvas[0].SetActive(true);
        }

        public void PlayButton()
        {
            animation.Play("HomeHide");
            LevelControl.Instance.OnPlay();
            Invoke(nameof(CloseHome), 1f);
        }

        public void HomeButton()
        {
            SceneManager.LoadScene(0);
        }

        public void ShowWin()
        {
            canvas[1].SetActive(true);
        }

        private void CloseHome()
        {
            canvas[0].SetActive(false);
        }
    }
}