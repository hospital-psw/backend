﻿namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
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
            RelocationService relocationService = new RelocationService(new InMemoryUnitOfWork());

            DateTime? dateTimes = relocationService.IsRoomAvailable(1, new DateTime(2022, 12, 11, 14, 0, 0, 0), new DateTime(2022, 12, 11, 18, 0, 0));

            Assert.Null(dateTimes);
        }

        [Fact]
        public void Is_Second_Room_Available()
        {
            RelocationService relocationService = new RelocationService(new InMemoryUnitOfWork());

            DateTime? dateTimes = relocationService.IsRoomAvailable(2, new DateTime(2022, 12, 11, 14, 0, 0, 0), new DateTime(2022, 12, 11, 18, 0, 0));

            Assert.Null(dateTimes);
        }
        [Fact]
        public void Find_Available_Appointments_For_Both_Rooms()
        {
            RelocationService relocationService = new RelocationService(new InMemoryUnitOfWork());

            List<DateTime> dateTimes = relocationService.GetAvailableAppointments(1, 2, new DateTime(2022, 12, 11, 14, 0, 0, 0), new DateTime(2022, 12, 11, 18, 0, 0), 4);

            Assert.NotEmpty(dateTimes);
            Assert.Equal(dateTimes[0], new DateTime(2022, 12, 11, 14, 0, 0));
        }


        [Fact]
        public void Relocation_Successfull_When_Equipment_Exists_In_Room()
        {
            //Arrange
            var unitOfWork = SetupUOW();

            var room = new Room()
            {
                Id = 1,
                Capacity = 1,
            };

            var toRoom = new Room()
            {
                Id = 2
            };

            var equipment = new Equipment()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 10,
                Room = room
            };


            RelocationRequest request = new RelocationRequest()
            {
                Id = 1,
                Quantity = 2,
                Equipment = equipment,
                ToRoom = toRoom

            };


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
        }

        [Fact]
        public void Relocation_Successfull_When_Equipment_New_In_Room()
        {
            //Arrange
            var unitOfWork = SetupUOW();

            var room = new Room()
            {
                Id = 1,
                Capacity = 1,
            };

            var toRoom = new Room()
            {
                Id = 2
            };

            var equipment = new Equipment()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 10,
                Room = room
            };

            var createdEquipment = new Equipment() {
                Id = 3,
                Deleted = false,
                EquipmentType = EquipmentType.BED,
                Quantity = 2,
                Room = toRoom
            };

          

            RelocationRequest request = new RelocationRequest()
            {
                Id = 1,
                Quantity = 2,
                Equipment = equipment,
                ToRoom = toRoom

            };

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
        }
    }
}
