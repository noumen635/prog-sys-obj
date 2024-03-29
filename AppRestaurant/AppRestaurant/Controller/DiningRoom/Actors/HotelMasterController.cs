﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Model.Common.Move;
using AppRestaurant.Model.DiningRoom.Actors;

namespace AppRestaurant.Controller.DiningRoom.Actors
{
    class HotelMasterController : IObserver<CustomerGroup>
    {
        private HotelMaster hotelMaster;
        private DiningRoomModel diningRoomModel;
        //private LineChiefController lineChiefController;

        private static LineChiefController lineChiefController;


        public HotelMasterController(DiningRoomModel diningRoomModel)
        {
            this.diningRoomModel = diningRoomModel;
            this.hotelMaster = diningRoomModel.HotelMaster;
        }
        

        public int[] CheckAvailableTables(CustomerGroup group, DiningRoomModel diningRoomModel)
        {
            int nbSquare = diningRoomModel.Squares.Count;
            for (int i = 0; i < nbSquare; i++)
            {
                int nbLine = diningRoomModel.Squares[i].Lines.Count;
                for (int j = 0; j < nbLine; j++)
                {
                    int nbTable = diningRoomModel.Squares[i].Lines[j].Tables.Count;
                    for (int k = 0; k < nbTable; k++)
                    {
                        if (group.Count < diningRoomModel.Squares[i].Lines[j].Tables[k].NbPlaces && diningRoomModel.Squares[i].Lines[j].Tables[k].State == EquipmentState.Available)
                        {
                            diningRoomModel.Squares[i].Lines[j].Tables[k].Group = group;
                            //return diningRoomModel.Squares[i].Lines[j].Tables[k];
                            return new int[3] { i, j, k };
                        }
                    }
                }
            }
            return null; 
        }

        public LineChief FindLineChief(CustomerGroup group, DiningRoomModel diningRoomModel)
        {
            foreach(LineChief lineChief in diningRoomModel.LineChiefs)
            {
                if (lineChief.Available)
                    return lineChief;
            }
            return null;
        }
        // pan defVal posX = 10, posY
        public void CallLineChief(LineChief lineChief, Position adjust)
        {
        }

        public void OnNext(CustomerGroup value)
        {
            Console.WriteLine("Maitre d'hotel: Combien etes vous ?");
            
            Console.WriteLine("Clients : " + value.Count+".");
            
            Console.WriteLine("Maitre d'hotel: Veuillez patienter s'il vous plait...");

            int[] table;

            table = this.CheckAvailableTables(value, this.diningRoomModel);

            if (table != null)
            {
                value.CustomerState = CustomerState.WaitLineChief;

                LineChief lineChief = this.FindLineChief(value, this.diningRoomModel);
                
                Console.WriteLine("Chef de rang: Oui");

                if (lineChief != null)
                {
                    Console.WriteLine("Chef de rang: Si vous voulez bien me suivre.");

                    lineChief.Available = false;
                    Position position = new Position(10, 0);
                    this.CallLineChief(lineChief, position);

                    lineChiefController = new LineChiefController(lineChief);
                    
                    lineChiefController.installClients(value, this.diningRoomModel.Squares[table[0]].Lines[table[1]].Tables[table[2]]);

                    //lineChiefController.setMenuCard(this.diningRoomModel.Squares[table[0]].Lines[table[1]].Tables[table[2]], diningRoomModel.MenuCards);

                    lineChiefController.LineChief.Available = true;

                    Console.WriteLine("=========Clients installé=========");

                    /*
                        lineChiefController.setMenuCard(this.diningRoomModel.Squares[table[0]].Lines[table[1]].Tables[table[2]], this.diningRoomModel.MenuCards);
                    

                        foreach (MenuCard menuCard in menuCards)
                        {
                            if (menuCard.State == EquipmentState.Available)
                            {
                                menuCard.State = EquipmentState.InUse;
                                table.MenuCard = menuCard;
                                return;
                            }
                        }
                    */
                    this.diningRoomModel.Squares[table[0]].Lines[table[1]].Tables[table[2]].MenuCard = this.diningRoomModel.MenuCards.Dequeue();

                    Console.WriteLine("=======Carte de menu deposée=======");

                }
            }
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
