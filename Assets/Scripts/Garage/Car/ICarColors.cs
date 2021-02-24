using UnityEngine;
namespace Garage.Car
{
    public interface ICarColors
    {
        Color[] GetCarColors();
        void UpdateCarColor(int index);
    }
}