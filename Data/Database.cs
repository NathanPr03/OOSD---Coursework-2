using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Coursework2
{
    public class Database
    {
        
        private static Database instance;

        /**
         * <summary>
         * Private constructor
         * </summary>
         */
        private Database() { }

        /**
         * <summary>
         * Constructor for the Database setter
         * </summary>
         * 
         * <returns>An instance of the database</returns>
         */
        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }
        private Dictionary<int, Courier> couriers = new Dictionary<int, Courier>();

       
        BinaryFormatter binaryFormatter = new BinaryFormatter(); //Binary formatter used for serialization

        /**
         * <summary>
         * Getter and setter
         * </summary>
         */
        public Dictionary<int, Courier> Couriers { get => couriers; set => couriers = value; }

        /**
        * <summary>
        * Adds a courier to the database
        * </summary>
        * 
        * <param name="courier">The courier to be added to the database</param>
        * 
        * <returns>Returns whether the courier was added successfullyt</returns>
        */
        public bool AddCourier(Courier courier)
        {
            if (Couriers.ContainsKey(courier.CourierId))
            {
                return false;
            }
            else
            {
                Couriers.Add(courier.CourierId, courier);
                return true;
            }
        }

        /**
        * <summary>
        * Serializes the database to the CSV file
        * </summary>
        */
        public void SerializeWrite()
        {
            using (Stream stream = File.Open(@"..\..\..\Data\serialize.csv", FileMode.Create))
            {
                binaryFormatter.Serialize(stream, couriers.Values.ToList());
            }
        }

        /**
        * <summary>
        * De-serializes the database from the CSV file
        * </summary>
        * 
        * <returns></returns>
        */
        public List<Courier> SerializeRead()
        {
            List<Courier> deserialized;

            using (Stream stream = File.Open(@"..\..\..\Data\serialize.csv", FileMode.Open))
            {
                deserialized = (List<Courier>)binaryFormatter.Deserialize(stream);
            }
            return deserialized;
        }
    }
}
