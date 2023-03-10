namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using Moq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    public class RenovationUnitTest
    {
        public Mock<IUnitOfWork> SetupUOW()
        {
            var equipmentRepository = new Mock<IEquipmentRepository>();
            var renovationRepository = new Mock<IRenovationRepository>();
            var roomRepository = new Mock<IRoomRepository>();
            var mapRepository = new Mock<IMapRepository>();
            var relocationRepository = new Mock<IRelocationRepository>();
            var appointmentRepository = new Mock<IAppointmentRepository>();

            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(u => u.EquipmentRepository).Returns(equipmentRepository.Object);
            unitOfWork.Setup(u => u.RenovationRepository).Returns(renovationRepository.Object);
            unitOfWork.Setup(u => u.RoomRepository).Returns(roomRepository.Object);
            unitOfWork.Setup(u => u.MapRepository).Returns(mapRepository.Object);
            unitOfWork.Setup(u => u.RelocationRepository).Returns(relocationRepository.Object);
            unitOfWork.Setup(u => u.AppointmentRepository).Returns(appointmentRepository.Object);

            return unitOfWork;
        }

        private void SetUpRooms(Room room1, Room room2)
        {
            Floor floor = new Floor()
            {
                Building = new Building()
                {
                    Address = "Jovana Piperovica 14",
                    Name = "Radosno detinjstvo"
                },
                Number = FloorNumber.Create(69),
                Purpose = "Krematorijum"
            };

            WorkingHours wh = new WorkingHours()
            {
                Start = new DateTime(),
                End = new DateTime(1, 1, 1, 23, 0, 0)
            };
            room1 = Room.Create("001", floor, "ordinacija", wh);
            room1.SetId(1);
            room1.SetCapacity(1);

            room2 = Room.Create("001", floor, "ordinacija", wh);
            room2.SetId(2);

        }
        private RenovationService SetUpRoomMaps(double x, double z, double width, double depth)
        {
            var unitOfWork = SetupUOW();
            var roomMap1 = new RoomMap()
            {
                X = 1,
                Z = 1,
                width = 1,
                depth = 1
            };

            var roomMap2 = new RoomMap()
            {
                X = x,
                Z = z,
                width = width,
                depth = depth
            };

            var queue = new Queue<RoomMap>();
            queue.Enqueue(roomMap1);
            queue.Enqueue(roomMap2);

            unitOfWork.Setup(u => u.MapRepository.GetRoomMapById(It.IsAny<int>())).Returns(queue.Dequeue);
            var renovationService = new RenovationService(unitOfWork.Object);
            return renovationService;
        }

        [Fact]
        public void Test_x_coordinate_when_merge_bigger_rooms()
        {

            Room room1 = new Room();
            Room room2 = new Room();
            SetUpRooms(room1, room2);
            RenovationService renovationService = SetUpRoomMaps(2.5,1,2,1);

            //Act
            double res = renovationService.GetNewRoomX(room1, room2);

            Assert.Equal(2, res);
        }

        [Fact]
        public void Test_x_coordinate_when_merge_smaller_rooms()
        {

            Room room1 = new Room();
            Room room2 = new Room();
            SetUpRooms(room1, room2);
            RenovationService renovationService = SetUpRoomMaps(1, 1, 1, 1);

            //Act
            double res = renovationService.GetNewRoomX(room1, room2);

            Assert.Equal(1.5, res);
        }

        [Fact]
        public void Test_delete_appointments_after_renovation()
        {
            var unitOfWork = SetupUOW();

            List<Appointment> appointments = new List<Appointment>();
            var appointment1 = new Appointment()
            {
                Id = 1,
                Deleted = false,
            };
            var appointment2 = new Appointment()
            {
                Id = 2,
                Deleted = false,
            };
            appointments.Add(appointment1);
            appointments.Add(appointment2);
            var renovationService = new RenovationService(unitOfWork.Object);
            List<Appointment> updatedAppointments = new List<Appointment>();
            unitOfWork.Setup(unitOfWork => unitOfWork.AppointmentRepository.Update(It.IsAny<Appointment>())).Callback((Appointment appointment) =>
            {
                updatedAppointments.Add(appointment);
            });

            renovationService.DeleteAppointmentsAfterRenovation(appointments);

            Assert.True(updatedAppointments[0].Deleted);
            Assert.True(updatedAppointments[1].Deleted);
        }

        [Fact]
        public void Test_delete_relocations_after_renovation()
        {
            var unitOfWork = SetupUOW();

            List<RelocationRequest> relocations = new List<RelocationRequest>();
            var equipment = Equipment.CreateWithReservedQuantity(EquipmentType.BED, 20, null, 7);
            equipment.Id = 1;
            Room fromRoom = Room.Create("101", null, "", null);
            fromRoom.Id = 1;
            Room toRoom = Room.Create("200", null, "", null);
            fromRoom.Id = 2;


            var relocationRequest1 = RelocationRequest.Create(fromRoom, toRoom, equipment, 5, DateTime.Now.AddDays(1), 1);
            relocationRequest1.Deleted = false;

            var relocationRequest2 = RelocationRequest.Create(fromRoom, toRoom, equipment, 2, DateTime.Now.AddDays(1), 1);
            relocationRequest2.Deleted = false;

            relocations.Add(relocationRequest1);
            relocations.Add(relocationRequest2);
            var renovationService = new RenovationService(unitOfWork.Object);
            List<RelocationRequest> updatedRelocations = new List<RelocationRequest>();
            unitOfWork.Setup(unitOfWork => unitOfWork.RelocationRepository.Update(It.IsAny<RelocationRequest>())).Callback((RelocationRequest request) =>
            {
                updatedRelocations.Add(request);
            });

            renovationService.DeleteRelocationsAfterRenovation(relocations);

            Assert.Equal(0, equipment.ReservedQuantity);
            Assert.True(updatedRelocations[0].Deleted);
            Assert.True(updatedRelocations[1].Deleted);
        }

        [Fact]
        public void Test_move_equipment_to_new_room()
        {
            var unitOfWork = SetupUOW();

            Floor floor = new Floor()
            {
                Building = new Building()
                {
                    Address = "Jovana Piperovica 14",
                    Name = "Radosno detinjstvo"
                },
                Number = FloorNumber.Create(69),
                Purpose = "Krematorijum"
            };

            WorkingHours wh = new WorkingHours()
            {
                Start = new DateTime(),
                End = new DateTime(1, 1, 1, 23, 0, 0)
            };
            var room1 = Room.Create("001", floor, "ordinacija", wh);
            room1.SetId(1);

            var room2 = Room.Create("001", floor, "ordinacija", wh);
            room2.SetId(2);

            var newRoom = Room.Create("001", floor, "ordinacija", wh);
            newRoom.SetId(3);

            var equipment1 = Equipment.CreateWithReservedQuantity(EquipmentType.BED, 20, room1, 7);
            equipment1.Id = 1;
            var equipment2 = Equipment.CreateWithReservedQuantity(EquipmentType.BANDAGE, 15, room2, 7);
            equipment2.Id = 2;
            List<Equipment> equipmentToMove = new List<Equipment>();
            equipmentToMove.Add(equipment1);
            equipmentToMove.Add(equipment2);

            List<Equipment> movedEquipment = new List<Equipment>();
            unitOfWork.Setup(unitOfWork => unitOfWork.EquipmentRepository.Update(It.IsAny<Equipment>())).Callback((Equipment equipment) =>
            {
                movedEquipment.Add(equipment);
            });
            var renovationService = new RenovationService(unitOfWork.Object);
            renovationService.MoveEquipmentToNewRoom(equipmentToMove, newRoom);

            Assert.Equal(newRoom, equipment1.Room);
            Assert.Equal(newRoom, equipment2.Room);
        }

        [Fact]
        public void Test_creating_rooms_when_splitting_big_room()
        {
            var unitOfWork = SetupUOW();

            RoomMap roomMap = new RoomMap()
            {
                Id = 1,
                X = 2,
                width = 3,
                Z = 1,
                depth = 1,
                Deleted = false
            };


            RoomMap newRoomMap1 = new RoomMap()
            {
                Id = 2,
                X = 1,
                width = 1,
                Z = 1,
                depth = 1
            };

            RoomMap newRoomMap2 = new RoomMap()
            {
                Id = 3,
                X = 2.5,
                width = 2,
                Z = 1,
                depth = 1
            };

            Floor floor = new Floor()
            {
                Building = new Building()
                {
                    Address = "Jovana Piperovica 14",
                    Name = "Radosno detinjstvo"
                },
                Number = FloorNumber.Create(69),
                Purpose = "Krematorijum"
            };

            WorkingHours wh = new WorkingHours()
            {
                Start = new DateTime(),
                End = new DateTime(1, 1, 1, 23, 0, 0)
            };
            var newRoom1 = Room.Create("001", floor, "ordinacija", wh);
            newRoom1.SetId(2);

            var newRoom2 = Room.Create("001", floor, "ordinacija", wh);
            newRoom2.SetId(3);


            var queue = new Queue<RoomMap>();
            queue.Enqueue(newRoomMap1);
            queue.Enqueue(newRoomMap2);


            unitOfWork.Setup(u => u.MapRepository.Create(It.IsAny<RoomMap>())).Returns(queue.Dequeue);
            unitOfWork.Setup(unitOfWork => unitOfWork.MapRepository.Update(It.IsAny<RoomMap>())).Callback((RoomMap rMap) =>
            {
                roomMap = rMap;
            });

            var renovationService = new RenovationService(unitOfWork.Object);
            renovationService.CreateNewRoomMap(roomMap, newRoom1, newRoom2);

            Assert.True(roomMap.Deleted);
        }

        [Fact]
        public void Test_filter_equipment_when_moved()
        {

            var unitOfWork = SetupUOW();

            Floor floor = new Floor()
            {
                Building = new Building()
                {
                    Address = "Jovana Piperovica 14",
                    Name = "Radosno detinjstvo"
                },
                Number = FloorNumber.Create(69),
                Purpose = "Krematorijum"
            };

            WorkingHours wh = new WorkingHours()
            {
                Start = new DateTime(),
                End = new DateTime(1, 1, 1, 23, 0, 0)
            };
            var room = Room.Create("001", floor, "ordinacija", wh);
            room.SetId(1);


            List<Equipment> equipment = new List<Equipment>();
            List<Equipment> sameEquipment = new List<Equipment>();

            Equipment e1 = Equipment.CreateWithReservedQuantity(EquipmentType.BED, 10, room, 0);
            Equipment e2 = Equipment.CreateWithReservedQuantity(EquipmentType.BED, 5, room, 0);
            Equipment e3 = Equipment.CreateWithReservedQuantity(EquipmentType.BANDAGE, 7, room, 0);

            equipment.Add(e1);
            equipment.Add(e2);
            equipment.Add(e3);

            sameEquipment.Add(e1);
            sameEquipment.Add(e2);

            unitOfWork.Setup(u => u.EquipmentRepository.GetEquipmentForRoom(It.IsAny<Room>())).Returns(equipment);
            unitOfWork.Setup(u => u.EquipmentRepository.GetSameEquipmentInRoom(It.IsAny<Room>(), It.IsAny<EquipmentType>())).Returns(sameEquipment);


            List<Equipment> resultEquipment = new List<Equipment>();
            unitOfWork.Setup(unitOfWork => unitOfWork.EquipmentRepository.Update(It.IsAny<Equipment>())).Callback((Equipment equipment) =>
            {
                resultEquipment.Add(equipment);
            });

            var renovationService = new RenovationService(unitOfWork.Object);
            renovationService.FilterEquipmentForMove(room);

            Assert.Equal(15, e1.Quantity);
            Assert.True(e2.Deleted);
        }
    }
}
