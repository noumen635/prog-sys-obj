﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Model.DiningRoom.Factory;
using AppRestaurant.Controller.DiningRoom.Actors;

namespace AppRestaurant.Controller.DiningRoom
{
    class DiningRoomController
    {
        private HotelMasterController hotelMasterController;

        private List<CustomerController> customerControllers;
        private List<LineChiefController> lineChiefControllers;
        private List<RoomClerkController> roomClerkControllers;
        private List<WaiterController> waiterControllers;

        static Queue<Customer> CustomerQueue = new Queue<Customer>();


        public DiningRoomController(DiningRoomModel diningRoomModel)
        {
            this.hotelMasterController = new HotelMasterController(diningRoomModel);
            this.lineChiefControllers = new List<LineChiefController>();
            this.roomClerkControllers = new List<RoomClerkController>();
            this.waiterControllers = new List<WaiterController>();
        }
        public void Run()
        {
            CustomersFactory factory = new CustomersFactory();
            
            installCustomers(factory);
        }

        public void installCustomers(CustomersFactory factory)
        {
            factory.Subscribe(hotelMasterController);

            for (int i = 0; i < 5; i++)
            {
                CustomerQueue.Enqueue(factory.CreateCustomers(4));
            }
        }

    }
}
