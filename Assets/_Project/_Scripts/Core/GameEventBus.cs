using HairvestMoon.Player;
using HairvestMoon.Tool;
using System;
using UnityEngine;
using static HairvestMoon.Player.PlayerStateController;

namespace HairvestMoon.Core
{
    /// <summary>
    /// Central event bus for all global game events. 
    /// Use strongly-typed argument classes for safety and clarity.
    /// No direct cross-system calls�everything routes through here or ServiceLocator.
    /// </summary>
    public class GameEventBus
    {
        // --- Inventory Events ---
        public event Action InventoryChanged;
        public void RaiseInventoryChanged() => InventoryChanged?.Invoke();

        public event Action BackpackChanged;
        public void RaiseBackpackChanged() => BackpackChanged?.Invoke();

        // --- Time Events ---
        public event Action<GameTimeChangedArgs> TimeChanged;
        public void RaiseTimeChanged(GameTimeChangedArgs args)
            => TimeChanged?.Invoke(args);

        public event Action OnDawn;
        public void RaiseDawn() => OnDawn?.Invoke();

        public event Action OnDusk;
        public void RaiseDusk() => OnDusk?.Invoke();

        public event Action OnNewDay;
        public void RaiseNewDay() => OnNewDay?.Invoke();

        // --- Game State Events ---
        public event Action<GameStateChangedArgs> GameStateChanged;
        public void RaiseGameStateChanged(GameState state)
            => GameStateChanged?.Invoke(new GameStateChangedArgs(state));

        public event Action<InputLockChangedArgs> InputLockChanged;
        public void RaiseInputLockChanged(bool locked)
            => InputLockChanged?.Invoke(new InputLockChangedArgs(locked));

        // --- Input Controller Events ---
        public event Action MenuToggle;
        public void RaiseMenuToggle() => MenuToggle?.Invoke();

        public event Action ToolNext;
        public void RaiseToolNext() => ToolNext?.Invoke();

        public event Action ToolPrevious;
        public void RaiseToolPrevious() => ToolPrevious?.Invoke();

        public event Action<ControlModeChangedArgs> ControlModeChanged;
        public void RaiseControlModeChanged(ControlMode mode)
            => ControlModeChanged?.Invoke(new ControlModeChangedArgs(mode));

        public event Action LookInputDetected;
        public void RaiseLookInputDetected() => LookInputDetected?.Invoke();

        public event Action InteractPressed;
        public void RaiseInteractPressed() => InteractPressed?.Invoke();
        public event Action InteractReleased;
        public void RaiseInteractReleased() => InteractReleased?.Invoke();


        // --- Farming/Player Events ---
        public event Action<Vector3Int> TileTilled;
        public void RaiseTileTilled(Vector3Int pos) => TileTilled?.Invoke(pos);

        public event Action<Vector3Int> TileWatered;
        public void RaiseTileWatered(Vector3Int pos) => TileWatered?.Invoke(pos);

        public event Action<PlayerFormChangedArgs> PlayerFormChanged;
        public void RaisePlayerFormChanged(PlayerForm newForm)
            => PlayerFormChanged?.Invoke(new PlayerFormChangedArgs(newForm));

        // --- Equip/Install Events ---
        public event Action<ItemInstalledEventArgs> ItemInstalled;
        public void RaiseItemInstalled(ItemData item)
        {
            ItemInstalled?.Invoke(new ItemInstalledEventArgs(item));
        }

        // --- System Events ---
        public event Action GlobalSystemsInitialized;
        public void RaiseGlobalSystemsInitialized()
        {
            GlobalSystemsInitialized?.Invoke();
        }

        // --- Tool/Hotbar Events ---
        public event Action<ToolType> ToolChanged;
        public void RaiseToolChanged(ToolType tool) => ToolChanged?.Invoke(tool);

        public event Action<ItemData> SeedSelectionChanged;
        public void RaiseSeedSelectionChanged(ItemData item) => SeedSelectionChanged?.Invoke(item);


        // --- Player Facing Event ---
        public event Action<PlayerFacingController.FacingDirection> FacingChanged;
        public void RaiseFacingChanged(PlayerFacingController.FacingDirection dir)
        {
            FacingChanged?.Invoke(dir);
        }

        // --- Expand with more events as needed (e.g., PlayerDamaged, CropHarvested, etc.) ---
    }

    // --- Event Argument Classes ---

    public class ItemInstalledEventArgs
    {
        public ItemData InstalledItem { get; }
        public ItemInstalledEventArgs(ItemData installedItem)
        {
            InstalledItem = installedItem;
        }
    }

    public class GameStateChangedArgs
    {
        public GameState State { get; set; }
        public GameStateChangedArgs(GameState state) { State = state; }
    }

    public class InputLockChangedArgs
    {
        public bool Locked { get; set; }
        public InputLockChangedArgs(bool locked) { Locked = locked; }
    }

    public class ControlModeChangedArgs
    {
        public ControlMode Mode { get; set; }
        public ControlModeChangedArgs(ControlMode mode) { Mode = mode; }
    }

    public class PlayerFormChangedArgs
    {
        public PlayerStateController.PlayerForm Form { get; set; }
        public PlayerFormChangedArgs(PlayerStateController.PlayerForm form) { Form = form; }
    }

    public class GameTimeChangedArgs
    {
        public int Hour { get; }
        public int Minute { get; }
        public int Day { get; }
        public GameTimeChangedArgs(int hour, int minute, int day)
        {
            Hour = hour; Minute = minute; Day = day;
        }
    }
}
