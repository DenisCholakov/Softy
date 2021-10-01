namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> bySN = new Dictionary<string, Invoice>();

        public void Create(Invoice invoice)
        {
            if (bySN.ContainsKey(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            bySN.Add(invoice.SerialNumber, invoice);
        }

        public void ThrowInvoice(string number)
        {
            if (!bySN.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            bySN.Remove(number);
        }

        public void ThrowPayed()
        {
            var toRemove = bySN.Values.Where(i => i.Subtotal == 0).Select(i => i.SerialNumber).ToList();

            foreach (var invoiceSN in toRemove)
            {
                bySN.Remove(invoiceSN);
            }
        }

        public int Count()
        {
            return bySN.Count;
        }

        public bool Contains(string number)
        {
            return bySN.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            var toPay = bySN.Values.Where(i => i.DueDate.Date == due.Date);

            if (toPay.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in toPay)
            {
                item.Subtotal = 0;
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            return bySN.Values.Where(i => i.IssueDate.Date >= start.Date && i.IssueDate.Date <= end.Date)
                .OrderBy(i => i.IssueDate).ThenBy(i => i.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var keys = bySN.Keys.Where(k => k.Contains(serialNumber));

            if (keys.Count() == 0)
            {
                throw new ArgumentException();
            }

            return keys.OrderByDescending(k => k).Select(k => bySN[k]);
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var toRemove = bySN.Values.Where(i => i.DueDate.Date > start.Date && i.DueDate.Date < end.Date);

            if (toRemove.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in toRemove)
            {
                bySN.Remove(item.SerialNumber);
            }

            return toRemove;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            return bySN.Values.Where(i => i.Department == department).OrderByDescending(i => i.Subtotal).ThenBy(i => i.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            return bySN.Values.Where(i => i.CompanyName == company).OrderByDescending(i => i.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            var forUpdate = bySN.Values.Where(i => i.DueDate.Date == dueDate.Date);

            if (forUpdate.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in forUpdate)
            {
                item.DueDate = item.DueDate.AddDays(days);
            }
        }
    }
}
