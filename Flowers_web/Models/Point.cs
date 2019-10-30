using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Google;
using Google.Apis;


namespace Flowers_web.Models
{
  public  class Point
    {
        private List<String> zip;

        public long[,] points;
       public List<String> driver1;
       public List<String> driver2;
      
            
        public DriverEntity dr1 ;
        public DriverEntity dr2;
        public Point(List<string> zips)
        {
            zip = zips;
            points = new long[zip.Count, zip.Count];
            string d = null;
             dr1=new DriverEntity("driver 1");
             dr2=new DriverEntity("driver 2");
            //for (int i = 0; i < 250; i++)
            //{
            //    Console.WriteLine(i+". "+ GetDistance("K1V 2K3", "k4a 0e7"));
            //}
            GetValues(zip);
            separateOrderList(2);
            GetRoutDriver(driver1,dr1);
            GetRoutDriver(driver2,dr2);
        }

       

        void GetValues(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < list.Count; j++)
                {
                    points[i, j] = GetDistance(list[i], list[j]);
                    //Console.Write(list[i] + "=" + points[i, j] + "  ");
                   
                }
            }


        }

        void separateOrderList(int driversKol)
        {
            driver1 = new List<string>(); //new string[zip.Count / 2 + zip.Count % 2];
            driver2 = new List<string>(); //new string[zip.Count - (zip.Count / 2 + zip.Count % 2)];
          //  Console.WriteLine("");
          //  Console.WriteLine("D2 =" + driver2.Count.ToString() + ";" + "D1 =" + driver1.Count.ToString());
            int maxX = 0;
            int maxY = 0;

            //double minY = points[0, 0];
            for (int i = 0; i < points.GetLength(0); i++)
            {
                for (int j = 0; j < points.GetLength(1); j++)
                {
                    if (points[i, j] > points[maxX, maxY])
                    {
                        maxX = i;
                        maxY = j;

                    }

                }
            }

            driver1.Add(zip[maxX]);
            //driver2.Add(zip[maxY]);

           // Console.WriteLine("D1 first zip =" + driver1[0]);

            for (int p = 0; p < (zip.Count / driversKol + zip.Count % driversKol) - 1; p++)
            {

                double min = points[maxX, maxY];
                for (int i = 0; i < points.GetLength(1); i++)
                {
                    if (points[maxX, i] < min && points[maxX, i] != 0)
                    {
                        min = points[maxX, i];
                        maxY = i;

                    }
                }

                driver1.Add(zip[maxY]);

                //Console.WriteLine(string.Join("|", driver1));
                //Console.WriteLine("Min = " + min);

                for (int j = 0; j < points.GetLength(0); j++) //delete x
                {
                    points[maxX, j] = 0;

                }

                for (int j = 0; j < points.GetLength(0); j++) //delete y
                {

                    points[j, maxY] = 0;
                    points[j, maxX] = 0;


                }

                //for (int i = 0; i < zip.Count; i++)
                //{
                //    Console.WriteLine("");

                //    for (int j = 0; j < zip.Count; j++)
                //    {

                //        Console.Write(points[i, j] + "  ");
                //    }
                //}

                maxX = maxY;
                maxY = 0;
            }
            // zip.ForEach(f=>f.);
            //zip.Where(d=>d.)

            foreach (var z in zip)
            {
                // Console.WriteLine("z " +z);
                foreach (var dr1 in driver1)
                {
                    //   Console.WriteLine( "dr1 " + dr1);
                    //   Console.WriteLine(!z.Equals(dr1) + ": " + z + " - "  + dr1);
                    if (!z.Equals(dr1) && !driver2.Contains(z) && !driver1.Contains(z))
                    {
                        driver2.Add(z);
                       // Console.WriteLine(z);
                    }
                }
            }
        }



        double GetXmlFromFileDistance()
        {
            try
            {
                Xml.DirectionsResponse cars = null;
                string responseFromServer;
                XmlSerializer serializer = new XmlSerializer(typeof(Xml.DirectionsResponse));


                using (XmlReader readerXml = XmlReader.Create(@"XMLFile1.xml"))
                {
                    cars = (Xml.DirectionsResponse) serializer.Deserialize(readerXml);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }

        public long GetDistance(string a1, string a2)
        {
            long result = 0;
            try
            {
                string query =
                    $"https://maps.googleapis.com/maps/api/directions/xml?origin=+{a1}+&destination=+{a2}+&sensor=false&alternatives=false&key=AIzaSyBEnVr56lRcRHGGX31OV8_Sj2AI1zzgekk";
                WebRequest request = WebRequest.Create(query);

                WebResponse response = request.GetResponse();

                Stream data = response.GetResponseStream();

                StreamReader reader = new StreamReader(data);

                // json-formatted string from maps api
                string responseFromServer = reader.ReadToEnd();

                Xml.DirectionsResponse distance = null;
                //    string path = cars.xml;

                XmlSerializer serializer = new XmlSerializer(typeof(Xml.DirectionsResponse));

                StreamReader reader1 = new StreamReader(data);
                // distance = (Xml.DirectionsResponse)serializer.Deserialize(reader);
                reader1.Close();
                response.Close();

                using (XmlReader readerXml = XmlReader.Create(new StringReader(responseFromServer)))
                {
                    distance = (Xml.DirectionsResponse) serializer.Deserialize(readerXml);
                }

                long.TryParse(distance.Route.Leg.Distance.Value, out result);
                result = result / 1000;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return result;
        }


      
        string GetQueryForDriver(List<string> zips)
        {
            //https://maps.googleapis.com/maps/api/directions/xml?origin=" & S1 & "&destination=" & S1 & "&waypoints=optimize:true"
            //& AV4 & "|" & AV5 & "|" & AV6 & "|" & AV7 & "|" & AV8 & "|" & AV9 & "|" & AV10 & "|" & AV11 & "|" & AV12 & "|" & AV13 &
            //"|" & AV14 & "|" & AV15 & "|" & AV16 & "|" & AV17 & "|" & AV18 & "|" & AV19 & "|" & AV20 & "|" & AV21 & "|" & AV22 & "|"
            //& AV23 & "|" & AV24 & "|" & AV25 & "|" & AV26 & "|" & AV27 & "|" & AV28 & "" & AV29 & "|" & AV30 &
            //"|&key=";"//end_address

            string warehouse = "4095 Belgreen Dr, Ottawa, ON K1G 3N2";
            string q =
                $"https://maps.googleapis.com/maps/api/directions/xml?origin={warehouse}&destination={warehouse}&waypoints=optimize:true";
            //& AV4 & "|" & AV5 & "|" & AV6 & "|" & AV7 & "|" & AV8 & "|" & AV9 & "|" & AV10 & "|" & AV11 & "|" & AV12 & "|" & AV13 & "|"
            //& AV14 & "|" & AV15 & "|" & AV16 & "|" & AV17 & "|" & AV18 & "|" & AV19 & "|" & AV20 & "|" & AV21 & "|" & AV22 & "|" & AV23 &
            //"|" & AV24 & "|" & AV25 & "|" & AV26 & "|" & AV27 & "|" & AV28 & "" & AV29 & "|" & AV30 &
            //"|&"; "//end_address"
            foreach (var dr1 in zips)
            {
                q += $"|{dr1}|";
            }

            return q += "&key=";


        }

        void GetRoutDriver(List<string> driverZipList, DriverEntity driver)
        {
            try
            {
                string query = GetQueryForDriver(driverZipList);
                WebRequest request = WebRequest.Create(query);

                WebResponse response = request.GetResponse();

                Stream data = response.GetResponseStream();

                StreamReader reader = new StreamReader(data);

                // json-formatted string from maps api
                string responseFromServer = reader.ReadToEnd();

                RouteXml.DirectionsResponse routes = null;
                //    string path = cars.xml;

                XmlSerializer serializer = new XmlSerializer(typeof(RouteXml.DirectionsResponse));

                StreamReader reader1 = new StreamReader(data);
                // distance = (Xml.DirectionsResponse)serializer.Deserialize(reader);
                reader1.Close();
                response.Close();

                using (XmlReader readerXml = XmlReader.Create(new StringReader(responseFromServer)))
                {
                    routes = (RouteXml.DirectionsResponse) serializer.Deserialize(readerXml);
                }

                FillingRoutes(routes, driver);
                //double.TryParse(distance.Route.Leg.Distance.Value, out result);
                // result = result / 1000;





            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



        }

        /// <summary>
        /// Заполнение роутов для драйверов
        /// </summary>
        /// <param name="routes"></param>
        void FillingRoutes(RouteXml.DirectionsResponse routes, DriverEntity driver)
        {
            int i = 0;
            double d = 0;
            int t = 0;
           // DriverEntity driverEntity = new DriverEntity(driver.DriverName);
            foreach (var route in routes.Route.Leg)
            {
          
                if (double.TryParse(route.Distance.Value, out d) && int.TryParse(route.Duration.Value, out t))
                {
                    driver.Routes.Add(new Route
                    {
                        OrderId = i++, // temp
                        EndAddress = route.End_address,
                        Distance = d/1000,
                        Time =  new TimeSpan(0,0,0,t),
                        Status = i.ToString() + "status"
                    });
                }
                else
                {
                    throw new ArgumentException("route.Distance.Value не обработано!");
                }
            }

         

            
           



        }
    }
}
