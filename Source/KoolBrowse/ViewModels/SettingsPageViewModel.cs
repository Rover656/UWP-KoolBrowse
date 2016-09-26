using KoolBrowse.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoolBrowse.ViewModels
{
    class SettingsPageViewModel
    {
        #region Bindable Vars
        public string SearchEngine
        {
            get { return SettingsService.Instance.SearchEngine; }
            set { SettingsService.Instance.SearchEngine = value; }
        }
        public bool StartOnSearchEngine
        {
            get { return SettingsService.Instance.StartOnSearchEngine; }
            set { SettingsService.Instance.StartOnSearchEngine = value; }
        }
        #endregion
    }
}
