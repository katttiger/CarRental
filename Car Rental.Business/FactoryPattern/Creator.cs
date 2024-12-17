using CarRental.Common.Classes;
using CarRental.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.FactoryPattern
{
    public abstract class Creator
    {
        public abstract IVehicle FactoryMethod();

        public IVehicle SomeOperation()
        {
            IVehicle product = FactoryMethod();
            return product;
        }

    }
    class MotorcycleCreator : Creator
    {
        public override IVehicle FactoryMethod()
        {
            return new Motorcycle();
        }
    }

    class CarCreator : Creator
    {
        public override IVehicle FactoryMethod()
        {
            return new Car();
        }
    }
}
