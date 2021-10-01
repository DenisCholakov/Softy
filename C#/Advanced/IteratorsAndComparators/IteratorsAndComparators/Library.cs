using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IteratorsAndComparators
{
    public class Library : IEnumerable<Book>
    {
        public Library(params Book[] books)
        {
            this.Books = new SortedSet<Book>(books, new BookComparator());
        }
        public SortedSet<Book> Books { get; set; }

        public IEnumerator<Book> GetEnumerator()
        {
            return new LibraryIterator(this.Books.ToList());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class LibraryIterator : IEnumerator<Book>
        {
            private int index = -1;
            public LibraryIterator(List<Book> books)
            {
                this.Books = books;
            }
            public List<Book> Books { get; set; }
            public Book Current => this.Books[index];

            object IEnumerator.Current => this.Books[index];

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return ++this.index < this.Books.Count;
            }

            public void Reset()
            {
                this.index = -1;
            }
        }
    }
}
