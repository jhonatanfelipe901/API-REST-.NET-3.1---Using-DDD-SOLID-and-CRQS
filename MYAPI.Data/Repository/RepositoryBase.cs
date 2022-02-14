using Microsoft.EntityFrameworkCore;
using MyAPI.Domain.Entities;
using MyAPI.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using MyAPI.CrossCutting.Helpers;
using MYAPI.Data.Context;
using MyAPI.CrossCutting.Settings;

namespace MYAPI.Data.Repository
{
    
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : BaseEntity
    {

        protected readonly MyApiDBContext myApiContext;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(MyApiDBContext dbContext)
        {
            myApiContext = dbContext;
            DbSet = myApiContext.Set<TEntity>();
        }


        public void Add(TEntity entity)
        {
            entity.CreateDate = Dates.GetBrazilianDate();
            HandleUpdateDateValue(entity);

            DbSet.Add(entity);
            SaveChanges();
        }

        public void AddRange(List<TEntity> entity)
        {
            if (IsBaseEntity())
                entity.ForEach(x => x.CreateDate = Dates.GetBrazilianDate());

            entity.ForEach(x => HandleUpdateDateValue(x));
            DbSet.AddRange(entity);
            SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            if (IsBaseEntity())
                entity.ToList().ForEach(x => x.CreateDate = Dates.GetBrazilianDate());

            entity.ToList().ForEach(x => HandleUpdateDateValue(x));

            DbSet.AddRange(entity);
            SaveChanges();
        }

        public void AddOrUpdate(TEntity entity)
        {
            if (entity == null)
                return;

            if (entity.Id == 0)
                Add(entity);
            else
                Update(entity);
        }

        public TEntity GetById(long id)
        {
            return DbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            HandleUpdateDateValue(entity);
            DbSet.Update(entity);
            SaveChanges();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        private void SaveChanges()
        {
            if (myApiContext != null)
                myApiContext.SaveChanges();
        }

        protected virtual bool IsBaseEntity() { return true; }
        protected virtual string GetConnectionString() { return Settings.ConnectionString; }
        protected string GetTreapDatabaseName()
        {
            string separator = ";";
            var connectionStringSplited = Settings.ConnectionString.Split(separator.ToCharArray());
            foreach (var connectionStringPart in connectionStringSplited)
            {
                if (connectionStringPart.Contains("Database="))
                    return connectionStringPart.Substring(connectionStringPart.IndexOf("=") + 1).Trim();
            }

            return null;
        }

        public void Dispose()
        {
            if (myApiContext != null)
                myApiContext.Dispose();

            GC.SuppressFinalize(this);
        }

        protected void HandleUpdateDateValue(TEntity entity)
        {
            if (HasProperty(entity, "UpdateDate"))
                entity.GetType().GetProperty("UpdateDate").SetValue(entity, Dates.GetBrazilianDate());
        }

        protected bool HasProperty(TEntity entity, string propertyName)
        {
            return entity.GetType().GetProperty(propertyName) != null;
        }

        public TEntity Get<TFirst>(Dictionary<string, Expression<Func<TEntity, object>>> inners, string query)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                TEntity entity;
                try
                {
                    con.Open();
                    var entityDictionary = new Dictionary<long, TEntity>();
                    entity = con.Query<TEntity, TFirst, TEntity>(
                        query,
                        (e, first) =>
                        {
                            TEntity eEntry;

                            if (!entityDictionary.TryGetValue((long)GetPropertyValue(e, "Id"), out eEntry))
                            {
                                eEntry = e;
                                entityDictionary.Add((long)GetPropertyValue(eEntry, "Id"), eEntry);
                            }
                            if (first != null && IsCreated(first))
                                HandleSetPropertyValue<TFirst>(eEntry, inners.ElementAt(0).Value, first);

                            return eEntry;
                        },
                        splitOn: string.Join(",", inners.Select(i => i.Key)))
                        .FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return entity;
            }
        }

        public IEnumerable<TEntity> GetList<TFirst>(Dictionary<string, Expression<Func<TEntity, object>>> inners, string query)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                var list = new List<TEntity>();
                try
                {
                    con.Open();
                    var entityDictionary = new Dictionary<long, TEntity>();
                    list = con.Query<TEntity, TFirst, TEntity>(
                        query,
                        (e, first) =>
                        {
                            TEntity eEntry;

                            if (!entityDictionary.TryGetValue((long)GetPropertyValue(e, "Id"), out eEntry))
                            {
                                eEntry = e;
                                entityDictionary.Add((long)GetPropertyValue(eEntry, "Id"), eEntry);
                            }
                            if (first != null && IsCreated(first))
                                HandleSetPropertyValue<TFirst>(eEntry, inners.ElementAt(0).Value, first);

                            return eEntry;
                        },
                        splitOn: string.Join(",", inners.Select(i => i.Key)))
                        .Distinct()
                        .ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return list;
            }
        }

