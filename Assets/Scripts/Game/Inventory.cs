// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Ainhoa Longo Pérez
// Last Modified On : 06-19-2020
// ***********************************************************************
// <copyright file="BaseDoubleAction.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Assets.Scripts.Actions;
using Assets.Scripts.Internationalization;
using Assets.Scripts.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Action = Assets.Scripts.Actions.Action;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Maneja el inventario de los personajes, así como su salud y porcentajes de construcción
    /// . Implementa a <see cref="UnityEngine.MonoBehaviour" />
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Inventory : MonoBehaviour
    {
        /// <summary>
        /// Unidades de agua
        /// </summary>
        public static int waterUnits;
        /// <summary>
        /// Unidades de comida
        /// </summary>
        public static int foodUnits;
        /// <summary>
        /// Unidades de materiales
        /// </summary>
        public static int resourcesUnits;
        /// <summary>
        /// Porcentaje de construcción del refugio
        /// </summary>
        public static int refugeBuildPercentage;
        /// <summary>
        /// Porcentaje de construcción de la barca
        /// </summary>
        public static int boatBuildPercentage;
        /// <summary>
        /// Puntos de salud
        /// </summary>
        public static int healthPoints;
        /// <summary>
        /// Determina si el refugio está construido
        /// </summary>
        public static bool refugeFinished;
        /// <summary>
        /// Determina si la barca está construida
        /// </summary>
        public static bool boatFinished;

        /// <summary>
        /// Slider de la barra de salud
        /// </summary>
        private static Slider slider;
        /// <summary>
        /// Relleno de la barra de salud
        /// </summary>
        public static Image fill;

        /// <summary>
        /// Todos los personajes en el juego
        /// </summary>
        private GameObject characters;
        /// <summary>
        /// Dice si el botón de construir bote se ha activado alguna vez
        /// </summary>
        private static bool buildBoatButtonActive;
        /// <summary>
        /// Número de personajes activos
        /// </summary>
        public static int numberOfActiveCharacters;
        
        /// <summary>
        /// Al comienzo de la partida, se pone la salud al máximo y lo demás al mínimo
        /// </summary>
        void Start()
        {
            characters = GameObject.Find("Characters");
            slider = GameObject.Find("HealthBar").GetComponent<Slider>();
            fill = GameObject.Find("Fill").GetComponent<Image>();

            numberOfActiveCharacters = 0;
            foreach (Transform child in characters.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    numberOfActiveCharacters++;
                }
            }

            waterUnits = 0;
            foodUnits = 0;
            resourcesUnits = 0;
            refugeBuildPercentage = 0;
            healthPoints = Constants.maxHealth;
            SetMaxHealth(Constants.maxHealth);
            refugeFinished = false;
            buildBoatButtonActive = false;
        }

        /// <summary>
        /// Derramar el agua (desastre al coger agua). La cantidad dependerá del número de personajes
        /// </summary>
        public static void PourWater()
        {
            waterUnits -= numberOfActiveCharacters * Constants.pourWaterQuantity;
            if (waterUnits < 0)
                waterUnits = 0;
        }

        /// <summary>
        /// Coger bayas venenosas (desastre al coger comida). La salud empeora
        /// </summary>
        public static void TakeToxicFood()
        {
            DecreaseHealth(Constants.healthDecreaseInFoodMesh);
        }

        /// <summary>
        /// Derrumbar parte del refugio (desastre al construir refugio)
        /// </summary>
        public static void TearDownRefuge()
        {
            DamageBuilding(Constants.buildingDamageInMesh);
        }

        /// <summary>
        /// Derrumbar parte del bote (desastre al construir bote)
        /// </summary>
        public static void TearDownBoat()
        {
            DamageBoat(Constants.boatDamageInMesh);
        }

        /// <summary>
        /// En las acciones simples, incrementa la cantidad del recurso relacionado (o el porcentaje de construcción) en la cantidad pasada como parámetro
        /// </summary>
        /// <param name="action"></param>
        /// <param name="quantity"></param>
        public static void Increment(Action action, int quantity)
        {
            if (action.GetType() == typeof(PickWaterAction))
            {
                waterUnits += quantity;
            }
            else if (action.GetType() == typeof(PickFoodAction))
            {
                foodUnits += quantity;
            }
            else if (action.GetType() == typeof(PickResourcesAction))
            {
                resourcesUnits += quantity;
                if (resourcesUnits > 0)
                {
                    //Cuando hay recursos disponibles, se activan los botones de construir refugio y bote, que en un principio están desactivados
                    Button refugeButton = GameObject.Find("BuildRefugeAction").GetComponentInChildren<Button>();
                    refugeButton.interactable = true;
                    if (buildBoatButtonActive)
                    {
                        Button boatButton = GameObject.Find("BuildBoatAction").GetComponentInChildren<Button>();
                        boatButton.interactable = true;
                    }
                }
            }
            else if (action.GetType() == typeof(BuildRefugeAction))
            {
                refugeBuildPercentage += quantity;
                if (refugeBuildPercentage >= 100)
                {
                    refugeFinished = true;
                }
            }
            else if (action.GetType() == typeof(BuildBoatAction))
            {
                boatBuildPercentage += quantity;
                if (boatBuildPercentage >= 100)
                {
                    boatFinished = true;                    
                }
            }
        }

        /// <summary>
        /// Cuando el refugio está construido y tienen comida y agua, se habilita el botón de construir bote
        /// </summary>
        public static void ActivateBuildBoatButton()
        {
            buildBoatButtonActive = true;
            Button boatButton = GameObject.Find("BuildBoatAction").GetComponentInChildren<Button>();
            boatButton.interactable = true;
            PopupManager.ShowMessage(StaticLanguageController.textMessages.CanBuildBoatText);
        }

        /// <summary>
        /// Consumir agua cada "waterConsumptionFrequency" horas. Se consumen "waterConsumptionPerCharacter" unidades por personaje activo.
        /// </summary>
        public static void ConsumeWater()
        {
            if (CourseOfTime.elapsedHours % Constants.waterConsumptionFrequency == 0 && !(CourseOfTime.elapsedHours == 0 && CourseOfTime.elapsedDays == 0))
            {
                waterUnits -= numberOfActiveCharacters * Constants.waterConsumptionPerCharacter;                
            }
        }

        /// <summary>
        /// Consumir comida cada "foodConsumptionFrequency" horas. Se consumen "foodConsumptionPerCharacter" unidades por personaje activo.
        /// </summary>
        public static void ConsumeFood()
        {
            if (CourseOfTime.elapsedHours % Constants.foodConsumptionFrequency == 0 && !(CourseOfTime.elapsedHours == 0 && CourseOfTime.elapsedDays == 0))
            {
                foodUnits -= numberOfActiveCharacters * Constants.foodConsumptionPerCharacter;
            }
        }

        /// <summary>
        /// Consumir materiales. Cuando la cantidad llega a 0, los botones de construir refugio y construir bote se desactivan.
        /// </summary>
        /// <param name="quantity"></param>
        public static void ConsumeResources(int quantity)
        {
            resourcesUnits -= quantity;
            if (resourcesUnits <= 0)
            {
                Button refugeButton = GameObject.Find("BuildRefugeAction").GetComponentInChildren<Button>();
                refugeButton.interactable = false;
                Button boatButton = GameObject.Find("BuildBoatAction").GetComponentInChildren<Button>();
                boatButton.interactable = false;
            }
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
        /// Restaura la salud general de los personajes en un porcentaje que depende de la aptitud del médico
        /// </summary>
        /// <param name="aptitude"></param>
        public static void Heal(double aptitude)
        {
            healthPoints += Convert.ToInt32(Constants.maxHealth * aptitude * 2 / numberOfActiveCharacters);
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
            int damage = Convert.ToInt32(Constants.maxHealth * inverseAptitude * 2 / numberOfActiveCharacters);
            DecreaseHealth(damage);
        }

        /// <summary>
        /// Daño periódico al refugio, que dependerá de la frecuencia establecida de daño
        /// </summary>
        /// <param name="damage"></param>
        public static void PeriodicBuildingDamage(int damage)
        {
            if (CourseOfTime.elapsedHours % Constants.buildingDamageFrequency == 0 && !(CourseOfTime.elapsedHours == 0 && CourseOfTime.elapsedDays == 0))
            {
                DamageBuilding(damage);
            }
        }

        /// <summary>
        /// Daño al refugio en los puntos especificados
        /// </summary>
        /// <param name="damage"></param>
        public static void DamageBuilding(int damage)
        {
            refugeBuildPercentage -= damage;
            if (refugeBuildPercentage < 0)
            {
                refugeBuildPercentage = 0;
                if (refugeFinished)
                {
                    GameObject finishedRefuge = GameObject.Find("FinishedRefuge");
                    finishedRefuge.transform.position = new Vector3(finishedRefuge.transform.position.x, -4, finishedRefuge.transform.position.z);
                    GameObject logs = GameObject.Find("InConstructionRefuge");
                    logs.transform.position = new Vector3(logs.transform.position.x, 0, logs.transform.position.z);
                }
            }
        }

        /// <summary>
        /// Daño al barco cuando ocurre un desastre durante la construcción
        /// </summary>
        /// <param name="damage"></param>
        public static void DamageBoat(int damage)
        {
            boatBuildPercentage -= damage;
            if (boatBuildPercentage < 0)
            {
                boatBuildPercentage = 0;
            }
        }

        /// <summary>
        /// Daño a la salud general por insolación
        /// </summary>
        public static void HaveSunstroke()
        {
            DecreaseHealth(Constants.sunHealthDamagePerHour);
        }

        /// <summary>
        /// Muestra en el panel superior los datos actualizados del inventario
        /// </summary>
        void Update()
        {
            SetHealth(healthPoints);
            TextMeshProUGUI mText = transform.GetComponent<TextMeshProUGUI>();
            if (transform.name == "WaterInventory")
            {
                if (waterUnits < 0)
                {
                    //Si no hay agua, se daña la salud
                    DecreaseHealth(Convert.ToInt32(-waterUnits/2));
                    waterUnits = 0;
                }
                if (waterUnits == 0)
                    mText.color = Color.red;
                else
                    mText.color = Color.white;
                mText.text = waterUnits.ToString();
            }
            else if (transform.name == "FoodInventory")
            {
                if (foodUnits < 0)
                {
                    //Si no hay comida, se daña la salud
                    DecreaseHealth(Convert.ToInt32(-foodUnits/2));
                    foodUnits = 0;
                }
                if (foodUnits == 0)
                    mText.color = Color.red;
                else
                    mText.color = Color.white;
                mText.text = foodUnits.ToString();
            }
            else if (transform.name == "ResourcesInventory")
            {
                if (resourcesUnits < 0)
                    resourcesUnits = 0;
                mText.text = resourcesUnits.ToString();
            }
            else if (transform.name == "RefugeBuildPercentage")
            {
                if (refugeBuildPercentage > 100)
                {
                    refugeBuildPercentage = 100;
                }
                mText.text = refugeBuildPercentage.ToString() + "%";
            }
            else if (transform.name == "BoatBuildPercentage")
            {
                if (boatBuildPercentage > 100)
                {
                    boatBuildPercentage = 100;
                }
                mText.text = boatBuildPercentage.ToString() + "%";
            }

            if (waterUnits > 0 && foodUnits > 0 && refugeFinished && !buildBoatButtonActive)
            {
                ActivateBuildBoatButton();
            }
        }

        /// <summary>
        /// Se fija la salud máxima al comienzo de la partida
        /// </summary>
        /// <param name="health"></param>
        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;

            fill.color = Color.green;
        }

        /// <summary>
        /// Cambia el color de la barra de salud según su valor
        /// </summary>
        /// <param name="health"></param>
        public static void SetHealth(int health)
        {
            slider.value = health;
            if (health > 66)
            {
                fill.color = Color.green;
            }
            else if (health >= 33)
            {
                fill.color = Color.yellow;
            }
            else
            {
                fill.color = Color.red;
            }
        }

        /// <summary>
        /// Obtiene los puntos de salud
        /// </summary>
        /// <returns></returns>
        public static int GetHealth()
        {
            return healthPoints;
        }

    }

}
