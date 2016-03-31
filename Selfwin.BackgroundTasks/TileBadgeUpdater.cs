using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
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
                var app = new SelfwinApp();

                var status = await this.GetSelfossUnreadCount(app);

                if (status != 0)
                {
                    this.UpdateTileBadge(app, status);
                }
            }
            catch (Exception ex)
            {
                //TODO: report error to user.
            }
            finally
            {
                defer.Complete();
            }
        }

        private void UpdateTileBadge(SelfwinApp app, int status)
        {
            app.UpdateTile(status);
        }

        private async Task<int> GetSelfossUnreadCount(SelfwinApp app)
        {
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