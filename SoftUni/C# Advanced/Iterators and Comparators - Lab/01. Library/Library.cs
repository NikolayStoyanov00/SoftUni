using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IteratorsAndComparators
{
    public class Library : IEnumerable<Book>
    {
        private List<Book> books;

        public Library(params Book[] books)
        {
            this.books = new List<Book>(books);
        }

        public void Add(Book book)
        {
            this.books.Add(book);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            foreach (var book in this.books)
            {
                yield return book;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class LibraryIterator : IEnumerator<Book>
        {
            private readonly List<Book> books;
            private int currentIndex;
            
            public LibraryIterator(IEnumerable<Book> books)
            {
                this.Reset();
                this.books = new List<Book>(books);
            }

            public void Dispose() { }

            public bool MoveNext() => ++this.currentIndex < this.books.Count;
            public void Reset() => this.currentIndex = -1;

            public Book Current => this.books[currentIndex];
            object IEnumerator.Current => this.Current;
        }
    }
}
