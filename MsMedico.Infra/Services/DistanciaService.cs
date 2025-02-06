using MsMedico.Core.Interfaces;
using Microsoft.Extensions.Configuration;
namespace MsMedico.Infra.Services
{
    public class DistanciaService : IDistanciaService
    {
        private readonly string _apiKey;

        public DistanciaService(IConfiguration configuration)
        {
            _apiKey = configuration["GoogleMaps:ApiKey"];       
        }

        public double ObterDistanciaHaversine(double latitudePontoA, double longitudePontoA, double latitudePontoB, double longitudePontoB)
        {
            double raioTerraKm = 6371; // Raio da Terra em km
            double latitude = (latitudePontoB - latitudePontoA) * Math.PI / 180;
            double longitude = (longitudePontoB - longitudePontoA) * Math.PI / 180;
            double distanciaAngular = Math.Sin(latitude / 2) * Math.Sin(latitude / 2) +
                       Math.Cos(latitudePontoA * Math.PI / 180) * Math.Cos(latitudePontoB * Math.PI / 180) *
                       Math.Sin(longitude / 2) * Math.Sin(longitude / 2);
            double arcoAngular = 2 * Math.Atan2(Math.Sqrt(distanciaAngular), Math.Sqrt(1 - distanciaAngular));
            double distance = raioTerraKm * arcoAngular;
            return Math.Round(distance, 2);
        }
    }
}
