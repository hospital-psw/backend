namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository;
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
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var equipment = SetUpEquipment(10, 1);
            RelocationRequest request = SetUpRelocationRequest();
            Equipment eqUpdate = null;
            unitOfWork.Setup(u => u.EquipmentRepository.GetEquipment(It.IsAny<EquipmentType>(), It.IsAny<Room>())).Returns(equipment);
            unitOfWork.Setup(u => u.EquipmentRepository.Update(It.IsAny<Equipment>())).Callback((Equipment eq) =>
            {
                eqUpdate = eq;
            });

            //Act
            RelocationRequest reqUpdate = Relocate(unitOfWork, request);

            //Assert
            Assert.True(IsEquipmentCorrect(eqUpdate,12));
            Assert.True(IsRequestCorrect(reqUpdate,13));
        }

        [Fact]
        public void Relocation_Successfull_When_Equipment_New_In_Room()
        {
            //Arrange
            var unitOfWork = SetupUOW();
            var equipment = SetUpEquipment(2, 2);
            RelocationRequest request = SetUpRelocationRequest();
            unitOfWork.Setup(u => u.EquipmentRepository.Create(It.IsAny<Equipment>())).Returns(equipment);
            
            //Act
            RelocationRequest reqUpdate = Relocate(unitOfWork,request);

            //Assert
            Assert.True(IsEquipmentCorrect(equipment, 2));
            Assert.True(IsRequestCorrect(reqUpdate, 13));
        }

        private WorkingHours SetUpWorkingHours() 
        { 
            return new WorkingHours()
                {
                    Start = new DateTime(),
                    End = new DateTime(1, 1, 1, 23, 0, 0)
                };
        }

        private Floor SetUpFloor()
        { 
            return  new Floor()
            {
                Building = new Building()
                {
                    Address = "Jovana Piperovica 14",
                    Name = "Radosno detinjstvo"
                },
                Number = FloorNumber.Create(69),
                Purpose = "Krematorijum"
            };
        }

        private Room SetUpRoom(int id)
        {   
            WorkingHours workingHours = SetUpWorkingHours();
            Floor floor = SetUpFloor();
            var room = Room.Create("001", floor, "ordinacija", workingHours);
            room.SetId(id);
            room.SetCapacity(1);
            return room;
        }

        private Equipment SetUpEquipment(int quantity, int roomId)
        {
            Room room = SetUpRoom(roomId);
            return Equipment.Create(EquipmentType.BED, quantity, room);
        }

        private RelocationRequest SetUpRelocationRequest()
        {
            Room fromRoom = SetUpRoom(5);
            Room toRoom = SetUpRoom(2);
            Equipment equipment = SetUpEquipment(15, 1);
            return  RelocationRequest.Create(fromRoom, toRoom, equipment, 2, DateTime.Now, 2);
        }

        private RelocationRequest Relocate(Mock<IUnitOfWork> unitOfWork,RelocationRequest request)
        {
            RelocationRequest reqUpdate = null;
            unitOfWork.Setup(u => u.EquipmentRepository.Save()).Returns(1);
            unitOfWork.Setup(u => u.RelocationRepository.Update(It.IsAny<RelocationRequest>())).Callback((RelocationRequest req) =>
            {
                reqUpdate = req;
            });

            unitOfWork.Setup(u => u.RelocationRepository.Save()).Returns(1);
            
            var relocationService = new RelocationService(unitOfWork.Object);
            relocationService.RelocateEquipment(request);


            return reqUpdate;
        }

        private bool IsEquipmentCorrect(Equipment equipment,int quantity)
        { 
            if(equipment == null || equipment.Quantity != quantity)
            { 
                return false; 
            }
            return true;
        }

        private bool IsRequestCorrect(RelocationRequest request, int quantity)
        {
            if (request != null && request.Deleted == true && request.Equipment.Quantity == quantity)
            {
                return true;
            }
            return false;
        }
    }
}
