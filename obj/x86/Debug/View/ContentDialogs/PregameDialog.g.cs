﻿#pragma checksum "C:\Users\stepa\Desktop\TotallyFairRace\View\ContentDialogs\PregameDialog.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "09F03AA629D1275328FDD5EDEA6BE11C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TotallyFairRace.View.ContentDialogs
{
    partial class PregameDialog : 
        global::Windows.UI.Xaml.Controls.ContentDialog, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // View\ContentDialogs\PregameDialog.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.ContentDialog element1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)element1).PrimaryButtonClick += this.StartSimulationClick;
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)element1).SecondaryButtonClick += this.ContentDialog_SecondaryButtonClick;
                }
                break;
            case 3: // View\ContentDialogs\PregameDialog.xaml line 26
                {
                    this.GridView = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // View\ContentDialogs\PregameDialog.xaml line 43
                {
                    this.CarColorPicker = (global::TotallyFairRace.View.Controls.CarColorPicker)(target);
                }
                break;
            case 5: // View\ContentDialogs\PregameDialog.xaml line 48
                {
                    this.BorderDistance = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 6: // View\ContentDialogs\PregameDialog.xaml line 54
                {
                    this.BorderBid = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 7: // View\ContentDialogs\PregameDialog.xaml line 60
                {
                    this.BorderNumOfCars = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 8: // View\ContentDialogs\PregameDialog.xaml line 58
                {
                    this.PlaceBidTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
