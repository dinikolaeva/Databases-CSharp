﻿namespace MiniORM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DbSet<TEntity> : ICollection<TEntity>
       where TEntity : class, new()
    {
        internal ChangeTracker<TEntity> ChangeTracker { get; set; }
        internal IList<TEntity> Entities { get; set; }

        internal DbSet(IEnumerable<TEntity> entites)
        {
            this.Entities = entites.ToList();
            this.ChangeTracker = new ChangeTracker<TEntity>(entites);
        }

        public int Count => this.Entities.Count();

        public bool IsReadOnly => this.Entities.IsReadOnly;

        public void Add(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            this.Entities.Add(item);
            this.ChangeTracker.Add(item);
        }

        public void Clear()
        {
            while (this.Entities.Any())
            {
                var entity = this.Entities.First();
                this.Remove(entity);
            }
        }

        public bool Remove(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            var removedSuccessfully = this.Entities.Remove(item);

            if (removedSuccessfully)
            {
                this.ChangeTracker.Remove(item);
            }

            return removedSuccessfully;
        }

        public bool Contains(TEntity item) => this.Entities.Contains(item);

        public void CopyTo(TEntity[] array, int arrayIndex) => this.Entities.CopyTo(array, arrayIndex);

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var e in entities.ToArray())
            {
                this.Remove(e);
            }
        }
    }
}