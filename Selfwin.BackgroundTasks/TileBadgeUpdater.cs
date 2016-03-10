using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using NotificationsExtensions.Badges;
using Selfnet;
using Selfwin.Core;

namespace Selfwin.BackgroundTasks
{
    public sealed class TileBadgeUpdater : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var defer = taskInstance.GetDeferral();
            try
            {
                var status = await this.GetSelfossUnreadCount();

                if (status != 0)
                {
                    this.UpdateTileBadge(status);
                }
            }
            finally
            {
                defer.Complete();
            }
        }

        private void UpdateTileBadge(int status)
        {
            var badgeContent = new BadgeNumericNotificationContent((uint)status);
            var badgeNotification = new BadgeNotification(badgeContent.GetXml());
            var updater = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            updater.Update(badgeNotification);
        }

        private async Task<int> GetSelfossUnreadCount()
        {
            var app = new SelfwinApp();
            var settings = app.Settings();
            var api = new SelfossApi(settings.SelfossOptions);
            var stats = (await api.Sources.Stats()).ToList();
            if (stats != null && stats.Any())
            {
                return stats.Sum(stat => stat.Unread);
            }
            return 0;
        }
    }
}