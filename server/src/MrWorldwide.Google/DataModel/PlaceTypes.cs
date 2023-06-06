﻿using System.Collections.Immutable;
using System.Reflection;

namespace MrWorldwide.Google.DataModel;

public readonly record struct PlaceType(string Name, string TypeId);

public static class PlaceTypes
{
    public static readonly PlaceType AccountingOffice = new("Accounting Office", "accounting");
    public static readonly PlaceType Airport = new("AAirport", "airport");
    public static readonly PlaceType AmusementPark = new("Amusement Park", "amusement_park");
    public static readonly PlaceType Aquarium = new("Aquarium", "aquarium");
    public static readonly PlaceType ArtGallery = new("Art Gallery", "art_gallery");
    public static readonly PlaceType Atm = new("ATM", "atm");
    public static readonly PlaceType Bakery = new("Bakery", "bakery");
    public static readonly PlaceType Bank = new("Bank", "bank");
    public static readonly PlaceType Bar = new("Bar", "bar");
    public static readonly PlaceType BeautySalon = new("Beauty Salon", "beauty_salon");
    public static readonly PlaceType BicycleStore = new("Bicycle Store", "bicycle_store");
    public static readonly PlaceType BookStore = new("Book Store", "book_store");
    public static readonly PlaceType BowlingAlley = new("Bowling Alley", "bowling_alley");
    public static readonly PlaceType BusStation = new("Bus Station", "bus_station");
    public static readonly PlaceType Cafe = new("Cafe", "cafe");
    public static readonly PlaceType Campground = new("Campground", "campground");
    public static readonly PlaceType CarDealer = new("CarDealer", "car_dealer");
    public static readonly PlaceType CarRental = new("Car Rental", "car_rental");
    public static readonly PlaceType CarRepair = new("Car Repair", "car_repair");
    public static readonly PlaceType CarWash = new("Car Wash", "car_wash");
    public static readonly PlaceType Casino = new("Casino", "casino");
    public static readonly PlaceType Cemetery = new("Cemetery", "cemetery");
    public static readonly PlaceType Church = new("Church", "church");
    public static readonly PlaceType CityHall = new("CityHall", "city_hall");
    public static readonly PlaceType ClothingStore = new("Clothing Store", "clothing_store");
    public static readonly PlaceType ConvenienceStore = new("Convenience Store", "convenience_store");
    public static readonly PlaceType Courthouse = new("Courthouse", "courthouse");
    public static readonly PlaceType Dentist = new("Dentist", "dentist");
    public static readonly PlaceType DepartmentStore = new("Department Store", "department_store");
    public static readonly PlaceType Doctor = new("Doctor", "doctor");
    public static readonly PlaceType DrugStore = new("Drug Store", "drugstore");
    public static readonly PlaceType Electrician = new("Electrician", "electrician");
    public static readonly PlaceType ElectronicsStore = new("Electronics Store", "electronics_store");
    public static readonly PlaceType Embassy = new("Embassy", "embassy");
    public static readonly PlaceType FireStation = new("Fire Station", "fire_station");
    public static readonly PlaceType Florist = new("Florist", "florist");
    public static readonly PlaceType FuneralHome = new("Funeral Home", "funeral_home");
    public static readonly PlaceType FurnitureStore = new("Furniture Store", "furniture_store");
    public static readonly PlaceType GasStation = new("Gas Station", "gas_station");
    public static readonly PlaceType Gym = new("Gym", "gym");
    public static readonly PlaceType HairCare = new("Hair Care", "hair_care");
    public static readonly PlaceType HardwareStore = new("Hardware Store", "hardware_store");
    public static readonly PlaceType HinduTemple = new("Hindu Temple", "hindu_temple");
    public static readonly PlaceType HomeGoodsStore = new("Home Goods Store", "home_goods_store");
    public static readonly PlaceType Hospital = new("Hospital", "hospital");
    public static readonly PlaceType InsuranceAgency = new("Insurance Agency", "insurance_agency");
    public static readonly PlaceType JewelryStore = new("Jewelry Store", "jewelry_store");
    public static readonly PlaceType Laundry = new("Laundry", "laundry");
    public static readonly PlaceType Lawyer = new("Lawyer", "lawyer");
    public static readonly PlaceType Library = new("Library", "library");
    public static readonly PlaceType LightRailStation = new("Light Rail Station", "light_rail_station");
    public static readonly PlaceType LiquorStore = new("Liquor Store", "liquor_store");
    public static readonly PlaceType LocalGovernmentOffice = new("Local Government Office", "local_government_office");
    public static readonly PlaceType Locksmith = new("Locksmith", "locksmith");
    public static readonly PlaceType Lodging = new("Lodging", "lodging");
    public static readonly PlaceType MealDelivery = new("Meal Delivery", "meal_delivery");
    public static readonly PlaceType MealTakeaway = new("Meal Takeaway", "meal_takeaway");
    public static readonly PlaceType Mosque = new("Mosque", "mosque");
    public static readonly PlaceType MovieRental = new("Movie Rental", "movie_rental");
    public static readonly PlaceType MovieTheater = new("Movie Theater", "movie_theater");
    public static readonly PlaceType MovingCompany = new("Moving Company", "moving_company");
    public static readonly PlaceType Museum = new("Museum", "museum");
    public static readonly PlaceType NightClub = new("Night Club", "night_club");
    public static readonly PlaceType Painter = new("Painter", "painter");
    public static readonly PlaceType Park = new("Park", "park");
    public static readonly PlaceType Parking = new("Parking", "parking");
    public static readonly PlaceType PetStore = new("Pet Store", "pet_store");
    public static readonly PlaceType Pharmacy = new("Pharmacy", "pharmacy");
    public static readonly PlaceType PhysioTherapist = new("PhysioTherapist", "physiotherapist");
    public static readonly PlaceType Plumber = new("Plumber", "plumber");
    public static readonly PlaceType Police = new("Police", "police");
    public static readonly PlaceType PostOffice = new("PostOffice", "post_office");
    public static readonly PlaceType PrimarySchool = new("Primary School", "primary_school");
    public static readonly PlaceType RealEstateAgency = new("Real Estate Agency", "real_estate_agency");
    public static readonly PlaceType RoofingContractor = new("Roofing Contractor", "roofing_contractor");
    public static readonly PlaceType RvPark = new("RV Park", "rv_park");
    public static readonly PlaceType School = new("School", "school");
    public static readonly PlaceType SecondarySchool = new("Secondary School", "secondary_school");
    public static readonly PlaceType ShoeStore = new("Shoe Store", "shoe_store");
    public static readonly PlaceType ShoppingMall = new("Shopping Mall", "shopping_mall");
    public static readonly PlaceType Spa = new("Spa", "spa");
    public static readonly PlaceType Stadium = new("Stadium", "stadium");
    public static readonly PlaceType Storage = new("Storage", "storage");
    public static readonly PlaceType Store = new("Store", "store");
    public static readonly PlaceType SubwayStation = new("Subway Station", "subway_station");
    public static readonly PlaceType Supermarket = new("Supermarket", "supermarket");
    public static readonly PlaceType Synagogue = new("Synagogue", "synagogue");
    public static readonly PlaceType TaxiStand = new("Taxi Stand", "taxi_stand");
    public static readonly PlaceType TouristAttraction = new("TouristAttraction", "tourist_attraction");
    public static readonly PlaceType TrainStation = new("Train Station", "train_station");
    public static readonly PlaceType TransitStation = new("TransitStation", "transit_station");
    public static readonly PlaceType TravelAgency = new("Travel Agency", "travel_agency");
    public static readonly PlaceType University = new("University", "university");
    public static readonly PlaceType VeterinaryCare = new("Veterinary Care", "veterinary_care");
    public static readonly PlaceType Zoo = new("Zoo", "zoo");

    public static ImmutableHashSet<PlaceType> All { get; } = typeof(PlaceTypes)
        .GetFields(BindingFlags.Static)
        .Where(x=>x.FieldType == typeof(PlaceType))
        .Select(x=>x.GetValue(null))
        .Cast<PlaceType>()
        .ToImmutableHashSet();

    public static ImmutableHashSet<string> TypeIds { get; } = All.Select(x => x.TypeId).ToImmutableHashSet();
}