namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> membersById = new Dictionary<int, Member>();
        private Dictionary<int, Trainer> trainersById = new Dictionary<int, Trainer>();
        private Dictionary<Trainer, HashSet<Member>> membersTrained = new Dictionary<Trainer, HashSet<Member>>();

        //private SortedSet<Member> orderedByRegAscThenByNameDesc = new SortedSet<Member>(OrderByRegAndName); if we need preformance
        public void AddMember(Member member)
        {
            if (membersById.ContainsKey(member.Id))
            {
                throw new ArgumentException($"Member with id {member.Id} already exists.");
            }

            membersById.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (trainersById.ContainsKey(trainer.Id))
            {
                throw new ArgumentException($"Trainer with id {trainer.Id} already exists.");
            }

            trainersById.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            CheckIfTrainerExists(trainer);

            if (member.Trainer != null)
            {
                throw new ArgumentException($"Member {member.Id} already has a trainer.");
            }

            if (!membersById.ContainsKey(member.Id))
            {
                membersById.Add(member.Id, member);
            }

            member.Trainer = trainer;
            
            if (!membersTrained.ContainsKey(trainer))
            {
                membersTrained.Add(trainer, new HashSet<Member>());
            }

            membersTrained[trainer].Add(member);

        }

        public bool Contains(Member member)
        {
            return membersById.ContainsKey(member.Id);
        }

        public bool Contains(Trainer trainer)
        {
            return trainersById.ContainsKey(trainer.Id);
        }

        public Trainer FireTrainer(int id)
        {
            if (!trainersById.ContainsKey(id))
            {
                throw new ArgumentException($"Trainer {id} does not exist!");
            }

            var trainerToDelete = trainersById[id];

            if (membersTrained.ContainsKey(trainerToDelete))
            {
                foreach (var member in membersTrained[trainerToDelete])
                {
                    member.Trainer = null;
                }

                membersTrained.Remove(trainerToDelete);
            }
            
            trainersById.Remove(id);

            return trainerToDelete;
        }

        public Member RemoveMember(int id)
        {
            var memberToDelete = membersById[id];

            if (memberToDelete.Trainer != null)
            {
                membersTrained[memberToDelete.Trainer].Remove(memberToDelete);
            }

            membersById.Remove(id);

            return memberToDelete;
        }

        public int MemberCount => membersById.Count;
        public int TrainerCount => trainersById.Count;

        public IEnumerable<Member> 
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {
            return membersById.Values.OrderBy(m => m.RegistrationDate).ThenByDescending(m => m.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return trainersById.Values.OrderBy(t => t.Popularity);
        }

        public IEnumerable<Member> 
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {
            if (!membersTrained.ContainsKey(trainer))
            {
                return new List<Member>();
            }

            return membersTrained[trainer].OrderBy(m => m.RegistrationDate).ThenBy(m => m.Name);
        }

        public IEnumerable<Member> 
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {
            return membersById.Values
                .Where(m => m.Trainer.Popularity >= lo && m.Trainer.Popularity <= hi)
                .OrderBy(m => m.Visits)
                .ThenBy(m => m.Name);
        }

        public Dictionary<Trainer, HashSet<Member>> 
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            return membersTrained.OrderBy(mt => mt.Value.Count).ThenBy(mt => mt.Key.Popularity)
                .ToDictionary(mt => mt.Key, mt => mt.Value);
        }

        private void CheckIfTrainerExists(Trainer trainer)
        {
            if (!trainersById.ContainsKey(trainer.Id))
            {
                throw new ArgumentException($"Trainer {trainer.Id} does not exist.");
            }
        }
    }
}