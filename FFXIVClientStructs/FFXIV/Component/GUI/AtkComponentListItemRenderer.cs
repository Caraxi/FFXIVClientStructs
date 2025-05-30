using FFXIVClientStructs.FFXIV.Client.System.Memory;

namespace FFXIVClientStructs.FFXIV.Component.GUI;

// Component::GUI::AtkComponentListItemRenderer
//  Component::GUI::AtkComponentButton
//   Component::GUI::AtkComponentBase
//     Component::GUI::AtkEventListener
// common CreateAtkComponent function "E8 ?? ?? ?? ?? 4C 8B F0 48 85 C0 0F 84 ?? ?? ?? ?? 49 8B 4D 08"
// type 14
[GenerateInterop]
[Inherits<AtkComponentButton>, Inherits<AtkDragDropInterface>(0xF0)]
[StructLayout(LayoutKind.Explicit, Size = 0x1A8)]
public unsafe partial struct AtkComponentListItemRenderer : ICreatable {
    [FieldOffset(0x120), CExporterUnion("RowTemplate")] public AtkResNode* RowTemplateNode; // if RowTemplateNodeCount == 1
    [FieldOffset(0x120), CExporterUnion("RowTemplate")] public AtkResNode** RowTemplateNodeList; // if RowTemplateNodeCount != 1
    [FieldOffset(0x128)] public AtkComponentListItemPopulator Populator;
    [FieldOffset(0x184)] public int ListItemIndex;
    [FieldOffset(0x1A4)] public int RowTemplateNodeCount;

    [MemberFunction("40 53 48 83 EC 20 48 8B D9 E8 ?? ?? ?? ?? 33 C9 48 C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ??")]
    public partial void Ctor();
}
