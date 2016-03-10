using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Notifications;
using NotificationsExtensions.Badges;
using Selfnet;

namespace Selfwin.Core
{
    public class SelfwinApp
    {
        private SelfossApi _api;

        private SelfossApi Api
        {
            get
            {
                var settings = Settings();
                if (settings != null && settings.SelfossOptions != null)
                {
                    if (_api == null)
                    {
                        _api = new SelfossApi(settings.SelfossOptions);
                    }
                    else
                    {
                        this.UpdateOptions(_api.Options, settings.SelfossOptions);
                    }
                }
                return _api;
            }
        }

        private void UpdateOptions(ConnectionOptions opts, ConnectionOptions app)
        {
            opts.Scheme = app.Scheme;
            opts.Host = app.Host;
            opts.Base = app.Base;
            opts.Port = app.Port;
            opts.Username = app.Username;
            opts.Password = app.Password;
        }

        private List<IItemViewModel> ItemsCache = new List<IItemViewModel>();

        public async Task<IList<IItemViewModel>> Items()
        {
            try
            {
                if ((ItemsCache == null || ItemsCache.Count == 0) && Api != null)
                {
                    await this.Refresh();
                }
            }
            catch (Exception ex)
            {
                //TODO: report error to user
            }
            return ItemsCache;
        }

        public async Task Refresh()
        {
            try
            {
                var settings = this.Settings();
                var items = await Api.Items.Get(new ItemsFilter());
                var vms = items.Select(item => this.CreateItemVm(settings, item)).ToList();
                ItemsCache = vms;
            }
            catch (Exception ex)
            {
                //TODO: report error to user
            }
        }

        public virtual IItemViewModel CreateItemVm(SelfWinSettings settings, Item item)
        {
            return null;
        }

        public async Task<IList<IItemViewModel>> UnreadItems()
        {
            try
            {
                var settings = this.Settings();
                var items = await Api.Items.Get(new ItemsFilter()
                {
                    ItemStatus = Status.Unread,
                });
                var vms = items.Select(item => this.CreateItemVm(settings, item)).ToList();
                return vms;
            }
            catch (Exception ex)
            {
                //TODO: report error to user
                return new List<IItemViewModel>();
            }
        }

        public async Task<IList<IItemViewModel>> StarredItems()
        {
            var items = await Items();
            return items.Where(i => i.Starred).ToList();
        }

        public void ChangeFavorite(IItemViewModel item, bool starred)
        {
            item.Starred = starred;
            if (starred)
            {
                Api.Items.MarkStarred(item.Parameter.Id);
            }
            else
            {
                Api.Items.MarkUnstarred(item.Parameter.Id);
            }
        }

        public void ChangeUnread(IItemViewModel item, bool unread)
        {
            item.Unread = unread;
            if (unread)
            {
                Api.Items.MarkUnread(item.Parameter.Id);
            }
            else
            {
                Api.Items.MarkRead(item.Parameter.Id);
            }
        }

        public SelfWinSettings Settings()
        {
            var settings = new SelfWinSettings();
            var store = SettingsStore();
            ReadConnection(store, settings.SelfossOptions);
            return settings;
        }

        public async Task SaveSettings(ISettingsViewModel settings)
        {
            var newSettings = new SelfWinSettings(settings.Url, settings.Port, settings.Username, settings.Password);

            SaveToApplicationData(newSettings);

            await ValidateSettings();
        }

        private async Task ValidateSettings()
        {
            var authenticated = await this.Api.Login();
            if (!authenticated)
            {
                throw new SelfWinException("Could not authenticate");
            }
        }

        private void SaveToApplicationData(SelfWinSettings newSettings)
        {
            var store = SettingsStore();
            SaveConnection(store, newSettings.SelfossOptions);
        }

        private static ApplicationDataContainer SettingsStore()
        {
            var applicationData = ApplicationData.Current;
            var store = applicationData.LocalSettings;
            return store;
        }

        private void ReadConnection(ApplicationDataContainer mainStore, ConnectionOptions conn)
        {
            ApplicationDataContainer store;
            if (mainStore.Containers.TryGetValue("connection", out store))
            {
                conn.Scheme = store.Values["scheme"] as string;
                conn.Host = store.Values["host"] as string;
                conn.Port = (int) store.Values["port"];
                conn.Base = store.Values["base"] as string;
                conn.Username = store.Values["username"] as string;
                conn.Password = store.Values["password"] as string;
            }
        }

        private void SaveConnection(ApplicationDataContainer store, ConnectionOptions conn)
        {
            ApplicationDataContainer connStore;
            if (!store.Containers.TryGetValue("connection", out connStore))
            {
                connStore = store.CreateContainer("connection", ApplicationDataCreateDisposition.Always);
            }

            connStore.Values["scheme"] = conn.Scheme;
            connStore.Values["host"] = conn.Host;
            connStore.Values["port"] = conn.Port;
            connStore.Values["base"] = conn.Base;
            connStore.Values["username"] = conn.Username;
            connStore.Values["password"] = conn.Password;
        }

        public void UpdateTile(int status)
        {
            var badgeContent = new BadgeNumericNotificationContent((uint)status);
            var badgeNotification = new BadgeNotification(badgeContent.GetXml());
            var updater = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            updater.Update(badgeNotification);
        }
    }
}