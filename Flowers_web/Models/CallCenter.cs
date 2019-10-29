using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flowers_web.Models
{
    public class CallCenter
    {
        private flowersEntities db;

        public int Order_ID { get; set; }
        public string Order_Status { get; set; }
        public int Bouquet_ID { get; set; }
        public int Florist_ID { get; set; }
        public int Cust_ID { get; set; }
        public Nullable<System.DateTime> Date_Delived { get; set; }

        public Cust2 Client { get; set; }
        public List<Bouquet> Bouquets { get; set; }
        public List<int> Quantities { get; set; }
        public List<string> Sizes { get; set; }
        public string ComboBouqets { get; set; }
        public string ComboQuantities { get; set; }
        public string ComboSizes { get; set; }
        
        public string PictureElement { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDelivery { get; set; }
        public string FindUser { get; set; }
        public string Sum { get; set; }

        public string Address_Numder_Appartment { get; set; }
        public string Address_Number_Street { get;set;}
        public string Address_Name_Street { get; set; }
        public string Address_City { get; set; }
        public string Address_ZIPCODE { get; set; }
        public string Address_State { get; set; }
        public string Address_Country { get; set; }
        public string Address_line1 { get; set; }
        public string Address_line2 { get; set; }

      

        public CallCenter()
        {
            db = new flowersEntities();
            Quantities = new List<int>();
            Sizes = new List<string>();
            for (int i = 1; i < 11; i++)
            {
                Quantities.Add(i);
            }

            Sizes.Add("S");
            Sizes.Add("M");
            Sizes.Add("L");
            DateDelivery=DateTime.Today;
        }
        /// <summary>
        /// Возвращает список букетов
        /// </summary>
        /// <returns></returns>
        public List<Bouquet> GetBouqets()
        {
            Bouquets = new List<Bouquet>();
            Bouquets = db.Bouquets.ToList();
            ComboBouqets = Bouquets[0].Bouquet_ID.ToString();
            return Bouquets;
        }

        public bool SaveOrder(CallCenter model)
        {
            if (model.Order_ID == 0)
            {
                return CreateNewOrder(model);
            }
            return EditOldOrder(model);

        }
        /// <summary>
        /// Возвращает true если равны
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="oldData"></param>
        /// <returns></returns>
        void RefreshAddress(Address newData, Address oldData)
        {
            if (!oldData.Address_City.Equals(newData.Address_City))
            {
                oldData.Address_City = newData.Address_City;
            }
            if (!oldData.Address_ZIPCODE.Equals(newData.Address_ZIPCODE))
            {
                oldData.Address_ZIPCODE = newData.Address_ZIPCODE;
            }
            
        }

        void RefreshCustomer(Cust2 newCust, Cust2 oldCust)
        {
            if (!oldCust.Email.Equals(newCust.Email))
            {
                oldCust.Email = newCust.Email;
            }
            if (!oldCust.Fname.Equals(newCust.Fname))
            {
                oldCust.Fname = newCust.Fname;
            }
        }
        void RefresOrder(Order_Del newOrder, Order_Del oldOrder)
        {
            if (!oldOrder.Bouquet_ID.Equals(newOrder.Bouquet_ID))
            {
                oldOrder.Bouquet_ID = newOrder.Bouquet_ID;
            }
            if (!oldOrder.Date_Delived.Equals(newOrder.Date_Delived))
            {
                oldOrder.Date_Delived = newOrder.Date_Delived;
            }
        }
        /// <summary>
        /// Редактирование старой заявки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditOldOrder(CallCenter model)
        {
            bool result = false;
            // 
            var oldOrderFromDB= db.Order_Del.FirstOrDefault(d => d.Order_ID == model.Order_ID);
            var oldAddress = oldOrderFromDB.Cust2.Address;
            var oldCustomer = oldOrderFromDB.Cust2;
           
            Address address = new Address()
            {
                Address_City = model.Address_City,
                Address_ZIPCODE = model.Address_ZIPCODE,
                Address_Numder_Appartment = model.Address_Numder_Appartment,
                Address_Country = model.Address_Country,
                Address_line1 = model.Address_line1,
                Address_line2 = model.Address_line2,
                Address_Name_Street = model.Address_Name_Street,
                Address_Number_Street = model.Address_Number_Street,
                Address_State = model.Address_State,
            };
            RefreshAddress(address,oldAddress);
            //create customer
            Cust2 cust2 = new Cust2()
            {
                Lname = model.Client.Lname,
                Fname = model.Client.Fname,              
                Email = model.Client.Email,
                Phone = model.Client.Phone,
                Gender = model.Gender
            };
           
            RefreshCustomer(cust2, oldCustomer);
            //create order
            int id = 0;
            int.TryParse(model.ComboBouqets, out id);
            int quantities = 0;
            int.TryParse(model.ComboQuantities, out quantities);
            decimal summ = 0;
            decimal.TryParse(model.Sum, out summ);

            Order_Del order = new Order_Del()
            {
                Bouquet_ID = id,              
                Bouquet_Quontity = quantities,
                Date_Delived = DateDelivery,
                Total_Price = summ,
                Size_Bouquets = model.ComboSizes,                
            };
            RefresOrder(order, oldOrderFromDB);

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        bool CreateNewOrder(CallCenter model)
        {
            bool result = false;
            //create address

            Address address = new Address()
            {
                Address_City = model.Address_City,
                Address_ZIPCODE = model.Address_ZIPCODE,
                Address_Numder_Appartment = model.Address_Numder_Appartment,
                Address_Country = model.Address_Country,
                Address_line1 = model.Address_line1,
                Address_line2 = model.Address_line2,
                Address_Name_Street = model.Address_Name_Street,
                Address_Number_Street = model.Address_Number_Street,
                Address_State = model.Address_State,
            };
            db.Addresses.Add(address);
            //create customer
            Cust2 cust2 = new Cust2()
            {
                Lname = model.Client.Lname,
                Fname = model.Client.Fname,
                Address = address,
                Email = model.Client.Email,
                Phone = model.Client.Phone,
                Gender = model.Gender
            };
            db.Cust2.Add(cust2);

            //create order
            int id = 0;
            int.TryParse(model.ComboBouqets, out id);
            int quantities = 0;
            int.TryParse(model.ComboQuantities, out quantities);
            decimal summ = 0;
            decimal.TryParse(model.Sum, out summ);

            Order_Del order = new Order_Del()
            {
                Bouquet_ID = id,
                Florist_ID = 25,
                Cust2 = cust2,
                Driver_ID = 1000,
                Order_Date = DateTime.Today,
                Bouquet_Quontity = quantities,
                Date_Delived = DateDelivery,
                Total_Price = summ,
                Size_Bouquets = model.ComboSizes,
                Order_Status = "Payed"
            };
            db.Order_Del.Add(order);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        /// <summary>
        /// Возвращает имена и фамилии эзеров
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public IEnumerable<Cust2> UsersFio(string prefix)
        {
           // var list = db.Cust2.Where(d => d.Phone.StartsWith(prefix)).ToList();
          var list2=  (from N in db.Cust2
                       where N.Phone.StartsWith(prefix)
             select new Cust2 {Phone= N.Phone });
            //List<Cust2> list2 = new List<Cust2>();
            //foreach (var item in list)
            //{
            //    list2.Add(item);
            //}
            return list2;
        }
        /// <summary>
        /// Возвращает конкретного юзера по имени и фамилии
        /// </summary>
        /// <returns></returns>
        public Cust2 GetUsers(string fio)
        { 
            string fname = fio.Split(' ')[1];
            string lname = fio.Split(' ')[0];
            var user = db.Cust2.FirstOrDefault(d => d.Lname.Equals(lname) && d.Fname.Equals(fname));


            return user;
        }
    }
}