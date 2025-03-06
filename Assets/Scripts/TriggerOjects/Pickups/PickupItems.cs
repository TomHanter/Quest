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

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        public void Trrigered()
        {
            Debug.Log("подобрал предмет!");
            AssembledPickups.AddPickup(_pickup);
            string allItems = "";
            foreach(Pickup pickup in AssembledPickups.GetAllPickups())
            {
                allItems += pickup.Name;
            } 
            Debug.Log(allItems);
        }
    }
}
