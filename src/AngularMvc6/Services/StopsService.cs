using System;
using System.Collections.Generic;
using System.IO;
using AngularMvc6.Models;

namespace AngularMvc6.Services
{
    internal class StopsService : IStopsService
    {
        private DateTime _cacheLastModified;
        private IEnumerable<Stop> _cache;
        private readonly StopsServiceOptions _options;
        private static object _locker = new object();

        public StopsService(StopsServiceOptions options)
        {
            _options = options;
        }

        public IEnumerable<Stop> GetStops()
        {
            if (!IsActual())
            {
                _cacheLastModified = GetLastModified();
                lock (_locker)
                {
                    _cache = ReloadSourceData(_options.FilePath);
                }
            }
            return _cache;
        }

        public bool IsActual()
        {
            return DateTime.Compare(_cacheLastModified, GetLastModified()) >= 0;
        }

        public DateTime GetLastModified()
        {
            return File.GetLastWriteTime(_options.FilePath);
        }

        private IEnumerable<Stop> ReloadSourceData(string sourcePath)
        {
            var reader = new StreamReader(File.OpenRead(sourcePath));
            if (_options.HasHeader)
                reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;
                if (_options.HasQuotes)
                    line = line.Replace("\"", string.Empty);

                var fields = line.Split(_options.Delimiter);
                if (fields?.Length > 8)
                    yield return CreateStop(fields);
            }
        }

        private Stop CreateStop(string[] fields)
        {
            var stop = new Stop
            {
                Id = int.Parse(fields[0]),
                Name = fields[1],
                Description = fields[2],
                Url = fields[6]
            };

            try
            {
                stop.Latitude = double.Parse(fields[3]);
                stop.Longitude = double.Parse(fields[4]);
                stop.LocationType = int.Parse(fields[7]);

                if (!string.IsNullOrEmpty(fields[5]))
                {
                    stop.ZoneId = int.Parse(fields[5]);
                }

                if (!string.IsNullOrEmpty(fields[8]))
                {
                    stop.ParentStationId = int.Parse(fields[8]);
                }
            }
            catch (FormatException)
            {
                // TODO: error logging
            }
            return stop;
        }
    }
}
