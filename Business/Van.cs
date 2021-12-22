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
    public class Van : Courier
    {
        /**
        * <summary>
        * Initialises a new instance of the <see cref="Van{T}"/>
        * </summary>
        */
        public Van()
        {
        }

        /**
        * <summary>
        * Adds a parcel to a van
        * </summary>
        * 
        * <param name="parcel">The parcel to be allocated to a van</param>
        * 
        * <returns>Returns whether the parcel was added succesfully</returns>
        */
        public override bool AddParcel(Parcel parcel)
        {
            foreach(var deliveryArea in DeliveryArea.Split(" "))
            {
                if (parcel.Postcode.Split(" ").First() != deliveryArea || Parcels.Count >= 100)
                {
                    return false;
                }
                else
                {
                    Parcels.Add(parcel);
                    return true;
                }
            }
            return false;
        }

        public override string CourierInfo()
        {
            string courierInfo = "";

            courierInfo += "Courier " + CourierId + " delivers to:";
            
            foreach (var deliveryArea in DeliveryArea.Split(" "))
            {
                courierInfo += "\n" + deliveryArea;
            }
            foreach (var parcel in Parcels)
            {
                courierInfo += "\n" + parcel.ParcelInformation();
            }

            return courierInfo;
        }
    }
}
