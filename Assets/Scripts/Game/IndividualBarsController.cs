using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    public class IndividualBarsController: MonoBehaviour
    {
        private static Slider healthSlider;
        private static Slider socialSlider;
        private static Slider moodSlider;
        private static Image healthFill;
        private static Image socialFill;
        private static Image moodFill;

        
        void Start()
        {
            healthSlider = GameObject.Find("IndividualBarsHUD/IndividualBars/HealthBar").GetComponent<Slider>();
            socialSlider = GameObject.Find("IndividualBarsHUD/IndividualBars/SocialBar").GetComponent<Slider>();
            moodSlider = GameObject.Find("IndividualBarsHUD/IndividualBars/MoodBar").GetComponent<Slider>();
            healthFill = GameObject.Find("IndividualBarsHUD/IndividualBars/HealthBar/Fill").GetComponent<Image>();
            socialFill = GameObject.Find("IndividualBarsHUD/IndividualBars/SocialBar/SocialFill").GetComponent<Image>();
            moodFill = GameObject.Find("IndividualBarsHUD/IndividualBars/MoodBar/MoodFill").GetComponent<Image>();
            SetMaxValues2(100, 100, 100);
        }

        /// <summary>
        /// Se fijan todos los valores al máximo al comienzo de la partida
        /// </summary>
        public static void SetMaxValues(int health, int social, int mood)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;

            socialSlider.maxValue = social;
            socialSlider.value = social;

            moodSlider.maxValue = mood;
            moodSlider.value = mood;

            healthFill.color = Color.green;
            socialFill.color = Color.green;
            moodFill.color = Color.green;
        }

        /// <summary>
        /// Se fijan todos los valores al máximo al comienzo de la partida
        /// </summary>
        public static void SetMaxValues2(int health, int social, int mood)
        {
            healthSlider.maxValue = health;

            socialSlider.maxValue = social;

            moodSlider.maxValue = mood;

            healthFill.color = Color.green;
            socialFill.color = Color.green;
            moodFill.color = Color.green;
        }

        public static void ShowValues(int health, int social, int mood)
        {
            SetHealth(health);
            SetSocial(social);
            SetMood(mood);
        }

        #region Health

        /// <summary>
        /// Cambia el valor de la barra de salud y su color según su valor
        /// </summary>
        /// <param name="health"></param>
        public static void SetHealth(int health)
        {
            healthSlider.value = health;
            if (health > 66)
            {
                healthFill.color = Color.green;
            }
            else if (health >= 33)
            {
                healthFill.color = Color.yellow;
            }
            else
            {
                healthFill.color = Color.red;
            }
        }

        /*
        /// <summary>
        /// Obtiene los puntos de salud
        /// </summary>
        /// <returns></returns>
        public static int GetHealth()
        {
            return healthPoints;
        }

        /// <summary>
        /// Restaura la salud general de los personajes en un porcentaje que depende de la aptitud del médico
        /// </summary>
        /// <param name="aptitude"></param>
        public static void Heal(double aptitude)
        {
            healthPoints += Convert.ToInt32(Constants.maxHealth * aptitude * 2);
            if (healthPoints > Constants.maxHealth)
            {
                healthPoints = Constants.maxHealth;
            }
        }

        /// <summary>
        /// Daña la salud general de los personajes en un porcentaje que depende de la aptitud del médico
        /// </summary>
        /// <param name="aptitude"></param>
        public static void DamageHealth(double aptitude)
        {
            double inverseAptitude = 0.6 - aptitude;
            int damage = Convert.ToInt32(Constants.maxHealth * inverseAptitude * 2);
            DecreaseHealth(damage);
        }

        /// <summary>
        /// Dañar salud en los puntos especificados
        /// </summary>
        /// <param name="decreasePoints"></param>
        public static void DecreaseHealth(int decreasePoints)
        {
            healthPoints -= decreasePoints;
        }

        /// <summary>
        /// Coger bayas venenosas (desastre al coger comida). La salud empeora
        /// </summary>
        public static void TakeToxicFood()
        {
            DecreaseHealth(Constants.healthDecreaseInFoodMesh);
        }

        /// <summary>
        /// Daño a la salud por insolación
        /// </summary>
        public static void HaveSunstroke()
        {
            DecreaseHealth(Constants.sunHealthDamagePerHour);
        }
        */
        #endregion

        #region Social

        /// <summary>
        /// Cambia el valor de la barra de social y su color según su valor
        /// </summary>
        /// <param name="social"></param>
        public static void SetSocial(int social)
        {
            socialSlider.value = social;
            if (social > 66)
            {
                socialFill.color = Color.green;
            }
            else if (social >= 33)
            {
                socialFill.color = Color.yellow;
            }
            else
            {
                socialFill.color = Color.red;
            }
        }

        #endregion

        #region Mood

        /// <summary>
        /// Cambia el valor de la barra de humor y su color según su valor
        /// </summary>
        /// <param name="mood"></param>
        public static void SetMood(int mood)
        {
            moodSlider.value = mood;
            if (mood > 66)
            {
                moodFill.color = Color.green;
            }
            else if (mood >= 33)
            {
                moodFill.color = Color.yellow;
            }
            else
            {
                moodFill.color = Color.red;
            }
        }

        #endregion
    }
}
