namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using HospitalLibraryTest.InMemoryRepositories;
    using Moq;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RelocationUnitTest
    {

        public Mock<IUnitOfWork> SetupUOW()
        {
            var equipmentRepository = new Mock<IEquipmentRepository>();
            var relocationRepository = new Mock<IRelocationRepository>();

            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(u => u.EquipmentRepository).Returns(equipmentRepository.Object);
            unitOfWork.Setup(u => u.RelocationRepository).Returns(relocationRepository.Object);

            return unitOfWork;
        }


        [Fact]
        public void Is_First_Room_Available()
        {
            RoomScheduleService roomScheduleService = new RoomScheduleService(new InMemoryUnitOfWork());

            DateTime? dateTimes = roomScheduleService.IsRoomAvailable(1, new DateTime(2023, 3, 11, 14, 0, 0, 0), new DateTime(2023, 3, 11, 18, 0, 0));

            Assert.Null(dateTimes);
        }

        [Fact]
        public void Is_Second_Room_Available()
        {
            RoomScheduleService relocationService = new RoomScheduleService(new InMemoryUnitOfWork());

            DateTime? dateTimes = relocationService.IsRoomAvailable(2, new DateTime(2023, 3, 11, 14, 0, 0, 0), new DateTime(2023, 3, 11, 18, 0, 0));

            Assert.Null(dateTimes);
        }
        [Fact]
        public void Find_Available_Appointments_For_Both_Rooms()
        {
            RoomScheduleService roomScheduleService = new RoomScheduleService(new InMemoryUnitOfWork());

            List<DateTime> dateTimes = roomScheduleService.GetAvailableAppointments(new List<int>() { 1, 2 }, new DateTime(2023, 3, 11, 14, 0, 0, 0), new DateTime(2023, 3, 11, 18, 0, 0), 4);

            Assert.NotEmpty(dateTimes);
            Assert.Equal(dateTimes[0], new DateTime(2023, 3, 11, 14, 0, 0));
        }


        [Fact]
        public void Relocation_Successfull_When_Equipment_Exists_In_Room()
        {
            //Arrange
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
            room.SetCapacity(1);

            var fromRoom = Room.Create("002", floor, "ordinacija", wh);
            fromRoom.SetId(5);

            var toRoom = Room.Create("003", floor, "ordinacija", wh);
            toRoom.SetId(2);

            var equipment = Equipment.Create(EquipmentType.BED, 10, room);
            /*var equipment = new Equipment()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 10,
                Room = room
            };*/

            var equipment2 = Equipment.Create(EquipmentType.BED, 15, room);
            /*var equipment2 = new Equipment()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 15,
                Room = room
            };*/


            RelocationRequest request = RelocationRequest.Create(fromRoom, toRoom, equipment2, 2, DateTime.Now, 2);

            Equipment eqUpdate = null;
            RelocationRequest reqUpdate = null;

            unitOfWork.Setup(u => u.EquipmentRepository.GetEquipment(It.IsAny<EquipmentType>(), It.IsAny<Room>())).Returns(equipment);
            unitOfWork.Setup(u => u.EquipmentRepository.Update(It.IsAny<Equipment>())).Callback((Equipment eq) =>
            {
                eqUpdate = eq;
            });
            unitOfWork.Setup(u => u.EquipmentRepository.Save()).Returns(1);
            unitOfWork.Setup(u => u.RelocationRepository.Update(It.IsAny<RelocationRequest>())).Callback((RelocationRequest req) =>
            {
                reqUpdate = req;
            });

            unitOfWork.Setup(u => u.RelocationRepository.Save()).Returns(1);

            var relocationService = new RelocationService(unitOfWork.Object);

            //Act
            relocationService.RelocateEquipment(request);


            //Assert
            Assert.NotNull(eqUpdate);
            Assert.NotNull(reqUpdate);
            Assert.True(reqUpdate.Deleted);
            Assert.Equal(12, eqUpdate.Quantity);
            Assert.Equal(13, reqUpdate.Equipment.Quantity);
        }

        [Fact]
        public void Relocation_Successfull_When_Equipment_New_In_Room()
        {
            //Arrange
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
            room.SetCapacity(1);

            var fromRoom = Room.Create("002", floor, "ordinacija", wh);
            fromRoom.SetId(5);

            var toRoom = Room.Create("003", floor, "ordinacija", wh);
            toRoom.SetId(2);

            var equipment = Equipment.Create(EquipmentType.BED, 10, room);
            /*var equipment = new Equipment()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 10,
                Room = room
            };*/
            var createdEquipment = Equipment.Create(EquipmentType.BED, 2, toRoom);
            /*var createdEquipment = new Equipment()
            {
                Id = 3,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 2,
                Room = toRoom
            };*/

            var equipment2 = Equipment.Create(EquipmentType.BED, 15, room);
            /*var equipment2 = new Equipment()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 15,
                Room = room
            };*/

            RelocationRequest request = RelocationRequest.Create(fromRoom, toRoom, equipment2, 2, DateTime.Now, 2);

            Equipment retEq = null;
            RelocationRequest reqUpdate = null;

            unitOfWork.Setup(u => u.EquipmentRepository.GetEquipment(It.IsAny<EquipmentType>(), It.IsAny<Room>())).Returns(retEq);
            unitOfWork.Setup(u => u.EquipmentRepository.Create(It.IsAny<Equipment>())).Returns(createdEquipment);

            unitOfWork.Setup(u => u.RelocationRepository.Update(It.IsAny<RelocationRequest>())).Callback((RelocationRequest req) =>
            {
                reqUpdate = req;
            });

            unitOfWork.Setup(u => u.RelocationRepository.Save()).Returns(1);

            var relocationService = new RelocationService(unitOfWork.Object);

            //Act
            relocationService.RelocateEquipment(request);

            //Assert
            Assert.NotNull(createdEquipment);
            Assert.NotNull(reqUpdate);
            Assert.True(reqUpdate.Deleted);
            Assert.Equal(2, createdEquipment.Quantity);
            Assert.Equal(13, reqUpdate.Equipment.Quantity);
        }
    }
}
