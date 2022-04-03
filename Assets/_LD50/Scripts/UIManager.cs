using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

namespace GildarGaming.LD50
{
    public class UIManager : MonoBehaviour
    {
        public UIManager Instance { get; private set; }
        public TMP_Text waterText;
        public TMP_Text moneyText;
        public TMP_Text scoreText;
        public Button waterBuyButton;
        public Button waterBombingButton;


        private void Start()
        {
            Instance = this;
            GameManager.Instance.MoneyChanged += OnMoneyChanged;
            GameManager.Instance.WaterStorageChanged += OnWaterChanged;
            GameManager.Instance.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            scoreText.text = GameManager.Instance.Score.ToString();
        }

        private void OnWaterChanged()
        {
            waterText.text = ((int)GameManager.Instance.WaterStorage).ToString();
        }

        private void OnMoneyChanged()
        {
            moneyText.text = ((int)GameManager.Instance.Money).ToString();
            if (GameManager.Instance.Money < 500)
            {
                waterBombingButton.gameObject.SetActive(false);
            } else
            {
                waterBombingButton.gameObject.SetActive(true);
            }
            if (GameManager.Instance.Money < 100)
            {
                waterBuyButton.gameObject.SetActive(false);

            } else
            {
                waterBuyButton.gameObject.SetActive(true);
            }
        }

        public void OnWaterBombingClicked()
        {
            GameManager.Instance.WaterBomb();

        }
        public void OnBuyWaterCkick()
        {
            GameManager.Instance.WaterStorage += 100;
            GameManager.Instance.Money -= 500;
            
        }
    }
}
