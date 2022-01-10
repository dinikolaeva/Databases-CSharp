namespace CarDealer
{
    using AutoMapper;
    using CarDealer.DTO;
    using CarDealer.Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SuppliersDTO, Supplier>();
            CreateMap<PartsDTO, Part>();
            CreateMap<CarsDTO, Car>();
            CreateMap<CustomersDTO, Customer>();
            CreateMap<SalesDTO, Sale>();
        }
    }
}
