using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDataAccess.Models;
using MongoDB.Driver;
using Rebar_LielMymon1;

namespace MongoDataAccess.DataAccess
{
    public class RebarDataAccess
    {
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        private const string DatabaseName = "Rebar-8200";
        private const string ShakeCollection = "Shake";
        private const string OrderCollection = "Order";
        private const string MenuCollection = "Menu";
        private const string RebarDataCollection = "RebarData";
        private const string OrdersHistoryCollection = "OrdersHistory";
        private const string RebarBranchCollection = "RebarBranch";
        

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }

        
            
        public async Task WhatsYourOrder()
        {
            int count = 0;

            var AllShakes = await GetAllShakes();
            string ItemInOrder = "", CustomerName = ""; char size;
            OrderModel Order = new OrderModel();
            Console.WriteLine(GetMenu());
            DateTime now = DateTime.Now;
            Order.OrderDate = DateTime.Today;
            ShakeModel FullItem = null;
            Console.WriteLine("what u wanna order? press stop to end");
            ItemInOrder = Console.ReadLine();
            while (count < 10 && ItemInOrder != "stop")
            {

                FullItem = AllShakes.Find(x => x.Name.Equals(ItemInOrder));
                if (FullItem == null) { Console.WriteLine("Not exist!");return; }
                Order.ShakesList.Add(FullItem);
                Console.WriteLine("size:");
                size = char.Parse(Console.ReadLine());
                if (size == 's')
                    Order.Payment += FullItem.Price_S;
                if (size == 'm')
                    Order.Payment += FullItem.Price_M;
                if (size == 'l')
                    Order.Payment += FullItem.Price_L;
                count++;
                ItemInOrder = Console.ReadLine();
            }
            Console.WriteLine("name:");
            CustomerName = Console.ReadLine();
            Order.CustomerName = CustomerName;
            Order.OrderingTimeSeconds = (DateTime.Now - now).Seconds;

            await AddNewOrder(Order);
        }


        public async Task<List<ShakeModel>> GetAllShakes()
        {
            var ShakesCollection = ConnectToMongo<ShakeModel>(ShakeCollection);
            var results = await ShakesCollection.FindAsync(_ => true);

            return results.ToList();
        }
        public async Task<List<OrdersHistory>> GetAllOrdersHistory()
        {
            var OrdersCollection = ConnectToMongo<OrdersHistory>(OrdersHistoryCollection);
            var results = await OrdersCollection.FindAsync(_ => true);

            return results.ToList();
        }
        public async Task<List<RebarBranchModel>> GetAllBranches()
        {
            var BranchesCollection = ConnectToMongo<RebarBranchModel>(RebarBranchCollection);
            var results = await BranchesCollection.FindAsync(_ => true);

            return results.ToList();
        }
        public async Task<List<MenuModel>> GetMenu()
        {
            var Menu = ConnectToMongo<MenuModel>(MenuCollection);
            var results = await Menu.FindAsync(_ => true);

            return results.ToList();
        }

        public async Task SetDataToBranch(RebarBranchModel Rebarbranch,int count,int sum)
        {
            Console.WriteLine(Rebarbranch.Id);
            try
            {
                RebarDataModel NewData = new RebarDataModel() { RebarBranchId = Rebarbranch.Id, Date = DateTime.Now, NumOfOrders = count, InCome = sum, Expenses = 100 };

                var RebardataCollection = ConnectToMongo<RebarDataModel>(RebarDataCollection);
                await RebardataCollection.InsertOneAsync(NewData);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task AddNewShake(ShakeModel shake)
        {
            try
            {
                var ShakesCollection = ConnectToMongo<ShakeModel>(ShakeCollection);
                await ShakesCollection.InsertOneAsync(shake);

                var Menu = ConnectToMongo<MenuModel>(MenuCollection);
                MenuModel NewItemToMenu = new MenuModel() { ItemName = shake.Name };
                await Menu.InsertOneAsync(NewItemToMenu);
                await Console.Out.WriteLineAsync("Shake added succesfully!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public Task AddNewRebarBranch(RebarBranchModel NewBranch)
        {
            var RebarbranchCollection = ConnectToMongo<RebarBranchModel>(RebarBranchCollection);
            return RebarbranchCollection.InsertOneAsync(NewBranch);

        }

        public async Task AddNewOrder(OrderModel order)
        {
            try
            {
                var OrdersCollection = ConnectToMongo<OrderModel>(OrderCollection);
                await OrdersCollection.InsertOneAsync(order);

                var historyCollection = ConnectToMongo<OrdersHistory>(OrdersHistoryCollection);
                OrdersHistory x = new OrdersHistory(order);
                await historyCollection.InsertOneAsync(x);
                await Console.Out.WriteLineAsync("We got your order");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, couldnt take your order!");
            }



        }

        public async Task CashBoxClose(RebarBranchModel Rebarbranch)
        {
            string cashBoxPassword;
            int FiveTries = 0;
            Console.WriteLine("You need to enter password to close the cashBox!");

            do
            {
                if (FiveTries == 5)
                {
                    Console.WriteLine("Try again latter...");
                    return;

                }
                cashBoxPassword = Console.ReadLine();
                FiveTries++;
            }
            while (cashBoxPassword != Rebarbranch.ManagerPassword);
            var history = await GetAllOrdersHistory();

            int count = 0, sum = 0;
            foreach (var historyItem in history)
            {
                if (historyItem.OrderDate.Day == DateTime.Now.Day)
                {
                    count++;
                    sum += historyItem.Paymet;
                }

            }
            
            Console.WriteLine("There were {0} orders today!", count);
            Console.WriteLine("Sum of all orders:" + sum);
            SetDataToBranch(Rebarbranch, count, sum);
            Console.WriteLine("CashBox Closed!");
        }



    }
}
