using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SaveChangesMaybe.Core;
using SaveChangesMaybe.Models;
using Z.BulkOperations;

namespace SaveChangesMaybe.Extensions
{
    public static class BulkDeleteMaybeExtensions
    {
        // DbContext

        // No BatchSize, No Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbContext dbContext, List<T> entities, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down
            await Task.Run(() =>
            {
                BulkDeleteMaybe(dbContext, entities, null, null);
            }, cancellationToken).ConfigureAwait(false);
        }

        // BatchSize, No Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbContext dbContext, List<T> entities, int batchSize, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down
            await Task.Run(() =>
            {
                BulkDeleteMaybe(dbContext, entities, batchSize, null);
            }, cancellationToken).ConfigureAwait(false);
        }

        // BatchSize, Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbContext dbContext, List<T> entities, int batchSize, Action<BulkOperation<T>> options, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down
            await Task.Run(() =>
            {
                BulkDeleteMaybe(dbContext, entities, batchSize, options);
            }, cancellationToken).ConfigureAwait(false);
        }

        // No BatchSize, Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbContext dbContext, List<T> entities, Action<BulkOperation<T>> options, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down
            await Task.Run(() =>
            {
                BulkDeleteMaybe(dbContext, entities, null, options);
            }, cancellationToken).ConfigureAwait(false);
        }


        public static void BulkDeleteMaybe<T>(this DbContext dbContext, List<T> entities, int? batchSize = null, Action<BulkOperation<T>>? options = null) where T : class
        {
            var callback = new Action<List<object>>(list =>
            {
                dbContext.FutureAction(x => x.BulkDelete(list, options));
            });

            var wrapper = new SaveChangesMaybeWrapper<T>
            (
                callback,
                options,
                batchSize,
                dbContext,
                SaveChangesMaybeOperationType.BulkDelete,
                entities
            );

            SaveChangesMaybeHelper.SaveChangesMaybe(wrapper);
        }

        // DbSet

        // No BatchSize, No Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbSet<T> dbSet, List<T> entities, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down

            await Task.Run(() =>
            {
                BulkDeleteMaybe<T>(dbSet, entities, null, null);
            }, cancellationToken).ConfigureAwait(false);
        }

        // BatchSize, No Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbSet<T> dbSet, List<T> entities, int batchSize, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down

            await Task.Run(() =>
            {
                BulkDeleteMaybe<T>(dbSet, entities, batchSize, null);
            }, cancellationToken).ConfigureAwait(false);
        }

        // BatchSize, Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbSet<T> dbSet, List<T> entities, int batchSize, Action<BulkOperation<T>> options, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down

            await Task.Run(() =>
            {
                BulkDeleteMaybe<T>(dbSet, entities, batchSize, options);
            }, cancellationToken).ConfigureAwait(false);
        }

        // No BatchSize, Options
        public static async Task BulkDeleteMaybeAsync<T>(this DbSet<T> dbSet, List<T> entities, Action<BulkOperation<T>> options, CancellationToken cancellationToken) where T : class
        {
            /// TODO: async all the way down

            await Task.Run(() =>
            {
                BulkDeleteMaybe<T>(dbSet, entities, null, options);
            }, cancellationToken).ConfigureAwait(false);
        }


        public static void BulkDeleteMaybe<T>(this DbSet<T> dbSet, List<T> entities, int? batchSize, Action<BulkOperation<T>>? options = null) where T : class
        {
            var dbContext = dbSet.GetService<ICurrentDbContext>().Context;

            var callback = new Action<List<object>>(list =>
            {
                dbContext.FutureAction(x => x.BulkDelete(list, options));
            });

            var wrapper = new SaveChangesMaybeWrapper<T>
            (
                callback,
                options,
                batchSize,
                dbContext,
                SaveChangesMaybeOperationType.BulkDelete,
                entities
            );

            SaveChangesMaybeHelper.SaveChangesMaybe(wrapper);
        }
    }
}