        public IEnumerable<TEntity> GetList<TFirst, TSecond>(Dictionary<string, Expression<Func<TEntity, object>>> inners, string query)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                var list = new List<TEntity>();
                try
                {
                    con.Open();
                    var entityDictionary = new Dictionary<long, TEntity>();
                    list = con.Query<TEntity, TFirst, TSecond, TEntity>(
                        query,
                        (e, first, second) =>
                        {
                            TEntity eEntry;

                            if (!entityDictionary.TryGetValue((long)GetPropertyValue(e, "Id"), out eEntry))
                            {
                                eEntry = e;
                                entityDictionary.Add((long)GetPropertyValue(eEntry, "Id"), eEntry);
                            }
                            if (first != null && IsCreated(first))
                                HandleSetPropertyValue<TFirst>(eEntry, inners.ElementAt(0).Value, first);

                            if (second != null && IsCreated(second))
                                HandleSetPropertyValue<TSecond>(eEntry, inners.ElementAt(1).Value, second);

                            return eEntry;
                        },
                        splitOn: string.Join(",", inners.Select(i => i.Key)))
                        .Distinct()
                        .ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return list;
            }
        }

        public IEnumerable<TEntity> GetList<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(Dictionary<string, Expression<Func<TEntity, object>>> inners, string query)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                var list = new List<TEntity>();
                try
                {
                    con.Open();
                    var entityDictionary = new Dictionary<long, TEntity>();
                    list = con.Query<TEntity, TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TEntity>(
                        query,
                        (e, first, second, third, fourth, fifth, sixth) =>
                        {
                            TEntity eEntry;

                            if (!entityDictionary.TryGetValue((long)GetPropertyValue(e, "Id"), out eEntry))
                            {
                                eEntry = e;
                                entityDictionary.Add((long)GetPropertyValue(eEntry, "Id"), eEntry);
                            }
                            if (first != null && IsCreated(first))
                                HandleSetPropertyValue<TFirst>(eEntry, inners.ElementAt(0).Value, first);

                            if (second != null && IsCreated(second))
                                HandleSetPropertyValue<TSecond>(eEntry, inners.ElementAt(1).Value, second);

                            if (third != null && IsCreated(third))
                                HandleSetPropertyValue<TThird>(eEntry, inners.ElementAt(2).Value, third);

                            if (fourth != null && IsCreated(fourth))
                                HandleSetPropertyValue<TFourth>(eEntry, inners.ElementAt(3).Value, fourth);

                            if (fifth != null && IsCreated(fifth))
                                HandleSetPropertyValue<TFifth>(eEntry, inners.ElementAt(4).Value, fifth);

                            if (sixth != null && IsCreated(sixth))
                                HandleSetPropertyValue<TSixth>(eEntry, inners.ElementAt(5).Value, sixth);

                            return eEntry;
                        },
                        splitOn: string.Join(",", inners.Select(i => i.Key)))
                        .Distinct()
                        .ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return list;
            }
        }

        protected object GetPropertyValue(object e, string propName)
        {
            return e.GetType().GetProperty(propName).GetValue(e);
        }

        protected void HandleSetPropertyValue<T>(TEntity e, Expression<Func<TEntity, object>> expression, object objeto)
        {
            if (!typeof(IEnumerable<T>).IsAssignableFrom(e.GetType().GetProperty(GetMemberName(expression.Body)).PropertyType))
                e.GetType().GetProperty(GetMemberName(expression.Body)).SetValue(e, objeto);
            else
            {
                var listPropName = GetMemberName(expression.Body);
                var currentPropValue = (ICollection<T>)GetPropertyValue(e, listPropName);

                if (!ItemExistsInCollection<T>(currentPropValue, objeto))
                {
                    var addList = new[] { objeto };
                    e.GetType().GetProperty(listPropName).PropertyType.GetMethod("Add").Invoke(currentPropValue, addList);
                }
            }
        }

        protected static string GetMemberName(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)expression).Member.Name;
                case ExpressionType.Convert:
                    return GetMemberName(((UnaryExpression)expression).Operand);
                default:
                    throw new NotSupportedException(expression.NodeType.ToString());
            }
        }

        private bool ItemExistsInCollection<T>(ICollection<T> collection, object objeto)
        {
            var propertyName = "Id";
            var id = (long)GetPropertyValue(objeto, propertyName);
            foreach (var item in collection)
            {
                var currentId = (long)GetPropertyValue(item, propertyName);
                if (currentId == id)
                    return true;
            }

            return false;
        }

        protected bool IsCreated(object objeto, string propertyName = "CreateDate")
        {
            if (objeto.GetType().Name == "CacheHotelsV2")
                return (string)GetPropertyValue(objeto, "HotelId") != string.Empty;

            return (DateTime)GetPropertyValue(objeto, propertyName) != default(DateTime);
        }
    }
}
