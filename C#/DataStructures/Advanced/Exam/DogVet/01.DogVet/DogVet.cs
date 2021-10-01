namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById = new Dictionary<string, Dog>();
        private Dictionary<string, Owner> ownersById = new Dictionary<string, Owner>();
        private Dictionary<Breed, HashSet<Dog>> dogsByBreed = new Dictionary<Breed, HashSet<Dog>>();
        private Dictionary<int, HashSet<Dog>> dogsByAge = new Dictionary<int, HashSet<Dog>>();
        private SortedSet<int> dogsAge = new SortedSet<int>();
        private SortedSet<Dog> sortedDogsByAgeNameAndOwnerName = new SortedSet<Dog>();


        public int Size => dogsById.Count;

        public void AddDog(Dog dog, Owner owner)
        {
            if (dogsById.ContainsKey(dog.Id))
            {
                throw new ArgumentException($"There is an existing dog with id: {dog.Id}");
            }

            if (!ownersById.ContainsKey(owner.Id))
            {
                ownersById.Add(owner.Id, owner);
            }

            if (owner.dogs.ContainsKey(dog.Name))
            {
                throw new ArgumentException($"This owner already has a dog with name: {dog.Name}");
            }

            dog.Owner = owner;

            ownersById[owner.Id].dogs.Add(dog.Name, dog);
            dogsById.Add(dog.Id, dog);
            sortedDogsByAgeNameAndOwnerName.Add(dog);

            AddToDogDictionaries(dog);

            
        }

        public bool Contains(Dog dog)
        {
            return dogsById.ContainsKey(dog.Id);
        }

        public Dog GetDog(string name, string ownerId)
        {
            CheckIfOwnerExists(ownerId);
            CheckIfDogExists(name, ownerId);

            return ownersById[ownerId].dogs[name];
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            CheckIfOwnerExists(ownerId);
            CheckIfDogExists(name, ownerId);

            var dogToDelete = ownersById[ownerId].dogs[name];

            ownersById[ownerId].dogs.Remove(name);
            dogsById.Remove(dogToDelete.Id);
            RemoveFromDogDict(dogToDelete);
            sortedDogsByAgeNameAndOwnerName.Remove(dogToDelete);

            return dogToDelete;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            CheckIfOwnerExists(ownerId);

            return ownersById[ownerId].dogs.Values;
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            if (!dogsByBreed.ContainsKey(breed))
            {
                throw new ArgumentException($"There is no dogs with breed {breed}");
            }

            return dogsByBreed[breed];
        }

        public void Vaccinate(string name, string ownerId)
        {
            CheckIfOwnerExists(ownerId);
            CheckIfDogExists(name, ownerId);

            var dog = ownersById[ownerId].dogs[name];
            dog.Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            CheckIfOwnerExists(ownerId);
            CheckIfDogExists(oldName, ownerId);

            var dogToRename = ownersById[ownerId].dogs[oldName];
            ownersById[ownerId].dogs.Remove(oldName);
            dogToRename.Name = newName;
            ownersById[ownerId].dogs.Add(newName, dogToRename);
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            if (!dogsByAge.ContainsKey(age))
            {
                throw new ArgumentException($"There is no dogs with age {age}");
            }

            return dogsByAge[age];
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            List<Dog> result = new List<Dog>(); 
            var ages = dogsAge.GetViewBetween(lo, hi);

            foreach (var age in ages)
            {
                result.AddRange(dogsByAge[age]);
            }

            return result;
            //return dogsById.Values.Where(d => d.Age >= lo && d.Age <= hi);
            //TO DO: can be done with ordered set  
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            return sortedDogsByAgeNameAndOwnerName;
        }

        private void AddToDogDictionaries(Dog dog)
        {
            if (!dogsByBreed.ContainsKey(dog.Breed))
            {
                dogsByBreed.Add(dog.Breed, new HashSet<Dog>());
            }

            dogsByBreed[dog.Breed].Add(dog);

            if (!dogsByAge.ContainsKey(dog.Age))
            {
                dogsAge.Add(dog.Age);
                dogsByAge.Add(dog.Age, new HashSet<Dog>());
            }

            dogsByAge[dog.Age].Add(dog);
        }

        private void RemoveFromDogDict(Dog dog)
        {
            dogsByBreed[dog.Breed].Remove(dog);

            if (dogsByBreed[dog.Breed].Count == 0)
            {
                dogsByBreed.Remove(dog.Breed);
            }

            dogsByAge[dog.Age].Remove(dog);

            if (dogsByAge[dog.Age].Count == 0)
            {
                dogsAge.Remove(dog.Age);
                dogsByAge.Remove(dog.Age);
            }
        }

        private void CheckIfOwnerExists(string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException($"There is no such owner: {ownerId}");
            }
        }

        private void CheckIfDogExists(string name, string ownerId)
        {
            if (!ownersById[ownerId].dogs.ContainsKey(name))
            {
                throw new ArgumentException($"Owner {ownerId} dosen't have dog with name {name}");
            }
        }
    }
}