using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.infrastructure
{
    public class ScenePlayerLocator : MonoInstaller
    {
        public Transform StartPoint;
        public GameObject Prefab;
        public Transform Parent;
        public Transform CameraTransform;

        public override void InstallBindings()
        {
            BindCameraTransform();
            InstantiateMainCharacter();
        }

        private void InstantiateMainCharacter()
        {
            if (Prefab == null)
            {
                Debug.LogError("Prefab не назначен в инспекторе!", this);
                return;
            }


            PlayerMoveController playerMoveController = Container
                .InstantiatePrefabForComponent<PlayerMoveController>(Prefab, StartPoint.position, Prefab.transform.rotation, Parent);

            Container
                .Bind<PlayerMoveController>()
                .FromInstance(playerMoveController)
                .AsSingle();

            // Выполняем инъекцию вручную, так как объект уже был создан
            Container.Inject(playerMoveController);
        }

        private void BindCameraTransform()
        {
            Container
                            .Bind<Transform>()
                            .FromInstance(CameraTransform)
                            .AsSingle();
        }
    }
}
