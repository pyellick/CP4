﻿#pragma checksum "C:\Users\Parker\source\repos\CP4\Dialogs\AddTeamDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "76770A33BB92ADEFE68626B12854275BD1E9FABBC32865EDCA61F7CA57726C52"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CP4
{
    partial class AddTeamDialog : 
        global::Windows.UI.Xaml.Controls.ContentDialog, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Dialogs\AddTeamDialog.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.ContentDialog element1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)element1).PrimaryButtonClick += this.ContentDialog_PrimaryButtonClick;
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)element1).SecondaryButtonClick += this.ContentDialog_SecondaryButtonClick;
                }
                break;
            case 21: // Dialogs\AddTeamDialog.xaml line 192
                {
                    this.TeamNameTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 22: // Dialogs\AddTeamDialog.xaml line 205
                {
                    this.PrimaryColorComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.PrimaryColorComboBox).SelectionChanged += this.PrimaryColorComboBox_SelectionChanged;
                }
                break;
            case 23: // Dialogs\AddTeamDialog.xaml line 228
                {
                    this.SecondaryColorComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.SecondaryColorComboBox).SelectionChanged += this.SecondaryColorComboBox_SelectionChanged;
                }
                break;
            case 24: // Dialogs\AddTeamDialog.xaml line 250
                {
                    this.TeamLogoComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.TeamLogoComboBox).SelectionChanged += this.TeamLogoComboBox_SelectionChanged;
                }
                break;
            case 25: // Dialogs\AddTeamDialog.xaml line 267
                {
                    this.SelectedLogoBorder = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 26: // Dialogs\AddTeamDialog.xaml line 268
                {
                    this.SelectedLogoImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
