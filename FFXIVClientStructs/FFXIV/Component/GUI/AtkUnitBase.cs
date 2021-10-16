﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using FFXIVClientStructs.Attributes;
using FFXIVClientStructs.FFXIV.Component.GUI.ULD;

namespace FFXIVClientStructs.FFXIV.Component.GUI
{
    // Component::GUI::AtkUnitBase
    //   Component::GUI::AtkEventListener

    // base class for all AddonXXX classes (visible UI objects)

    // size = 0x220
    // ctor E8 ? ? ? ? 83 8B ? ? ? ? ? 33 C0 

    [StructLayout(LayoutKind.Explicit, Size = 0x220)]
    public unsafe partial struct AtkUnitBase
    {
        [FieldOffset(0x0)] public AtkEventListener AtkEventListener;
        [FieldOffset(0x8)] public fixed byte Name[0x20];
        [FieldOffset(0x28)] public AtkUldManager UldManager;
        [FieldOffset(0xC8)] public AtkResNode* RootNode;
        [FieldOffset(0x108)] public AtkComponentNode* WindowNode;
        [FieldOffset(0x1AC)] public float Scale;
        [FieldOffset(0x182)] public byte Flags;
        [FieldOffset(0x1BC)] public short X;
        [FieldOffset(0x1BE)] public short Y;
        [FieldOffset(0x1CC)] public short ID;
        [FieldOffset(0x1CE)] public short ParentID;
        [FieldOffset(0x1D5)] public byte Alpha;

        [FieldOffset(0x1D8)]
        public AtkCollisionNode**
            CollisionNodeList; // seems to be all collision nodes in tree, may be something else though

        [FieldOffset(0x1E0)] public uint CollisionNodeListCount;

        public bool IsVisible
        {
            get => (Flags & 0x20) == 0x20;
            set => Flags = value ? Flags |= 0x20 : Flags &= 0xDF;
        }

        [MemberFunction("E8 ?? ?? ?? ?? 0F BF CB 0F 28 F8")]
        public partial float GetScale();

        [MemberFunction("E8 ?? ?? ?? ?? 0F BF 45 00")]
        public partial float GetGlobalUIScale();

        [MemberFunction("E8 ?? ?? ?? ?? 8D 56 54")]
        public partial AtkResNode* GetNodeById(uint nodeId);

        [MemberFunction("E8 ?? ?? ?? ?? 8D 55 1C")]
        public partial AtkTextNode* GetTextNodeById(uint nodeId);

        [MemberFunction("E8 ?? ?? ?? ?? 8D 55 4D")]
        public partial AtkImageNode* GetImageNodeById(uint nodeId);

        [MemberFunction("E9 ?? ?? ?? ?? 83 FB 15")]
        public partial byte FireCallbackInt(int callbackValue);

        [MemberFunction("E8 ?? ?? ?? ?? 8B 44 24 20 C1 E8 05")]
        public partial void FireCallback(int valueCount, AtkValue* values, void* a4 = null);

        [VirtualFunction(3)]
        public partial bool Show(int unkInt, bool unkBool = false);
        
        [VirtualFunction(4)]
        public partial bool Hide(bool unknown);
        
        [VirtualFunction(7)]
        public partial void SetPosition(short x, short y);

        [VirtualFunction(46)]
        public partial void OnUpdate(NumberArrayData** numberArrayData, StringArrayData** stringArrayData);

        public void FireCallback(params object[] values) {
            var atkValues = (AtkValue*) Marshal.AllocHGlobal(values.Length * sizeof(AtkValue));
            if (atkValues == null) return;
            try {
                for (var i = 0; i < values.Length; i++) {
                    var v = values[i];
                    switch (v) {
                        case uint uintValue:
                            atkValues[i].Type = ValueType.UInt;
                            atkValues[i].UInt = uintValue;
                            break;
                        case int intValue:
                            atkValues[i].Type = ValueType.Int;
                            atkValues[i].Int = intValue;
                            break;
                        case float floatValue:
                            atkValues[i].Type = ValueType.Float;
                            atkValues[i].Float = floatValue;
                            break;
                        case bool boolValue:
                            atkValues[i].Type = ValueType.Bool;
                            atkValues[i].Byte = (byte) (boolValue ? 1 : 0);
                            break;
                        case string stringValue: {
                            atkValues[i].Type = ValueType.String;
                            var stringBytes = Encoding.UTF8.GetBytes(stringValue);
                            var stringAlloc = Marshal.AllocHGlobal(stringBytes.Length + 1);
                            Marshal.Copy(stringBytes, 0, stringAlloc, stringBytes.Length);
                            Marshal.WriteByte(stringAlloc, stringBytes.Length, 0);
                            atkValues[i].String = (byte*)stringAlloc;
                            break;
                        }
                        default:
                            throw new ArgumentException($"Unable to convert type {v.GetType()} to AtkValue");
                    }
                }

                FireCallback(values.Length, atkValues);
            } finally {
                if (atkValues != null) {
                    for (var i = 0; i < values.Length; i++) {
                        if (atkValues[i].Type == ValueType.String) {
                            Marshal.FreeHGlobal(new IntPtr(atkValues[i].String));
                        }
                    }
                    Marshal.FreeHGlobal(new IntPtr(atkValues));
                }
            }
        }
    }
}
