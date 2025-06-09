using SmartDrones.Domain.Entities;
using SmartDrones.Domain.Enums;
using System;
using System.Linq;

namespace SmartDrones.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(SmartDronesDbContext context)
        {
            if (context.Drones.Any())
            {
                return;
            }

            var drone1 = new Drone("DRONE-SP-001", "DJI Phantom 4 Pro");
            drone1.UpdateStatus("Online");
            var drone2 = new Drone("DRONE-RJ-002", "Autel Evo Lite+");
            drone2.UpdateStatus("In Mission");
            var drone3 = new Drone("DRONE-MG-003", "Skydio 2+");
            drone3.UpdateStatus("Offline");

            context.Drones.AddRange(drone1, drone2, drone3);
            context.SaveChanges();

            context.SensorData.AddRange(
                new SensorData(drone1.Id, 26.3, 62.1, 550.0, false, -23.55052, -46.633309),
                new SensorData(drone1.Id, 31.8, 58.0, 720.0, true, -23.56135, -46.65600),
                new SensorData(drone1.Id, 25.0, 65.5, 480.0, false, -23.5489, -46.6388)
            );

            context.SensorData.AddRange(
                new SensorData(drone2.Id, 29.5, 75.2, 650.0, true, -22.906847, -43.172897),
                new SensorData(drone2.Id, 28.0, 70.0, 580.0, false, -22.9519, -43.2105)
            );

            context.SensorData.AddRange(
                new SensorData(drone3.Id, 22.1, 58.9, 400.0, false, -19.916667, -43.933333),
                new SensorData(drone3.Id, 24.5, 60.0, 450.0, false, -19.9200, -43.9500)
            );
            context.SaveChanges();

            context.Alerts.AddRange(
                new Alert(drone1.Id, "Possível foco de incêndio na região central de São Paulo. Nível de fumaça elevado.", RiskLevel.High, -23.56135, -46.65600),
                new Alert(drone2.Id, "Relato de grande aglomeração e detecção de fumaça na área da Lapa, Rio de Janeiro.", RiskLevel.Critical, -22.906847, -43.172897),
                new Alert(drone3.Id, "Baixa luminosidade e umidade moderada em área de mata em Minas Gerais. Monitoramento contínuo.", RiskLevel.Low, -19.9200, -43.9500)
            );
            context.SaveChanges();
        }
    }
}