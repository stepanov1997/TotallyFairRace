﻿#pragma checksum "C:\Users\stepa\Fakultet\Projekti\Interakcija čovjek-računar\TotallyFairRace\View\Controls\CarRaceBar.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "15C953263A0262B6779E049DC19D3094"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TotallyFairRace.View.Controls
{
    partial class CarRaceBar : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_FrameworkElement_Margin(global::Windows.UI.Xaml.FrameworkElement obj, global::Windows.UI.Xaml.Thickness value)
            {
                obj.Margin = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class CarRaceBar_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ICarRaceBar_Bindings
        {
            private global::TotallyFairRace.View.Controls.CarRaceBar dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.Image obj3;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj3MarginDisabled = false;

            private CarRaceBar_obj1_BindingsTracking bindingsTracking;

            public CarRaceBar_obj1_Bindings()
            {
                this.bindingsTracking = new CarRaceBar_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 13 && columnNumber == 72)
                {
                    isobj3MarginDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3: // View\Controls\CarRaceBar.xaml line 13
                        this.obj3 = (global::Windows.UI.Xaml.Controls.Image)target;
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // ICarRaceBar_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::TotallyFairRace.View.Controls.CarRaceBar)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::TotallyFairRace.View.Controls.CarRaceBar obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_GridLayout(obj.GridLayout, phase);
                    }
                }
            }
            private void Update_GridLayout(global::Windows.UI.Xaml.Controls.Grid obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_GridLayout(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_GridLayout_Margin(obj.Margin, phase);
                    }
                }
            }
            private void Update_GridLayout_Margin(global::Windows.UI.Xaml.Thickness obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\Controls\CarRaceBar.xaml line 13
                    if (!isobj3MarginDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Margin(this.obj3, obj);
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class CarRaceBar_obj1_BindingsTracking
            {
                private global::System.WeakReference<CarRaceBar_obj1_Bindings> weakRefToBindingObj; 

                public CarRaceBar_obj1_BindingsTracking(CarRaceBar_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<CarRaceBar_obj1_Bindings>(obj);
                }

                public CarRaceBar_obj1_Bindings TryGetBindingObject()
                {
                    CarRaceBar_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_GridLayout(null);
                }

                public void DependencyPropertyChanged_GridLayout_Margin(global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop)
                {
                    CarRaceBar_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        global::Windows.UI.Xaml.Controls.Grid obj = sender as global::Windows.UI.Xaml.Controls.Grid;
                        if (obj != null)
                        {
                            bindings.Update_GridLayout_Margin(obj.Margin, DATA_CHANGED);
                        }
                    }
                }
                private global::Windows.UI.Xaml.Controls.Grid cache_GridLayout = null;
                private long tokenDPC_GridLayout_Margin = 0;
                public void UpdateChildListeners_GridLayout(global::Windows.UI.Xaml.Controls.Grid obj)
                {
                    if (obj != cache_GridLayout)
                    {
                        if (cache_GridLayout != null)
                        {
                            cache_GridLayout.UnregisterPropertyChangedCallback(global::Windows.UI.Xaml.FrameworkElement.MarginProperty, tokenDPC_GridLayout_Margin);
                            cache_GridLayout = null;
                        }
                        if (obj != null)
                        {
                            cache_GridLayout = obj;
                            tokenDPC_GridLayout_Margin = obj.RegisterPropertyChangedCallback(global::Windows.UI.Xaml.FrameworkElement.MarginProperty, DependencyPropertyChanged_GridLayout_Margin);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\Controls\CarRaceBar.xaml line 9
                {
                    this.GridLayout = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // View\Controls\CarRaceBar.xaml line 13
                {
                    this.CarImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 4: // View\Controls\CarRaceBar.xaml line 18
                {
                    this.Semaphore = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 5: // View\Controls\CarRaceBar.xaml line 20
                {
                    this.SvgImageSource2 = (global::Windows.UI.Xaml.Media.Imaging.SvgImageSource)(target);
                }
                break;
            case 6: // View\Controls\CarRaceBar.xaml line 15
                {
                    this.SvgImageSource = (global::Windows.UI.Xaml.Media.Imaging.SvgImageSource)(target);
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
            switch(connectionId)
            {
            case 1: // View\Controls\CarRaceBar.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    CarRaceBar_obj1_Bindings bindings = new CarRaceBar_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

