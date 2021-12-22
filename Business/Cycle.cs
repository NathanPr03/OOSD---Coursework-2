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
    public class Cycle : Courier
    {
        /**
        * <summary>
        * Initialises a new instance of the <see cref="Cycle{T}"/>
        * </summary>
        */
        public Cycle()
        {

        }

        /**
        * <summary>
        * Adds a parcel to a cyclist
        * </summary>
        * 
        * <param name="parcel">The parcel to be allocated to a cyclist</param>
        * 
        * <returns>Returns whether the parcel was added succesfully</returns>
        */
        public override bool AddParcel(Parcel parcel)
        {
            if (parcel.Postcode.Split(" ").First() != DeliveryArea || Parcels.Count >= 10)
            {
                return false;
            }
            else
            {
                Parcels.Add(parcel);
                return true;
            }
        }

    }
}
