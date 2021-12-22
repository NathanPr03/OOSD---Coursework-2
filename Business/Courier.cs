using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Coursework2
{
    [Serializable]
    public class Courier
    {
        private string deliveryArea;
        private int courierId;
        private List<Parcel> parcels = new List<Parcel>();

        /**
        * <summary>
        * Initialises a new instance of the <see cref="Courier{T}"/>
        * </summary>
        */
        public Courier()
        {

        }
        /**
         * <summary>
         * Getters and setters
         * </summary>
         */
        public virtual string DeliveryArea { get => deliveryArea; set => deliveryArea = value; }
        public int CourierId { get => courierId; set => courierId = value; }
        public List<Parcel> Parcels { get => parcels; set => parcels = value; }

        /**
        * <summary>
        * Adds a parcel to a courier
        * </summary>
        * 
        * <param name="parcel">The parcel to be allocated to a courier</param>
        * 
        * <returns>Returns false but is abstract</returns>
        */
        public virtual bool AddParcel(Parcel parcel)
        {
            return false;
        }

        /**
        * <summary>
        * Prints information about a courier
        * </summary>
        * 
        * <param name="lstBox">The list box from the UI</param>
        */
        public virtual string CourierInfo()
        {
            string courierInfo = "";

            courierInfo += "Courier " + CourierId + " delivers to " + DeliveryArea + " and has the following parcels: \n";

            foreach(var parcel in parcels)
            {
                courierInfo += parcel.ParcelInformation() + "\n";
            }
            return courierInfo;
        }

        /**
        * <summary>
        * Returns the area code for a delivery area. For example delivery area EH10 would have an area code of 10
        * </summary>
        * 
        * <returns>Returns the area code as an int</returns>
        */
        public int AreaCode()
        {
            return Convert.ToInt32(deliveryArea.Split('H').Last());
        }

        /**
       * <summary>
       * Returns the type of courier (Van, Walking or Cycle)
       * </summary>
       * 
       * <returns>Returns the type as a string</returns>
       */
        public string CourierType()
        {
            return this.ToString().Split('.').Last();
        }
    }
}
