using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    [Serializable]
    public class Parcel
    {
        private string postcode;
        private string addressee;
        private int parcelId;

        /**
        * <summary>
        * Initialises a new instance of the <see cref="Parcel{T}"/>
        * </summary>
        */
        public Parcel()
        {
        }

        /**
        * <summary>
        * Getters and setters
        * </summary>
        */
        public string Postcode { get => postcode; set => postcode = value; }
        public string Addressee { get => addressee; set => addressee = value; }
        public int ParcelId { get => parcelId; set => parcelId = value; }

        /**
        * <summary>
        * Returns information about a parcel
        * </summary>
        * 
        * <returns>Information about a parcel</returns>
        */
        public string ParcelInformation()
        {
            return this.postcode + ", \"" + this.addressee + "\", " + this.parcelId;
        }
    }
}
