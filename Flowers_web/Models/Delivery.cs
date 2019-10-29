using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;


namespace Flowers_web.Models
{
    public class Delivery
    {
        flowersEntities db;
       public List<Order_Del> OrderList { get; set; }
        static Delivery Delivery2 { get; set; }
        private ObservableCollection<Route> _routes1;
        private ObservableCollection<Route> _routes2;
        public int Driver_ID { get; set; }
        public string ComboDr1{ get; set; }
        public string Driver2{ get; set; }
      public  List<DriverEntity> DriverList{ get; set; }
        public List<Driver> Drivers { get; set; }
        String[] zipForDriver1 = { "K2W", "K2T", "K2K", "K2V", "K2S", "K2M", "K2H", "K2R", "K2L", "K2J", "K2G", "K2B", "K2C", "K2A", "K1Z", "K1Y", "K4M", "K1B", "K1X", "K1V", "J9J", "J9H", "J9A", "J8Y" };
        String[] zipForDriver2 = { "K1S", "K1R", "K2P", "K1P", "K1H", "K1T", "K1G", "K1B", "K1K", "K1N", "K1L", "K1M", "K1K", "K1J", "K1C", "K1W", "K1E", "K4A", "J8X", "J8Z", "J8T", "J8V", "J8P", "J8R" };

        /// <summary>
        /// Возвращает список drivers
        /// </summary>
        /// <returns></returns>
        public List<Driver> GetDrivers()
        {
            Drivers = db.Drivers.ToList();
            ComboDr1 = Drivers[0].Driver_ID.ToString();
            Driver2 = Drivers[1].Driver_ID.ToString();
            return Drivers;
        }
        /// <summary>
        /// Коллекция маршрутов для водителя 1
        /// </summary>
        public ObservableCollection<Route> Routes1
        {
            get { return _routes1; }
            set
            {
                _routes1 = value;
            }
        }
        /// <summary>
        /// Коллекция маршрутов для водителя 2
        /// </summary>
        public ObservableCollection<Route> Routes2
        {
            get { return _routes2; }
            set
            {
                _routes2 = value;
            }
        }
        public Delivery() { }
        protected Delivery(int i) 
        {
            db = new flowersEntities();
            OrderList = db.Order_Del.Where(d => d.Date_Delived.Value.Year == DateTime.Now.Year &&
           d.Date_Delived.Value.Month == DateTime.Now.Month &&
           d.Date_Delived.Value.Day == DateTime.Now.Day).ToList();
            GetDrivers();
        }

        public static Delivery Instance()
        {
            if (Delivery2 == null)
            {
                Delivery2=new Delivery(1);
                return Delivery2;
            }
            else
            {
                return Delivery2;
            }
        }

        public List<Order_Del> GetOrderList(string searching)
        {
            DateTime date;
            DateTime.TryParse(searching, out date);
            OrderList = db.Order_Del.Where(s => s.Date_Delived.Value.Year == date.Year &&
            s.Date_Delived.Value.Month == date.Month &&
            s.Date_Delived.Value.Day == date.Day).ToList();
            return OrderList;
        }
        /// <summary>
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        public static double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = LevenshteinDistance.Compute(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        void Equals(string zipOrdera,Order_Del orderDel, string[] zipForDriver,string ComboDriver)
        {
            foreach (var dr1 in zipForDriver)
            {
                if (dr1.Equals(zipOrdera))
                {
                    orderDel.Driver_ID = Convert.ToInt32(ComboDriver);
                    orderDel.Order_Status = "InTransite";
                     
                    break;
                }
            }
            try { db.SaveChanges(); } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public void SetSimpleDrivers()
        {

            foreach (var orderDel in OrderList)
            {
                var zipOrdera = orderDel.Cust2.Address.Address_ZIPCODE.Split(' ')[0];
                Equals(zipOrdera, orderDel, zipForDriver1, ComboDr1);
                Equals(zipOrdera, orderDel, zipForDriver2, Driver2);
            }
        }
        public void SetDrivers()
        {
            String[] zipForDriver1 = { "K2W", "K2T", "K2K", "K2V", "K2S", "K2M", "K2H", "K2R", "K2L", "K2J", "K2G", "K2B", "K2C", "K2A", "K1Z", "K1Y", "K4M", "K1B", "K1X", "K1V", "J9J", "J9H", "J9A", "J8Y"};
            String[] zipForDriver2 = { "K1S", "K1R", "K2P", "K1P", "K1H", "K1T", "K1G", "K1B", "K1K", "K1N", "K1L", "K1M", "K1K", "K1J", "K1C", "K1W", "K1E", "K4A", "J8X", "J8Z", "J8T", "J8V", "J8P", "J8R" };
           DriverList = new List<DriverEntity>();
            List<String> zips = new List<string>();
            foreach (var orderDel in OrderList)
            {
                zips.Add(orderDel.Cust2.Address.Address_Number_Street + " " + orderDel.Cust2.Address.Address_Name_Street
                + " " + orderDel.Cust2.Address.Address_City + " " + orderDel.Cust2.Address.Address_ZIPCODE);
            }
            var point = new Point(zips);
            var dr1 = point.dr1;
            var dr2 = point.dr2;
            DriverList.Add(dr1);
            DriverList.Add(dr2);
            foreach (var orderDel in OrderList)
            {
                string a1 = orderDel.Cust2.Address.Address_Number_Street + " " + orderDel.Cust2.Address.Address_Name_Street
                 + " " + orderDel.Cust2.Address.Address_City + " " + orderDel.Cust2.Address.Address_ZIPCODE;
                foreach (var item in DriverList)
                {
                    foreach (var route in item.Routes)
                    {
                        double b1 = CalculateSimilarity(a1, route.EndAddress);
                        
                        if (b1>0.5)//(route.EndAddress.Equals(a1))
                        {
                            route.OrderId = orderDel.Order_ID;
                            route.Status = orderDel.Order_Status;
                        }
                    }
                }


            }


            //for (int i=0;i<OrderList.Count;i++)
            //{
            //    for (int i1= i; i1 < list.Count; i1++)
            //    {
            //        for(int i2= i1; i2 < list[i1].Routes.Count; i2++)
            //        {
            //            list[i1].Routes[i2].OrderId = OrderList[i].Order_ID;
            //            list[i1].Routes[i2].Status = OrderList[i].Order_Status;
            //            break;
            //        }
            //        break;
            //    }
            //}

            //dr1.Routes.ForEach(f => f.OrderId=);
            //dr2.Routes.ForEach(f => Routes2.Add(f));
            // var a=  dr2.Routes;

            


        }
    }
}