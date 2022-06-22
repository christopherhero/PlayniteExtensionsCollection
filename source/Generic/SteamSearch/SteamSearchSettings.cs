﻿using Playnite.SDK;
using Playnite.SDK.Data;
using SteamCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamSearch
{
    public class SteamSearchSettings : ObservableObject
    {
        private string selectedManualCountry = "US";
        public string SelectedManualCountry { get => selectedManualCountry; set => SetValue(ref selectedManualCountry, value); }
        private bool useManualCurrency = false;
        public bool UseManualCurrency { get => useManualCurrency; set => SetValue(ref useManualCurrency, value); }
    }

    public class SteamSearchSettingsViewModel : ObservableObject, ISettings
    {
        private readonly SteamSearch plugin;
        private SteamSearchSettings editingClone { get; set; }

        private SteamSearchSettings settings;
        public SteamSearchSettings Settings
        {
            get => settings;
            set
            {
                settings = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, string> steamCountriesDictionary;
        public Dictionary<string, string> SteamCountriesDictionary { get => steamCountriesDictionary; set => SetValue(ref steamCountriesDictionary, value); }

        public SteamSearchSettingsViewModel(SteamSearch plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            this.plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<SteamSearchSettings>();

            // LoadPluginSettings returns null if not saved data is available.
            if (savedSettings != null)
            {
                Settings = savedSettings;
            }
            else
            {
                Settings = new SteamSearchSettings();
            }

            SteamCountriesDictionary = Steam.GetCountryLocCurrencyDictionary();
        }

        public void BeginEdit()
        {
            // Code executed when settings view is opened and user starts editing values.
            editingClone = Serialization.GetClone(Settings);
        }

        public void CancelEdit()
        {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to Option1 and Option2.
            Settings = editingClone;
        }

        public void EndEdit()
        {
            // Code executed when user decides to confirm changes made since BeginEdit was called.
            // This method should save settings made to Option1 and Option2.
            plugin.SavePluginSettings(Settings);
        }

        public bool VerifySettings(out List<string> errors)
        {
            // Code execute when user decides to confirm changes made since BeginEdit was called.
            // Executed before EndEdit is called and EndEdit is not called if false is returned.
            // List of errors is presented to user if verification fails.
            errors = new List<string>();
            return true;
        }
    }
}