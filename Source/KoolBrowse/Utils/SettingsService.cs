using System.Linq;
using Windows.Security.Credentials;
using Template10.Services.SettingsService;
using System.ComponentModel;
using System;
using Template10.Mvvm;
using System.Runtime.CompilerServices;

namespace KoolBrowse.Utils
{
    public class SettingsService : BindableBase
    {
        #region Singleton
        private readonly static Lazy<SettingsService> _instance = new Lazy<SettingsService>(() => new SettingsService());
        public static SettingsService Instance { get { return _instance.Value; } }
        private SettingsService()
        {
        }
        #endregion

        #region Storage helper
        private readonly SettingsHelper _helper = new SettingsHelper();
        public void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            _helper.Write(propertyName, value);
            RaisePropertyChanged(propertyName);
        }
        public T Get<T>(T defaultValue, [CallerMemberName] string propertyName = null)
        {
            return _helper.Read<T>(propertyName, defaultValue);
        }
        #endregion

        #region General
        public string StartPage
        {
            get { return Get("SearchEngine"); }
            set { Set(value); }
        }
        #endregion

        #region Search
        public string SearchEngine
        {
            get { return Get("Google"); }
            set { Set(value); }
        }
        public bool StartOnSearchEngine
        {
            get { return Get(true); }
            set { Set(value); }
        }
        #endregion
    }
}