﻿using CarRental.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Common.Interface
{
    public interface IVehicle
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string Maker { get; set; }
        public double Odometer { get; set; }
        public double PriceKm { get; set; }
        public double PriceDay => (int)VehicleType;
        public Vehicletypes VehicleType { get; set; }
        public VehicleStatuses Status{get; set;}
        void UpdateOdomoter(double km);
        void ChangeStatus(VehicleStatuses stat);        
    }
}
