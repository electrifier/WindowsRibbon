//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using RibbonLib;
using RibbonLib.Controls;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private static class Cmd
        {
            public const uint cmdApplication = 2;
            public const uint cmdRecentItems = 10;
            public const uint cmdButtonOpen = 11;
            public const uint cmdButtonHelp = 14;
            public const uint cmdQat = 3;
            public const uint cmdTab1 = 5;
            public const uint cmdGroup1_1 = 6;
            public const uint cmdDropDownColor = 7;
            public const uint cmdGroup1_2 = 8;
            public const uint cmdFontPicker = 9;
            public const uint cmdGroup1_3 = 12;
            public const uint cmdInRibbon = 13;
            public const uint cmdGroup1_4 = 15;
            public const uint cmdComboBox = 16;
            public const uint cmdQatCustomize = 4;
        }

        // ContextPopup CommandName

        public Ribbon Ribbon { get; private set; }
        public RibbonApplicationMenu Application { get; private set; }
        public RibbonRecentItems RecentItems { get; private set; }
        public RibbonButton ButtonOpen { get; private set; }
        public RibbonHelpButton ButtonHelp { get; private set; }
        public RibbonQuickAccessToolbar Qat { get; private set; }
        public RibbonTab Tab1 { get; private set; }
        public RibbonGroup Group1_1 { get; private set; }
        public RibbonDropDownColorPicker DropDownColor { get; private set; }
        public RibbonGroup Group1_2 { get; private set; }
        public RibbonFontControl FontPicker { get; private set; }
        public RibbonGroup Group1_3 { get; private set; }
        public RibbonInRibbonGallery InRibbon { get; private set; }
        public RibbonGroup Group1_4 { get; private set; }
        public RibbonComboBox ComboBox { get; private set; }

        public RibbonItems(Ribbon ribbon)
        {
            if (ribbon == null)
                throw new ArgumentNullException(nameof(ribbon), "Parameter is null");
            this.Ribbon = ribbon;
            Application = new RibbonApplicationMenu(ribbon, Cmd.cmdApplication);
            RecentItems = new RibbonRecentItems(ribbon, Cmd.cmdRecentItems);
            ButtonOpen = new RibbonButton(ribbon, Cmd.cmdButtonOpen);
            ButtonHelp = new RibbonHelpButton(ribbon, Cmd.cmdButtonHelp);
            Qat = new RibbonQuickAccessToolbar(ribbon, Cmd.cmdQat, Cmd.cmdQatCustomize);
            Tab1 = new RibbonTab(ribbon, Cmd.cmdTab1);
            Group1_1 = new RibbonGroup(ribbon, Cmd.cmdGroup1_1);
            DropDownColor = new RibbonDropDownColorPicker(ribbon, Cmd.cmdDropDownColor);
            Group1_2 = new RibbonGroup(ribbon, Cmd.cmdGroup1_2);
            FontPicker = new RibbonFontControl(ribbon, Cmd.cmdFontPicker);
            Group1_3 = new RibbonGroup(ribbon, Cmd.cmdGroup1_3);
            InRibbon = new RibbonInRibbonGallery(ribbon, Cmd.cmdInRibbon);
            Group1_4 = new RibbonGroup(ribbon, Cmd.cmdGroup1_4);
            ComboBox = new RibbonComboBox(ribbon, Cmd.cmdComboBox);
        }

    }
}
