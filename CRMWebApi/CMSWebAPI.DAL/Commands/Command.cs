using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CMSWebAPI.DAL.Commands
{
    public abstract class Command<TEntity, TKey> where TEntity : class
    {
        protected int? UID { get; set; }
        protected DbContext DbContext { get; set; }
        protected DbSet<TEntity> DbSet { get { return DbContext.Set<TEntity>(); } }        

        public virtual TEntity Find(TKey key)
        {
            return DbSet.Find(key);
        }

        public virtual void Delete(TKey key)
        {
            var entity = DbSet.Find(key);
            if (entity != null)
            {
                if (DbContext.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                DbSet.Remove(entity);
            }
        }
    }

    public abstract class Command<TEntity> : Command<TEntity, Guid> where TEntity : class
    {
        protected bool IsDisposable = true;

        public virtual TEntity Find(int key)
        {
            return DbSet.Find(key);
        }

        public virtual DbSet<TEntity> GetAll()
        {
            return DbSet; 
        }

        public virtual bool Any()
        {
            return DbSet.Any();
        }
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual void Commit()
        {
            try
            {
                DbContext.SaveChanges();              
            }
            catch(Exception ex)
            {                           
                throw new Exception(ex.Message);
            }            
        }
        
        public virtual List<TEntity> Get(int pageSize, int pageNumber, Dictionary<string, List<string>> filters, string sortOrder, string sortBy)
        {
            var filterExpression = FilterExpressionBuilder(filters);

            ParameterExpression parameterExpression = null;
            MemberExpression propertyExpression = null;

            var sortPropertyDataType=GetSortPropertyAndType(sortBy,out parameterExpression,out propertyExpression);
            Type sortMethodClassType = GetType();            
            MethodInfo sortMethodInfo = sortMethodClassType.GetMethod("SortExpressionBuilder");
            Type[] sortMethodGenericArugements = new Type[] { sortPropertyDataType };
            MethodInfo genericSortMethodInfo = sortMethodInfo.MakeGenericMethod(sortMethodGenericArugements);
            object sortExpression = genericSortMethodInfo.Invoke(this, new object[] { sortBy,parameterExpression,propertyExpression });

         

            // var sortExpression1 = SortExpressionBuilder<int>(sortBy);
            var skipItems = (pageNumber-1) * pageSize;
            var filteredData = new List<TEntity>();
            if (sortOrder == "1")
            {
                if (pageSize == 0)
                {
                    //filteredData = DbSet.AsQueryable<TEntity>().OrderBy(sortExpression).Where(filterExpression).ToList();
                    filteredData = DbSet.AsQueryable<TEntity>().Where(filterExpression).ToList();
                }
                else
                {
                    //filteredData = DbSet.AsQueryable<TEntity>().OrderBy(sortExpression).Where(filterExpression).Skip(skipItems).Take(pageSize).ToList();
                    filteredData = DbSet.AsQueryable<TEntity>().Where(filterExpression).Skip(skipItems).Take(pageSize).ToList();
                }
                
            }
            else
            {
                if (pageSize == 0)
                {
                    //filteredData = DbSet.AsQueryable<TEntity>().OrderByDescending(sortExpression).Where(filterExpression).ToList();
                    filteredData = DbSet.AsQueryable<TEntity>().Where(filterExpression).ToList();
                }
                else
                {
                    //filteredData = DbSet.AsQueryable<TEntity>().OrderByDescending(sortExpression).Where(filterExpression).Skip(skipItems).Take(pageSize).ToList();
                    filteredData = DbSet.AsQueryable<TEntity>().Where(filterExpression).Skip(skipItems).Take(pageSize).ToList();
                }
                
            }
            return filteredData;

        }

        public virtual int GetCount(Dictionary<string, List<string>> filters)
        {
            var filterExpression = FilterExpressionBuilder(filters);
            return DbSet.AsQueryable<TEntity>().Where(filterExpression).Count();
        }

        private Func<TEntity, bool> FilterExpressionBuilder(Dictionary<string, List<string>> filters)
        {
            var type = typeof(TEntity);
            var parameterExpression = Expression.Parameter(type);
            Expression finalExpression = Expression.Constant(true);
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    //Set property name for filter
                    var propertyName = filter.Key;
                    var property = Expression.Property(parameterExpression, propertyName);
                    Expression expressionForOnePropertyValue = Expression.Constant(false);

                    foreach (var value in filter.Value)
                    {
                        //Property value to be compare
                        var valueToCompare = Convert.ChangeType(value, property.Type);
                        var constant = Expression.Constant(valueToCompare);
                        var expression = Expression.Equal(property, constant);
                        expressionForOnePropertyValue = Expression.Or(expressionForOnePropertyValue, expression);
                    }

                    finalExpression = Expression.And(finalExpression, expressionForOnePropertyValue);
                }
            }
            var lambda = Expression.Lambda<Func<TEntity, bool>>(finalExpression, parameterExpression);
            var compiledLambda = lambda.Compile();
            return compiledLambda;
        }
      
        public Type GetSortPropertyAndType(string sortBy,out ParameterExpression parameterExpression,out MemberExpression property)
        {
            var type = typeof(TEntity);
            parameterExpression = Expression.Parameter(type);
            property = Expression.Property(parameterExpression, sortBy);
            return property.Type;
        }

        public Func<TEntity, SortPropType> SortExpressionBuilder<SortPropType>(string sortBy,ParameterExpression parameterExpression,MemberExpression property)
        {
            //var type = typeof(TEntity);
            //var parameterExpression = Expression.Parameter(type);
            //var property = Expression.Property(parameterExpression, sortBy);
            var propExpression = Expression.Lambda<Func<TEntity, SortPropType>>(property, parameterExpression);
            var compiledLambda = propExpression.Compile();
            return compiledLambda;
        }

    }
}
