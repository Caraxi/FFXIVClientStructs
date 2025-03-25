namespace FFXIVClientStructs.FFXIV.Client.Game.Event;

[GenerateInterop]
[Inherits<GatheringEventHandler>]
[StructLayout(LayoutKind.Explicit, Size = 0x478)]
public unsafe partial struct GatheringPointEventHandler {
    [FieldOffset(0x475)] public bool QuickGatheringEnabled;
}
