using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Coursework2
{
    public class DeliverySystem
    {
        Database database = Database.Instance;
        Logger logger = new Logger();

        DisplayToUI displayToUI = new DisplayToUI();

        public Database _Database_ { get => database; set => database = value; }

        /**
         * <summary>
         * Takes input from text boxes in UI, then calls create method
         * </summary>
         * 
         * <param name="deliveryArea">The area(s) that a courier delivers to</param>
         * <param name="courierId">The unique courier ID</param>
         * <param name="courierType">The couriers type (Van, Wlakng or Cycle)</param>
         */
        public void CreateCourier(string deliveryArea, string courierIdAsString, string courierType)
        {
            int courierId = 0;
            try
            {
                courierId = Convert.ToInt32(courierIdAsString);
            }
            catch
            {
                displayToUI.ShowMessageBox("The courier ID is not an int");
                return;
            }
            if (courierType == "Van")
            {
                CreateVan(deliveryArea, courierId);
            }
            else if (courierType == "Cycle")
            {
                CreateCyclist(deliveryArea, courierId);
            }
            else if (courierType == "Walking")
            {
                CreateWalker(deliveryArea, courierId);
            }
            else
            {
                displayToUI.ShowMessageBox("The courier type must be Van, Cycle or Walking");
            }
        }

        /**
        * <summary>
        * Creates a van courier
        * </summary>
        * 
        * <param name="deliveryArea">The area(s) that a courier delivers to</param>
        * <param name="courierId">The unique courier ID</param>
        */
        public void CreateVan(string deliveryArea, int courierId)
        {
            Van van = new Van();

            van.DeliveryArea = deliveryArea;
            van.CourierId = courierId;
            List<string> deliveryAreas = new List<string>();
            //Vans can have more than one delivery, this handles them one at a time
            foreach (string singleDeliveryArea in deliveryArea.Split(" "))
            {
                ValidateVan(van, deliveryAreas, singleDeliveryArea);
            }

            if (database.AddCourier(van))
            {
                logger.LogAddCourier(van);
            }else
            {
                displayToUI.ShowMessageBox("Each courier needs to have a unique ID");
            }
        }

        /**
        * <summary>
        * Validates the user input for a van courier
        * </summary>
        * 
        * <param name="van">A van courier</param>
        * <param name="singleDeliveryArea">A delivery area entered by the user</param>
        * <param name="deliveryAreas">A list to store all the delivery areas of a van. Ensures they are unique</param>
        */
        public void ValidateVan(Van van, List<string> deliveryAreas, string singleDeliveryArea)
        {
            if (deliveryAreas.Contains(singleDeliveryArea))
            {
                displayToUI.ShowMessageBox("Each delivery area needs to be unique");
                return;
            }
            else if (van.AreaCode() > 22)
            {
                displayToUI.ShowMessageBox("The maximum postcode is 22");
                return;
            }
            else
            {
                deliveryAreas.Add(singleDeliveryArea);
            }
        }
        /**
        * <summary>
        * Creates a cycle courier
        * </summary>
        * 
        * <param name="deliveryArea">The area that a cyclist delivers to</param>
        * <param name="courierId">The unique courier ID</param>
        */
        public void CreateCyclist(string deliveryArea, int courierId)
        {
            Cycle cyclist = new Cycle();

            cyclist.DeliveryArea = deliveryArea;
            cyclist.CourierId = courierId;
            ValidateCyclist(cyclist, deliveryArea);
            if (database.AddCourier(cyclist))
            {
                logger.LogAddCourier(cyclist);
            }else
            {
                displayToUI.ShowMessageBox("Each courier needs to have a unique ID");
            }
        }

        /**
        * <summary>
        * Validates the user input for a cycle courier
        * </summary>
        * 
        * <param name="cyclist">A cyclist courier</param>
        * <param name="deliveryArea">The area that a cyclist delivers to</param>
        */
        public void ValidateCyclist(Cycle cyclist, string deliveryArea)
        {
            if (cyclist.AreaCode() > 22)
            {
                displayToUI.ShowMessageBox("The maximum postcode is 22");
                return;
            }
            else if (deliveryArea.Length > 4)
            {
                displayToUI.ShowMessageBox("A cyclist can only have 1 delivery area");
            }
        }
        /**
        * <summary>
        * Creates a walker courier
        * </summary>
        * 
        * <param name="deliveryArea">The area that a cyclist delivers to</param>
        * <param name="courierId">The unique courier ID</param>
        */
        public void CreateWalker(string deliveryArea, int courierId)
        {
            Walking walker = new Walking();

            walker.DeliveryArea = deliveryArea;
            walker.CourierId = courierId;

            ValidateWalker(walker, deliveryArea);
            if (database.AddCourier(walker))
            {
                logger.LogAddCourier(walker);
            }else
            {
                displayToUI.ShowMessageBox("Each courier needs to have a unique ID");
            }
        }

        /**
        * <summary>
        * Validates the user input for a cycle courier
        * </summary>
        * 
        * <param name="walker">A walker courier</param>
        * <param name="deliveryArea">The area that a walker delivers to</param>
        */
        public void ValidateWalker(Walking walker, string deliveryArea)
        {
            if (walker.AreaCode() > 4)
            {
                displayToUI.ShowMessageBox("Walking courier can only deliver parcels between EH1 and EH4");
                return;
            }
            else if (deliveryArea.Length > 4)
            {
                displayToUI.ShowMessageBox("A walker can only have 1 delivery area");
            }
        }
        /**
         * <summary>
         * Takes input from text boxes in UI, creates a parcel then adds it to the first available courier
         * </summary>
         * 
         * <param name="parcelPostcode">Delivery postcode of the parcel</param>
         * <param name="parcelId">Unique ID of the parcel</param>
         * <param name="parcelAddressee">The addressee the parcel is going to be delivered to</param>
         */
        public void AddParcel(string parcelPostcode, string parcelId, string parcelAddressee)
        {
            Parcel parcel = new Parcel();
            parcel.Postcode = parcelPostcode;
            parcel.Addressee = parcelAddressee;
            try
            {
                parcel.ParcelId = Convert.ToInt32(parcelId);
            }
            catch
            {
                displayToUI.ShowMessageBox("The parcel ID is not an int");
                return;
            }
            foreach (var courier in database.Couriers.Values)
            {
                if (courier.Parcels.Contains(parcel.ParcelId))
                {
                    displayToUI.ShowMessageBox("Each parcel needs to have a unique ID");
                    return;
                }
                if (courier.AddParcel(parcel))
                {
                    logger.LogAddParcel(parcel, courier);
                    return;
                }
            }
            displayToUI.ShowMessageBox("There are no available couriers to deliver to that postcode. The parcel has not been allocated, add a courier with the same delivery area and try again.");
        }

        /**
        * <summary>
        * Takes a list of strings containing information about each area and performs bubble sort algorithm on it
        * </summary>
        * 
        * <param name="areaInformation">A list of strings containing delivery information about each area</param>
        */
        public void SortList(ref List<string> areaInformation)
        {
            //Bubble sort
            for (int i = 0; i < areaInformation.Count; i++)
            {
                for (int n = 0; n < areaInformation.Count; n++)
                {
                    //Gets the area code, for example in string 'EH10 - 10 (Cycle) : 1/10'. Will return 10
                    if (Convert.ToInt32(areaInformation[i].Split(" ").First().Split("H").Last()) < Convert.ToInt32(areaInformation[n].Split(" ").First().Split("H").Last()))
                    {
                        string tempString = areaInformation[i];
                        areaInformation[i] = areaInformation[n];
                        areaInformation[n] = tempString;
                    }
                }
            }
        }

        /**
        * <summary>
        * Takes a courier and works out how many parcels they can carry
        * </summary>
        * 
        * <param name="courier">A courier</param>
        * 
        * <returns>The couriers capacity</returns>
        */
        public string GetListSize(Courier courier)
        {
            string parcelCapacity = "0";
            if (courier.CourierType() == "Cycle")
            {
                parcelCapacity = "10";
            }
            else if (courier.CourierType() == "Walking")
            {
                parcelCapacity = "5";
            }
            else if (courier.CourierType() == "Van")
            {
                parcelCapacity = "100";
            }
            return parcelCapacity;
        }

        /**
        * <summary>
        * Writes the information about all the areas to the list box
        * </summary>
        * 
        * <param name="lstBox">The list box from the UI</param>
        */
        public void WriteToListBox(ListBox lstBox)
        {
            List<string> areaInformationList = new List<string>();
            List<string> justTheAreas = new List<string>(); //List of all the areas that a courier is assigned to. Using list so I can make it distinct
            foreach (var courier in database.Couriers.Values)
            {
                foreach (string deliveryArea in courier.DeliveryArea.Split(" "))
                {
                    string areaInformationString = ConcatAreaInfo(courier, justTheAreas, deliveryArea);
                    areaInformationList.Add(areaInformationString);
                }
            }
            SortList(ref areaInformationList);
            foreach (string courierString in areaInformationList)
            {
                lstBox.Items.Add(courierString);
            }
            logger.LogAreaInfo(justTheAreas);
        }

        /**
        * <summary>
        * Concatenates all the information about a given area
        * </summary>
        * 
        * <param name="courier">A courier</param>
        * <param name="justTheAreas">List of all the areas that a courier is assigned to</param>
        * <param name="deliveryArea">A given delivery area</param>
        * 
        * <returns>The concatenated string containing information about an area</returns>
        */
        public string ConcatAreaInfo(Courier courier, List<string> justTheAreas, string deliveryArea)
        {
            string parcelCapacity = GetListSize(courier);

            string areaInformationString = deliveryArea + " - " + courier.CourierId + " (" +
                courier.CourierType() + ") : " + courier.Parcels.Count + "/" + parcelCapacity;
            justTheAreas.Add(deliveryArea);
            return areaInformationString;
        }

        /**
        * <summary>
        * Serializes the databse to a CSV file
        * </summary>
        */
        public void SerializeWrite()
        {
            database.SerializeWrite();
            logger.LogCSVWrite(database);
        }

        /**
        * <summary>
        * De-serializes the databse from the CSV file
        * </summary>
        */
        public void SerializerRead()
        {
            List<Courier> deserializedList = database.SerializeRead();

            foreach (var courier in deserializedList)
            {
                if (!database.AddCourier(courier))
                {
                    displayToUI.ShowMessageBox("Each courier must have a unique ID");
                    return;
                }
            }
            logger.LogCSVRead(database);
        }

        /**
        * <summary>
        * Identifies the parcels that the user wants to swap
        * </summary>
        * 
        * <param name="parcelIdToSwap">The ID of the parcel the user wishes to swap</param>
        */
        public void IdentifySwap(string parcelIdToSwapAsString)
        {
            int parcelIdToSwap = 0;
            try
            {
                parcelIdToSwap = Convert.ToInt32(parcelIdToSwapAsString);
            }
            catch
            {
                displayToUI.ShowMessageBox("The parcel ID is not an int");
                return;
            }
            Parcel parcelToSwap;
            foreach (var courier in database.Couriers.Values)
            {
                foreach (var parcel in courier.Parcels)
                {
                    if (parcel.ParcelId == parcelIdToSwap)
                    {
                        parcelToSwap = parcel;
                        int courierKey = courier.CourierId;

                        if (SwapCouriers(parcelToSwap, courierKey))
                        {
                            courier.Parcels.Remove(parcel);
                            return;
                        }
                        else
                        {
                            displayToUI.ShowMessageBox("There are no other couriers who can take this parcel. The parcel has not been swapped, add a courier with the same delivery area and try again.");
                            return;
                        }
                    }
                }
            }
            displayToUI.ShowMessageBox("There are no parcels that match that ID");
        }

        /**
        * <summary>
        * Swaps the parcels
        * </summary>
        * 
        * <param name="parcelToSwap">The parcel the user wishes to swap</param>
        * <param name="courierKey">The courier ID that the parcel belongs to</param>
        * 
        * <returns>Boolean return to indicate whether parcel has been succesfully swapped or not</returns>
        */
        public bool SwapCouriers(Parcel parcelToSwap, int courierKey)
        {
            foreach (var courier in database.Couriers.Values)
            {
                if (courier.CourierId != courierKey)
                {
                    if (courier.AddParcel(parcelToSwap))
                    {
                        logger.LogParcelTransfer(courierKey, courier.CourierId);
                        return true;
                    }
                }
            }
            return false;
        }

        /**
        * <summary>
        * Prints the details abour a courier
        * </summary>
        * 
        * <param name="courierId">The ID of the courier the user wantst details of</param>
        * <param name="lstBox">The list box from the UI</param>
        */
        public void DetailsForCourier(string courierIdAsString, ListBox lstBox)
        {
            int courierId = 0;
            try
            {
                courierId = Convert.ToInt32(courierIdAsString);
            }
            catch
            {
                displayToUI.ShowMessageBox("The courier ID is not an int");
                return;
            }
            foreach (var courier in database.Couriers.Values)
            {
                if(courier.CourierId == courierId)
                {
                    lstBox.Items.Add(courier.CourierInfo());
                    logger.LogPrintCourierInfo(courier);
                }
            }
        }
    }
}
