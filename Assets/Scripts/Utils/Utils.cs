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

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    /// <summary>
    /// Enumerado de los géneros que puede tener un personaje
    /// </summary>
    public enum Gender
    {
        Male,
        Female
    }

    /// <summary>
    /// Clase de utilidades.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Lista de nombres masculinos
        /// </summary>
        public static List<string> MaleNames { get; set; }
        /// <summary>
        /// Lista de nombres femeninos
        /// </summary>
        public static List<string> FemaleNames { get; set; }
        /// <summary>
        /// Objeto que representa el centro de la isla, a donde los personajes deben dirigirse
        /// </summary>
        public static GameObject Center { get => GameObject.Find("Center"); set { } }

        /// <summary>
        /// Obtiene todas las posiciones céntricas a las que los personajes pueden dirigirse al terminar una acción
        /// </summary>
        /// <returns></returns>
        public static List<Vector3> GetCenteredPositions()
        {
            List<Vector3> positions = new List<Vector3>();
            foreach (Transform child in Center.transform)
            {
                positions.Add(child.position);
            }
            return positions;
        }

        /// <summary>
        /// Obtiene una posición céntrica aleatoria
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetRandomCenteredPosition()
        {
            System.Random random = new System.Random();
            List<Vector3> positions = GetCenteredPositions();
            return positions.ElementAt(random.Next(positions.Count - 1));
        }

        /// <summary>
        /// Detecta si dos posiciones están cerca. Llamado en el Update() del personaje para saber cuándo está cerca del centro.
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public static bool PositionsAreNear(Vector3 position1, Vector3 position2)
        {
            if (Math.Abs(position1.x - position2.x) <= 0.2 && Math.Abs(position1.z - position2.z) <= 1)
                return true;
            return false;
        }

        /// <summary>
        /// Lee el fichero "MaleNames.txt" o "FemaleNames.txt" según corresponda, y le asigna a cada personaje un nombre aleatorio
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static string GetRandomName(Gender gender)
        {
            int index;
            string name;
            if (MaleNames == null && FemaleNames == null)
            {
                string current_path = Environment.CurrentDirectory;
                string[] maleNames = System.IO.File.ReadAllLines(current_path + "/Assets/Scripts/Utils/MaleNamesInternational.txt");
                MaleNames = new List<string>(maleNames);
                string[] femaleNames = System.IO.File.ReadAllLines(current_path + "/Assets/Scripts/Utils/FemaleNamesInternational.txt");
                FemaleNames = new List<string>(femaleNames);
            }
            if (gender == Gender.Male)
            {
                index = StaticRandom.Instance.Next(0, MaleNames.Count);
                name = MaleNames.ElementAt(index);
                MaleNames.RemoveAt(index);
            }
            else
            {
                index = StaticRandom.Instance.Next(0, FemaleNames.Count);
                name = FemaleNames.ElementAt(index);
                FemaleNames.RemoveAt(index);
            }
            return name;
        }
    }
}
