using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    class Logger
    {
        /**
        * <summary>
        * Logs whenever a courier is added
        * </summary>
        * 
        * <param name="courier">A courier</param>
        */
        public void LogAddCourier(Courier courier)
        {
            using (StreamWriter w = File.AppendText(@"..\..\..\Data\log.txt"))
            {
                DateTime dateAndTime = DateTime.Now;
                w.WriteLine(dateAndTime.ToString("HH:mm") + " " +
                    dateAndTime.ToString("dd/MM/yy") + " New Courier Added - id = "
                    + courier.CourierId + ", type = " + courier.CourierType());
            }
        }

        /**
        * <summary>
        * Logs whenever a parcel is added to a courier
        * </summary>
        * 
        * <param name="parcel">A parcel</param>
        * <param name="courier">A courier</param>
        */
        public void LogAddParcel(Parcel parcel, Courier courier)
        {
            using (StreamWriter w = File.AppendText(@"..\..\..\Data\log.txt"))
            {
                DateTime dateAndTime = DateTime.Now;
                w.WriteLine(dateAndTime.ToString("HH:mm") + " " +
                    dateAndTime.ToString("dd/MM/yy") + " New Parcel Added (" +
                    parcel.ParcelInformation() + ") Allocated to Courier " + courier.CourierId);
            }
        }

        /**
        * <summary>
        * Logs whenever information about a courier is printed
        * </summary>
        * 
        * <param name="courier">A courier</param>
        */
        public void LogPrintCourierInfo(Courier courier)
        {
            DateTime dateAndTime = DateTime.Now;
            using (StreamWriter w = File.AppendText(@"..\..\..\Data\log.txt"))
            {
                w.WriteLine(dateAndTime.ToString("HH:mm") + " " +
                    dateAndTime.ToString("dd/MM/yy") + " Information printed for Courier " + courier.CourierId);
            }
        }

        /**
        * <summary>
        * Logs whenever information about areas is printed
        * </summary>
        * 
        * <param name="areas">A list of areas</param>
        */
        public void LogAreaInfo(List<string> areas)
        {
            DateTime dateAndTime = DateTime.Now;
            string areaString = "";
            var distinctAreaList = areas.Distinct().ToList(); //Get distinct areas

            foreach (var area in distinctAreaList)
            {
                areaString += area + " ";
            }
            using (StreamWriter w = File.AppendText(@"..\..\..\Data\log.txt"))
            {
                w.WriteLine(dateAndTime.ToString("HH:mm") + " " +
                    dateAndTime.ToString("dd/MM/yy") + " Information printed for area(s): "
                    + areaString);
            }

        }

        /**
        * <summary>
        * Logs whenever a parcel is transferred between couriers
        * </summary>
        * 
        * <param name="originalCourierId">The ID of the courier the parcel is coming from</param>
        * <param name="newCourierId">The ID of the courier the parcel is going to</param>
        */
        public void LogParcelTransfer(int originalCourierId, int newCourierId)
        {
            DateTime dateAndTime = DateTime.Now;
            using (StreamWriter w = File.AppendText(@"..\..\..\Data\log.txt"))
            {
                w.WriteLine(dateAndTime.ToString("HH:mm") + " " +
                    dateAndTime.ToString("dd/MM/yy") + " Parcel " +
                    " transferred from Courier " + originalCourierId +
                    " to Courier " + newCourierId);
            }
        }

        /**
        * <summary>
        * Logs whenever the CSV file is written to
        * </summary>
        * 
        * <param name="database">The database that stores all of the couriers</param>
        */
        public void LogCSVWrite(Database database)
        {
            DateTime dateAndTime = DateTime.Now;
            string allCourierIds = "";
            foreach (int id in database.Couriers.Keys.ToList())
            {
                allCourierIds += id.ToString() + " ";
            }
            using (StreamWriter w = File.AppendText(@"..\..\..\Data\log.txt"))
            {
                w.WriteLine(dateAndTime.ToString("HH:mm") + " " +
                    dateAndTime.ToString("dd/MM/yy") + " Couriers written to CSV file " + allCourierIds);
            }
        }

        /**
        * <summary>
        * Logs whenever the CSV file is read from
        * </summary>
        * 
        * <param name="database">The database that stores all of the couriers</param>
        */
        public void LogCSVRead(Database database)
        {
            DateTime dateAndTime = DateTime.Now;
            string allCourierIds = "";
            foreach (int id in database.Couriers.Keys.ToList())
            {
                allCourierIds += id.ToString() + " ";
            }
            using (StreamWriter w = File.AppendText(@"..\..\..\Data\log.txt"))
            {
                w.WriteLine(dateAndTime.ToString("HH:mm") + " " +
                    dateAndTime.ToString("dd/MM/yy") + " Couriers read from CSV file " + allCourierIds);
            }
        }
    }
}
