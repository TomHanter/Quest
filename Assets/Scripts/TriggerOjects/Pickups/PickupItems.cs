using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.TriggerOjects
{
    internal class PickupItems : MonoBehaviour, ITriggerable
    {
        private Pickup _pickup;
        private ToggleObjectOnButtonPress _toggleScript;

        [Inject]
        private void Construct(ToggleObjectOnButtonPress toggleScript)
        {
            _toggleScript = toggleScript;
        }

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        public void Trrigered()
        {
            Debug.Log("подобрал предмет!");
            AssembledPickups.AddPickup(_pickup);
            string allItems = "";
            foreach (Pickup pickup in AssembledPickups.GetAllPickups())
            {
                allItems += pickup.Name;
            }
            Debug.Log(allItems);
            if(_pickup.RenderedOnScreen)
            {
                if (_toggleScript.TargetObject != null && _toggleScript.TargetObject.activeSelf == false)
                {
                    _toggleScript.Activate(true);
                }

                if (_toggleScript.TargetObject != null && !string.IsNullOrEmpty(_pickup.Picture))
                {
                    // Загружаем спрайт из ресурсов
                    Sprite loadedSprite = Resources.Load<Sprite>(_pickup.Picture);

                    if (loadedSprite != null)
                    {
                        var image = _toggleScript.TargetObject.GetComponent<UnityEngine.UI.Image>();
                        if (image != null)
                        {
                            image.sprite = loadedSprite;
                        }
                        else
                        {
                            Debug.LogError("У целевого объекта отсутствует компонент Image.");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Не удалось загрузить спрайт по пути: {_pickup.Picture}");
                    }
                }
            }           

        }
    }

}
