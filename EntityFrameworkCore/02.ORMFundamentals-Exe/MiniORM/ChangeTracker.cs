namespace MiniORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public class ChangeTracker<T>
        where T : class, new()
    {

        private readonly List<T> allEntities;
        private readonly List<T> added;
        private readonly List<T> removerd;

        public IReadOnlyCollection<T> AllEntities => this.allEntities.AsReadOnly();
        public IReadOnlyCollection<T> Added => this.added.AsReadOnly();
        public IReadOnlyCollection<T> Removed => this.removerd.AsReadOnly();

        public ChangeTracker(IEnumerable<T> entities)
        {
            this.added = added;
            this.removerd = removerd;

            this.allEntities = CloneEntities(entities);
        }

        private static List<T> CloneEntities(IEnumerable<T> entities)
        {
            var clonedEntities = new List<T>();
            var propertiesToClone = typeof(T).GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            foreach (var e in entities)
            {
                var clone = Activator.CreateInstance<T>();

                foreach (var prop in propertiesToClone)
                {
                    var value = prop.GetValue(e);
                    prop.SetValue(clone, value);
                }

                clonedEntities.Add(clone);
            }

            return clonedEntities;
        }

        public void Add(T item) => this.added.Add(item);
        public void Remove(T item) => this.removerd.Add(item);

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            var modifiedEntites = new List<T>();

            var primaryKeys = typeof(T)
                 .GetProperties()
                 .Where(pi => pi.HasAttribute<KeyAttribute>())
                 .ToArray();

            foreach (T proxyEntity in this.AllEntities)
            {
                var primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                T entity = dbSet
                    .Entities
                    .Single(e => GetPrimaryKeyValues(primaryKeys, e)
                    .SequenceEqual(primaryKeyValues));

                bool isModified = IsModified(proxyEntity, entity);

                if (isModified)
                {
                    modifiedEntites.Add(entity);
                }
            }

            return modifiedEntites;
        }

        private bool IsModified(T proxyEntity, T entity)
        {
            var monitoredProperties = typeof(T).GetProperties()
                                               .Where(pi => DbContext
                                               .AllowedSqlTypes
                                               .Contains(pi.PropertyType))
                                               .ToArray();

            PropertyInfo[] modifiedProperties = monitoredProperties
                                               .Where(pi => !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity)  ))  
                                               .ToArray();

            bool isModified = modifiedProperties.Any();

            return isModified;
        }

        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T proxyEntity)
        {
            return primaryKeys.Select(pk => pk.GetValue(proxyEntity));
        }
    }
}
