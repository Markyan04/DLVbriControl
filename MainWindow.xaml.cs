using System;
using System.Windows;
using System.Windows.Controls;
using DLVbri;

namespace DisplayLinkBrightness
{
    public partial class MainWindow : Window
    {
        private VBri _vBri;
        private const string AppVersion = "1.00-Beta";
        private int _currentBrightness = -1; // Track current brightness

        public MainWindow()
        {
            InitializeComponent();
            tbAppVersion.Text = AppVersion;
            _vBri = new VBri();
        }

        // Window load event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_vBri.Virtual_Control_Init())
            {
                AppendOutput("Initialization failed! Please check the device connection.");
                DisableControls();
                return;
            }

            if (!_vBri.Virtual_Control_Connect())
            {
                AppendOutput("Failed to connect the device!");
                DisableControls();
                return;
            }

            tbDllVersion.Text = _vBri.Dll_Version();
            AppendOutput("Device initialized successfully");
            RefreshCurrentBrightness(); // Gets the current brightness at initialization
        }

        // Disable all controls
        private void DisableControls()
        {
            sliderBrightness.IsEnabled = false;
            btnBrightnessUp.IsEnabled = false;
            btnBrightnessDown.IsEnabled = false;
            btnReadBrightness.IsEnabled = false;
        }

        // Output log
        private void AppendOutput(string message)
        {
            txtOutput.AppendText($"{DateTime.Now:HH:mm:ss} - {message}\n");
            txtOutput.ScrollToEnd();
        }

        // Slider value change event
        private void SliderBrightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!IsLoaded || _currentBrightness == (int)e.NewValue) return;

            int newValue = (int)e.NewValue;
            int result = _vBri.Set_Brightness(newValue);

            if (result == (int)ReturnCode.VC_OK)
            {
                _currentBrightness = newValue;
                tbCurrentLevel.Text = $"Current Level: {newValue}";
                AppendOutput($"Brightness set to {newValue}");
            }
            else
            {
                sliderBrightness.Value = _currentBrightness; // Roll back the slider position
                HandleResult(result, "");
            }
        }

        // Refresh brightness
        private void RefreshCurrentBrightness()
        {
            int currentValue = -1;
            int result = _vBri.Get_Brightness(ref currentValue);

            if (result == (int)ReturnCode.VC_OK)
            {
                _currentBrightness = currentValue;
                sliderBrightness.Value = currentValue;
                tbCurrentLevel.Text = $"Current Level: {currentValue}";
                AppendOutput($"Brightness refreshed: {currentValue}");
            }
            else
            {
                AppendOutput("Failed to refresh brightness");
            }
        }

        // Button event handling
        private void btnBrightnessUp_Click(object sender, RoutedEventArgs e)
        {
            int result = _vBri.Brightness_Up();
            HandleResult(result, "Brightness increased");
            if (result == (int)ReturnCode.VC_OK) RefreshCurrentBrightness();
        }

        private void btnBrightnessDown_Click(object sender, RoutedEventArgs e)
        {
            int result = _vBri.Brightness_Down();
            HandleResult(result, "Brightness decreased");
            if (result == (int)ReturnCode.VC_OK) RefreshCurrentBrightness();
        }

        private void btnReadBrightness_Click(object sender, RoutedEventArgs e) => RefreshCurrentBrightness();

        private void btnClear_Click(object sender, RoutedEventArgs e) => txtOutput.Clear();

        // Unified processing results
        private void HandleResult(int result, string successMessage)
        {
            switch (result)
            {
                case (int)ReturnCode.VC_OK when !string.IsNullOrEmpty(successMessage):
                    AppendOutput(successMessage);
                    break;
                case (int)ReturnCode.VC_ERROR:
                    AppendOutput("Operation failed: Device not responding");
                    break;
                case (int)ReturnCode.VC_OUT_OF_RANGE:
                    AppendOutput("Operation failed: Brightness limit reached");
                    break;
                default:
                    if (result != (int)ReturnCode.VC_OK)
                        AppendOutput($"Unknown error code: {result}");
                    break;
            }
        }
    }

    public enum ReturnCode
    {
        VC_OK = 0x01,
        VC_ERROR = 0x00,
        VC_OUT_OF_RANGE = 0xFF
    }
}