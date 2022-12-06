namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
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

        //[Fact]
        //public void Merge_renovation() {
        //    var unitOfWork = SetupUOW();

        //    var room1 = new Room()
        //    {
        //        Id = 1,
        //        Capacity = 1,
        //    };

        //    var room2 = new Room()
        //    {
        //        Id = 2,
        //    };

        //    var equipmentRoom1 = new Equipment()
        //    {
        //        Id = 1,
        //        Room = room1,
        //        EquipmentType = EquipmentType.BED,
        //        Quantity = 10
        //    };

        //    var roomMap1 = new RoomMap()
        //    {
        //        X = 1,
        //        Z = 1,
        //        width = 1,
        //        depth = 1
        //    };

        //    var roomMap2 = new RoomMap()
        //    {
        //        X = 2,
        //        Z = 1,
        //        width = 1,
        //        depth = 1
        //    };

        //    var newRoom = new Room() {
        //        Id = 3,
        //    };


        //    var queue = new Queue<RoomMap>();
        //    queue.Enqueue(roomMap1);
        //    queue.Enqueue(roomMap2);
        //    queue.Enqueue(roomMap1);
        //    queue.Enqueue(roomMap2);

        //    RoomMap roomMap = null;
        //    Room newRm = null;
        //    unitOfWork.Setup(u => u.MapRepository.GetRoomMapById(It.IsAny<int>())).Returns(queue.Dequeue);

        //    unitOfWork.Setup(u => u.MapRepository.Create(It.IsAny<RoomMap>())).Callback((RoomMap map) =>
        //    {
        //        roomMap = map;
        //    });

        //    unitOfWork.Setup(unitOfWork => unitOfWork.RoomRepository.Create(It.IsAny<Room>())).Callback((Room room) =>
        //    {
        //        newRm = room;
        //    });




        //    Assert.NotNull(newRm);
        //    Assert.NotNull(roomMap);

        //}

        [Fact]
        public void Test_x_coordinate_when_merge_bigger_rooms() {

            var unitOfWork = SetupUOW();

            var room1 = new Room()
            {
                Id = 1,
                Capacity = 1,
            };

            var room2 = new Room()
            {
                Id = 2,
            };

            var roomMap1 = new RoomMap()
            {
                X = 1,
                Z = 1,
                width = 1,
                depth = 1
            };

            var roomMap2 = new RoomMap()
            {
                X = 2.5,
                Z = 1,
                width = 2,
                depth = 1
            };

            var queue = new Queue<RoomMap>();
            queue.Enqueue(roomMap1);
            queue.Enqueue(roomMap2);

            unitOfWork.Setup(u => u.MapRepository.GetRoomMapById(It.IsAny<int>())).Returns(queue.Dequeue);
            var renovationService = new RenovationService(unitOfWork.Object);

            //Act
            double res = renovationService.GetNewRoomX(room1, room2);

            Assert.Equal(2, res);
        }

        [Fact]
        public void Test_x_coordinate_when_merge_smaller_rooms()
        {

            var unitOfWork = SetupUOW();

            var room1 = new Room()
            {
                Id = 1,
                Capacity = 1,
            };

            var room2 = new Room()
            {
                Id = 2,
            };

            var roomMap1 = new RoomMap()
            {
                X = 1,
                Z = 1,
                width = 1,
                depth = 1
            };

            var roomMap2 = new RoomMap()
            {
                X = 1,
                Z = 1,
                width = 1,
                depth = 1
            };

            var queue = new Queue<RoomMap>();
            queue.Enqueue(roomMap1);
            queue.Enqueue(roomMap2);

            unitOfWork.Setup(u => u.MapRepository.GetRoomMapById(It.IsAny<int>())).Returns(queue.Dequeue);
            var renovationService = new RenovationService(unitOfWork.Object);

            //Act
            double res = renovationService.GetNewRoomX(room1, room2);

            Assert.Equal(1.5, res);
        }

        [Fact]
        public void Test_delete_appointments_after_renovation() {
            var unitOfWork = SetupUOW();

            List<Appointment> appointments = new List<Appointment>();
            var appointment1 = new Appointment() { 
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
            var equipment = new Equipment() {
                Id = 1,
                EquipmentType = EquipmentType.BED,
                Quantity = 20,
                ReservedQuantity = 7
            };
            var relocationRequest1 = new RelocationRequest()
            {
                Id = 1,
                Deleted = false,
                Quantity = 5,
                Equipment = equipment
            };
            var relocationRequest2 = new RelocationRequest()
            {
                Id = 2,
                Deleted = false,
                Quantity = 2,
                Equipment = equipment
            };
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
        public void Test_move_equipment_to_new_room() {
            var unitOfWork = SetupUOW();

            var room1 = new Room()
            {
                Id = 1,
            };
            var room2 = new Room()
            {
                Id = 2,
            };
            var newRoom = new Room()
            {
                Id = 3,
            };
            var equipment1 = new Equipment()
            {
                Id = 1,
                EquipmentType = EquipmentType.BED,
                Quantity = 20,
                ReservedQuantity = 7,
                Room = room1
            };
            var equipment2 = new Equipment()
            {
                Id = 2,
                EquipmentType = EquipmentType.BANDAGE,
                Quantity = 15,
                ReservedQuantity = 7,
                Room = room2
            };
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
        public void Test_creating_rooms_when_splitting_big_room() {
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


            Room newRoom1 = new Room()
            {
                Id = 2,
            };

            Room newRoom2 = new Room()
            {
                Id = 3,
            };


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
        public void Test_filter_equipment_when_moved() {

            var unitOfWork = SetupUOW();

            Room room = new Room()
            {
                Id = 1,
            };

            List<Equipment> equipment = new List<Equipment>();
            List<Equipment> sameEquipment = new List<Equipment>();

            Equipment e1 = new Equipment() { 
                Room = room,
                EquipmentType = EquipmentType.BED,
                Quantity = 10
            };
            Equipment e2 = new Equipment()
            {
                Room = room,
                EquipmentType = EquipmentType.BED,
                Quantity = 5
            };
            Equipment e3 = new Equipment()
            {
                Room = room,
                EquipmentType = EquipmentType.BANDAGE,
                Quantity = 7
            };
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
