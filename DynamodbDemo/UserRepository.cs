using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamodbDemo
{
    public class UserRepository : IData
    {
        public readonly DynamoDBContext DbContext;
        public AmazonDynamoDBClient DynamoClient;
        public string AccessKey = "";
        public string SecretKey = "";
        public static RegionEndpoint RegionEndpoint = RegionEndpoint.APSouth1;
        public UserRepository()
        {
            DynamoClient = new AmazonDynamoDBClient(AccessKey, SecretKey, RegionEndpoint);

            DbContext = new DynamoDBContext(DynamoClient, new DynamoDBContextConfig
            {
                //Setting the Consistent property to true ensures that you'll always get the latest 
                ConsistentRead = true,
                SkipVersionCheck = true
            });
        }


        public IEnumerable<T> GetAll<T>() where T : class
        {

            throw new NotImplementedException();
            //List<ScanCondition> conditions = new List<ScanCondition>();
            //conditions.Add(new ScanCondition("UserId", ScanOperator.Equal,
            //IEnumerable < T > items = DbContext.FromScanAsync<T>();
            //return items;
        }

        public T GetItem<T>(string key) where T : class
        {
            return DbContext.LoadAsync<T>(key).Result;
        }

        public void Store<T>(T item) where T : new()
        {
            DbContext.SaveAsync(item);
        }
        public void UpdateItem<T>(T item) where T : class
        {
            T savedItem = DbContext.LoadAsync(item).Result;

            if (savedItem == null)
            {
                throw new AmazonDynamoDBException("The item does not exist in the Table");
            }

            DbContext.SaveAsync(item);
        }

        /// <summary>
        /// Deletes an Item from the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void DeleteItem<T>(T item)
        {
            var savedItem = DbContext.LoadAsync(item);

            if (savedItem == null)
            {
                throw new AmazonDynamoDBException("The item does not exist in the Table");
            }

            DbContext.DeleteAsync(item);
        }
        public Task<CreateTableResponse> CreateTable<T>(CreateTableRequest request)
        {
            var response = DynamoClient.CreateTableAsync(request);
            return response;
        }
    }
}
