using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Auction.Notification.EventsModels;
using Auction.Notification.SqlDependency;

namespace Auction.Notification
{
    public sealed class DataNotificator: IDataNotificator
    {
        public event EventHandler<DataUpdatedEventArgs> OnDataUpdated;

        private readonly SqlDependencyEx _sqlDependency;
        private static string _connectionString;
        private static string _databaseName;
        private static string _tableName;

        private static readonly object locker = new object();

        private DataNotificator()
        {
            _sqlDependency = new SqlDependencyEx(_connectionString, _databaseName, _tableName);
            _sqlDependency.TableChanged += SqlDependencyOnTableChanged;
        }

        private static DataNotificator _instance;

        public static DataNotificator Instance
        {
            get
            {
                lock (locker)
                {
                    return _instance ?? (_instance = new DataNotificator());
                }
            }
        }

        private void SqlDependencyOnTableChanged(object sender, SqlDependencyEx.TableChangedEventArgs e)
        {
            OnDataUpdated?.Invoke(this, new DataUpdatedEventArgs
            {
                Action = (ActionType)e.NotificationType,
                Data = e.Data
            });
        }

        public async Task Listen()
        {
            await _sqlDependency.Start();
        }

        public void Stop()
        {
            _sqlDependency.Stop();
        }

        public static void Register(string connectionString, string databaseName, string tableName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
            _tableName = tableName;
        }
    }
}
