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
namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase que representa la acción secundaria de la clase "Heal"
    /// . Implementa a <see cref="Assets.Scripts.Actions.BasePasiveAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BasePasiveAction" />
    public class BeHealedAction : BasePasiveAction
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => "DoctorPatientPosition"; set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "beHealed"; set { } }
        /// <summary>
        /// Lugar hacia el que el personaje debe mirar
        /// </summary>
        public override string WhereToLookAt { get => "DoctorPosition"; set { } }
    }
}
