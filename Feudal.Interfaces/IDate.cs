﻿namespace Feudal.Interfaces;

public interface IDate
{
    int Year { get; }
    int Month { get; }
    int Day { get; }

    void OnDaysInc();
}
