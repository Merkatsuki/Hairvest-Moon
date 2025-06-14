using HairvestMoon.Core;
using HairvestMoon.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace HairvestMoon.UI
{
    /// <summary>
    /// Shows fill bar for backpack capacity: filled unlocked slots / unlocked slots.
    /// Updates in response to backpack changes.
    /// </summary>
    public class BackpackCapacityBarUI : MonoBehaviour, IBusListener
    {
        [SerializeField] private Image fillImage;

        private BackpackInventorySystem _backpackInventory;

        public void InitializeUI()
        {
            _backpackInventory = ServiceLocator.Get<BackpackInventorySystem>();
        }

        public void RegisterBusListeners()
        {
            var bus = ServiceLocator.Get<GameEventBus>();
            bus.GlobalSystemsInitialized += OnGlobalSystemsInitialized;
            bus.BackpackChanged += Refresh;
        }

        private void OnGlobalSystemsInitialized()
        {
            Refresh();
        }

        /// <summary>
        /// Updates fill amount: filled unlocked slots divided by unlocked slots.
        /// Always clamps unlocked to actual slot count.
        /// </summary>
        private void Refresh()
        {
            if (_backpackInventory == null || _backpackInventory.Slots == null)
                return;

            int actualSlotCount = _backpackInventory.Slots.Count;
            int unlocked = Mathf.Min(_backpackInventory.UnlockedSlots, BackpackInventorySystem.MaxSlots, actualSlotCount);

#if UNITY_EDITOR
            Debug.Log($"[BackpackCapacityBarUI] Fill: {unlocked} unlocked, model slots: {actualSlotCount}");
#endif

            int filled = 0;
            var slots = _backpackInventory.Slots;
            for (int i = 0; i < unlocked; i++)
            {
                if (slots[i].Item != null)
                    filled++;
            }
            float fillAmount = (unlocked > 0) ? (float)filled / unlocked : 0f;
            fillImage.fillAmount = fillAmount;
        }
    }
}
