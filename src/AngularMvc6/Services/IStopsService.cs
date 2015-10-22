using System;
using System.Collections.Generic;
using AngularMvc6.Models;

namespace AngularMvc6.Services
{
    public interface IStopsService
    {
        IEnumerable<Stop> GetStops();
        bool IsActual();
        DateTime GetLastModified();
    }
}