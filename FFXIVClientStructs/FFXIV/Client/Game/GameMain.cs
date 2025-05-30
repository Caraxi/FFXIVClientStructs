using FFXIVClientStructs.FFXIV.Client.Game.Control;

namespace FFXIVClientStructs.FFXIV.Client.Game;

// Client::Game::GameMain
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x4100)]
public unsafe partial struct GameMain {
    [StaticAddress("48 8D 0D ?? ?? ?? ?? 0F 28 F2 48 89 44 24 ??", 3)]
    public static partial GameMain* Instance();

    [FieldOffset(0x0), FixedSizeArray] internal FixedSizeArray4<Festival> _activeFestivals;
    [FieldOffset(0x40), FixedSizeArray] internal FixedSizeArray4<Festival> _queuedFestivals;

    [FieldOffset(0x210)] public ZoneSharedGroupManager ZoneSharedGroupManager;

    [FieldOffset(0xAD8)] public JobGaugeManager JobGaugeManager;

    [FieldOffset(0x3FD0)] public EmoteManager EmoteManager;

    [FieldOffset(0x4088)] public void* CurrentContentFinderConditionRow;
    [FieldOffset(0x4090)] public float TerritoryTransitionDelay; // in seconds
    [FieldOffset(0x4094)] public byte TerritoryTransitionState; // 1 = waiting for layout to load or delay to end, 2 = transition finished, switching territories
    [FieldOffset(0x4096)] public bool ConnectedToZone;

    [FieldOffset(0x4098)] public uint TerritoryLoadState; // 1 = loading, 2 = loaded, 3 = unloading/shutting down
    [FieldOffset(0x409C)] public uint NextTerritoryTypeId;
    [FieldOffset(0x40A0)] public uint CurrentTerritoryTypeId; // can be 0 during loading screens
    [FieldOffset(0x40A4)] public byte CurrentTerritoryIntendedUseId;
    [FieldOffset(0x40A8)] public uint CurrentTerritoryFilterKey;
    [FieldOffset(0x40AC)] public ushort CurrentContentFinderConditionId;
    [FieldOffset(0x40B0)] public uint TransitionTerritoryTypeId;
    [FieldOffset(0x40B4)] public uint TransitionTerritoryFilterKey;
    [FieldOffset(0x40B8)] public uint CurrentMapId;

    [FieldOffset(0x40C8)] public float MilisecondCounter;
    [FieldOffset(0x40CC)] public uint RuntimeSeconds;
    [FieldOffset(0x40D0)] public bool RuntimeSecondsChanged;
    [FieldOffset(0x40D4)] public float Runtime;
    [FieldOffset(0x40D8), CExporterExcel("TerritoryType")] public nint CurrentTerritoryTypeRow;
    [FieldOffset(0x40E0), CExporterExcel("TerritoryIntendedUse")] public nint CurrentTerritoryIntendedUseRow;
    [FieldOffset(0x40E8), CExporterExcel("TerritoryType")] public nint NextTerritoryTypeRow;
    [FieldOffset(0x40F0), CExporterExcel("TerritoryIntendedUse")] public nint NextTerritoryIntendedUseRow;

    [MemberFunction("E8 ?? ?? ?? ?? 44 8B B3 ?? ?? ?? ?? 33 FF")]
    public partial bool IsInInstanceArea();

    [MemberFunction("E8 ?? ?? ?? ?? 88 45 F1")]
    public static partial bool IsInPvPArea();

    [MemberFunction("40 53 48 83 EC 20 48 8B 1D ?? ?? ?? ?? 48 85 DB 74 1E 48 8D 0D")]
    public static partial bool IsInPvPInstance();

    [MemberFunction("E8 ?? ?? ?? ?? 41 83 7F ?? ?? 4C 8D 2D")]
    public static partial bool IsInGPose();

    [MemberFunction("E8 ?? ?? ?? ?? 0F 57 FF 84 C0")]
    public static partial bool IsInIdleCam();

    [MemberFunction("E8 ?? ?? ?? ?? E9 ?? ?? ?? ?? 8B 44 24 70 48 8D 8D ?? ?? ?? ??")]
    public partial void QueueActiveFestivals(uint festival1, uint festival2, uint festival3, uint festival4); // Applies once the current "event" is done (GPose, Cutscene etc)

    [MemberFunction("E8 ?? ?? ?? ?? 80 63 50 FE")]
    public partial void SetActiveFestivals(uint festival1, uint festival2, uint festival3, uint festival4); // Applies immediately regardless of client state

    [StructLayout(LayoutKind.Explicit, Size = 0x04)]
    public struct Festival {
        [FieldOffset(0x00)] public ushort Id;
        [FieldOffset(0x02)] public ushort Phase;
    }
}
