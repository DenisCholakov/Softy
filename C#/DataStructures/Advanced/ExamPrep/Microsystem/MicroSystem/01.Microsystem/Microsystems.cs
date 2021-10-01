namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Microsystems : IMicrosystem
    {
        private List<Computer> computers = new List<Computer>();
        private Dictionary<int, Computer> byId = new Dictionary<int, Computer>();
        private Dictionary<Brand, HashSet<Computer>> byBrand = new Dictionary<Brand, HashSet<Computer>>();
        private Dictionary<string, HashSet<Computer>> byColor = new Dictionary<string, HashSet<Computer>>();

        public void CreateComputer(Computer computer)
        {
            if (byId.ContainsKey(computer.Number))
            {
                throw new ArgumentException($"computer with nuumber {computer.Number} already exists");
            }

            byId[computer.Number] = computer;

            if (!byBrand.ContainsKey(computer.Brand))
            {
                byBrand.Add(computer.Brand, new HashSet<Computer>());
            }

            byBrand[computer.Brand].Add(computer);

            if (!byColor.ContainsKey(computer.Color))
            {
                byColor.Add(computer.Color, new HashSet<Computer>());
            }

            byColor[computer.Color].Add(computer);

            computers.Add(computer);
        }

        public bool Contains(int number)
        {
            return byId.ContainsKey(number);
        }

        public int Count()
        {
            return computers.Count;
        }

        public Computer GetComputer(int number)
        {
            ValudateNumber(number);

            return byId[number];
        }

        public void Remove(int number)
        {
            ValudateNumber(number);

            var computer = byId[number];

            computers.Remove(computer);
            byId.Remove(number);
            byBrand[computer.Brand].Remove(computer);

            if (byBrand[computer.Brand].Count == 0)
            {
                byBrand.Remove(computer.Brand);
            }

            byColor[computer.Color].Remove(computer);

            if (byColor[computer.Color].Count == 0)
            {
                byColor.Remove(computer.Color);
            }

        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!byBrand.ContainsKey(brand))
            {
                throw new ArgumentException($"There i9s no computers wirh this brand {brand}");
            }

            var toRemove = byBrand[brand];
            byBrand.Remove(brand);

            foreach (var computer in toRemove)
            {
                computers.Remove(computer);
                byId.Remove(computer.Number);
                byColor[computer.Color].Remove(computer);
            }
        }

        public void UpgradeRam(int ram, int number)
        {
            var computerToUpgrade = GetComputer(number);

            if (computerToUpgrade.RAM < ram)
            {
                computerToUpgrade.RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (!byBrand.ContainsKey(brand))
            {
                return Enumerable.Empty<Computer>();
            }

            return byBrand[brand].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            return computers.Where(c => c.ScreenSize == screenSize).OrderByDescending(c => c.Number);
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            if (!byColor.ContainsKey(color))
            {
                return Enumerable.Empty<Computer>();
            }

            return byColor[color].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            return computers.Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                        .OrderByDescending(c => c.Price);
        }

        private void ValudateNumber(int number)
        {
            if (!byId.ContainsKey(number))
            {
                throw new ArgumentException($"There is no computer with this number: {number}");
            }
        }
    }
}
