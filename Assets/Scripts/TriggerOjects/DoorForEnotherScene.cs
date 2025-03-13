﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TriggerOjects
{
    internal class DoorForEnotherScene : MonoBehaviour, ITriggerable
    {
        [SerializeField] string SceneName;
        [SerializeField] private Vector3 SpawnPosition; // Точка спавна в новой сцене
        [SerializeField] private Vector3 SpawnEulerAngles = Vector3.zero; // Углы Эйлера для поворота

        // Скрытое поле для Quaternion, вычисляемое на основе SpawnEulerAngles
        private Quaternion SpawnRotation => Quaternion.Euler(SpawnEulerAngles);
        public event Action<string> OnActivated;
        public void Trrigered()
        {
            Debug.Log("Переход на новую локу!");

            // Сохраняем данные о точке спавна
            PlayerSpawnData.SpawnPosition = SpawnPosition;
            PlayerSpawnData.SpawnRotation = SpawnRotation;

            // Вызываем событие для загрузки сцены
            OnActivated?.Invoke(SceneName);
        }
    }
}