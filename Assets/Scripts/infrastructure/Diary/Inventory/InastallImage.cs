using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.infrastructure.Diary.Inventory
{
    internal class InastallImage : MonoInstaller
    {
        [SerializeField] private GameObject ObjectForBackgroundChange; // Объект, который будет использоваться для изменения фона

        public override void InstallBindings()
        {
            // Привязываем ObjectForBackgroundChange к контейнеру Zenject
            Container.Bind<GameObject>()
                .WithId("ObjectForBackgroundChange") // Уникальный идентификатор для привязки
                .FromInstance(ObjectForBackgroundChange)
                .WhenInjectedInto<ChangeBackgroundImage>();

            // Находим все объекты с компонентом ChangeBackgroundImage
            List<ChangeBackgroundImage> backgroundImages = new List<ChangeBackgroundImage>(
                FindObjectsOfType<ChangeBackgroundImage>(true) // true для поиска неактивных объектов
            );

            // Биндим каждый объект с ChangeBackgroundImage
            foreach (var backgroundImage in backgroundImages)
            {
                Container.BindInstance(backgroundImage)
                    .AsCached();
            }
        }
    }
}